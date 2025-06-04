using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using ClosedXML.Excel;
using Microsoft.Win32;

namespace ElevatorServiceApp
{
    public partial class ElevatorsPage : Page
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string ApiBaseUrl = "https://localhost:7258/api/Elevators";
        private Elevator _editingElevator = null;
        private List<Elevator> _allElevators = new List<Elevator>();

        public ElevatorsPage()
        {
            InitializeComponent();
            LoadElevators();
        }

        private async void LoadElevators()
        {
            try
            {
                ElevatorsGrid.IsEnabled = false;
                var response = await _httpClient.GetStringAsync(ApiBaseUrl);
                _allElevators = JsonConvert.DeserializeObject<List<Elevator>>(response);
                ApplyFiltersAndSearch();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
            finally
            {
                ElevatorsGrid.IsEnabled = true;
            }
        }

        private void ApplyFiltersAndSearch()
        {
            var filteredElevators = _allElevators.AsQueryable();

            var searchText = SearchTextBox.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(searchText))
            {
                filteredElevators = filteredElevators.Where(e => e.ElevatorId.ToString().ToLower().Contains(searchText) ||
                                                               e.SerialNumber.ToLower().Contains(searchText) ||
                                                               e.Model.ToLower().Contains(searchText));
            }

            if (StatusFilterComboBox.SelectedItem is ComboBoxItem selectedStatus && selectedStatus.Content.ToString() != "Все")
            {
                filteredElevators = filteredElevators.Where(e => e.Status == selectedStatus.Content.ToString());
            }

            ElevatorsGrid.ItemsSource = filteredElevators.ToList();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFiltersAndSearch();
        }

        private void StatusFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFiltersAndSearch();
        }

        private void AddElevator_Click(object sender, RoutedEventArgs e)
        {
            _editingElevator = null;
            DialogTitle.Text = "Новый лифт";
            SerialNumberTextBox.Text = "";
            ModelTextBox.Text = "";
            StatusComboBox.SelectedIndex = -1;
            InstallationDatePicker.SelectedDate = null;
            ElevatorDialog.IsOpen = true;
        }

        private void EditElevator_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Elevator elevator)
            {
                _editingElevator = elevator;
                DialogTitle.Text = "Редактировать лифт";
                SerialNumberTextBox.Text = elevator.SerialNumber;
                ModelTextBox.Text = elevator.Model;
                StatusComboBox.SelectedItem = StatusComboBox.Items.Cast<ComboBoxItem>().FirstOrDefault(x => x.Content.ToString() == elevator.Status) ?? StatusComboBox.Items[0];
                InstallationDatePicker.SelectedDate = elevator.InstallationDate;
                ElevatorDialog.IsOpen = true;
            }
        }

        private async void SaveElevator_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SerialNumberTextBox.Text) || string.IsNullOrWhiteSpace(ModelTextBox.Text))
                {
                    MessageBox.Show("Введите серийный номер и модель.");
                    return;
                }
                if (InstallationDatePicker.SelectedDate == null)
                {
                    MessageBox.Show("Выберите дату установки.");
                    return;
                }

                var elevator = new Elevator
                {
                    ElevatorId = _editingElevator?.ElevatorId ?? 0,
                    SerialNumber = SerialNumberTextBox.Text,
                    Model = ModelTextBox.Text,
                    Status = StatusComboBox.SelectedItem is ComboBoxItem statusItem ? statusItem.Content.ToString() : "Active",
                    InstallationDate = InstallationDatePicker.SelectedDate.Value
                };

                HttpResponseMessage response;
                if (_editingElevator == null)
                {
                    var content = new StringContent(JsonConvert.SerializeObject(elevator), Encoding.UTF8, "application/json");
                    response = await _httpClient.PostAsync(ApiBaseUrl, content);
                }
                else
                {
                    var content = new StringContent(JsonConvert.SerializeObject(elevator), Encoding.UTF8, "application/json");
                    response = await _httpClient.PutAsync($"{ApiBaseUrl}/{elevator.ElevatorId}", content);
                }

                if (response.IsSuccessStatusCode)
                {
                    ElevatorDialog.IsOpen = false;
                    LoadElevators();
                    MessageBox.Show(_editingElevator == null ? "Лифт добавлен." : "Лифт обновлен.");
                }
                else
                {
                    MessageBox.Show($"Ошибка: {await response.Content.ReadAsStringAsync()}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private async void DeleteElevator_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Elevator elevator)
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить этот лифт?", "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        var response = await _httpClient.DeleteAsync($"{ApiBaseUrl}/{elevator.ElevatorId}");
                        if (response.IsSuccessStatusCode)
                        {
                            LoadElevators();
                            MessageBox.Show("Лифт удален.");
                        }
                        else
                        {
                            MessageBox.Show($"Ошибка при удалении: {await response.Content.ReadAsStringAsync()}");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}");
                    }
                }
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadElevators();
        }

        private void ExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var elevators = ElevatorsGrid.ItemsSource as IEnumerable<Elevator>;
                if (elevators == null || !elevators.Any())
                {
                    MessageBox.Show("Нет данных для экспорта.");
                    return;
                }

                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    DefaultExt = "xlsx",
                    FileName = $"Elevators_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Elevators");
                        var headers = new[] { "ID", "Серийный номер", "Модель", "Статус", "Дата установки" };
                        for (int i = 0; i < headers.Length; i++)
                        {
                            worksheet.Cell(1, i + 1).Value = headers[i];
                        }

                        int row = 2;
                        foreach (var elevator in elevators)
                        {
                            worksheet.Cell(row, 1).Value = elevator.ElevatorId;
                            worksheet.Cell(row, 2).Value = elevator.SerialNumber;
                            worksheet.Cell(row, 3).Value = elevator.Model;
                            worksheet.Cell(row, 4).Value = elevator.Status;
                            worksheet.Cell(row, 5).Value = elevator.InstallationDate.ToString("dd.MM.yyyy");
                            row++;
                        }

                        worksheet.Columns().AdjustToContents();
                        workbook.SaveAs(saveFileDialog.FileName);
                    }
                    MessageBox.Show($"Данные успешно экспортированы в {saveFileDialog.FileName}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка экспорта: {ex.Message}");
            }
        }
    }
}