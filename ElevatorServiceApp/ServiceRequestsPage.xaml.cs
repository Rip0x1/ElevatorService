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
    public partial class ServiceRequestsPage : Page
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string ApiBaseUrl = "https://localhost:7258/api/ServiceRequests";
        private ServiceRequest _editingRequest = null;
        private List<ServiceRequest> _allRequests = new List<ServiceRequest>();

        public ServiceRequestsPage()
        {
            InitializeComponent();
            LoadRequests();
            LoadComboBoxes();
        }

        private async void LoadRequests()
        {
            try
            {
                RequestsGrid.IsEnabled = false;
                var response = await _httpClient.GetStringAsync(ApiBaseUrl);
                _allRequests = JsonConvert.DeserializeObject<List<ServiceRequest>>(response);
                ApplyFiltersAndSearch();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
            finally
            {
                RequestsGrid.IsEnabled = true;
            }
        }

        private async void LoadComboBoxes()
        {
            try
            {
                var elevatorsResponse = await _httpClient.GetStringAsync("https://localhost:7258/api/Elevators");
                var elevators = JsonConvert.DeserializeObject<List<Elevator>>(elevatorsResponse);
                ElevatorIdComboBox.ItemsSource = elevators;

                var clientsResponse = await _httpClient.GetStringAsync("https://localhost:7258/api/Clients");
                var clients = JsonConvert.DeserializeObject<List<Client>>(clientsResponse);
                ClientIdComboBox.ItemsSource = clients;

                var employeesResponse = await _httpClient.GetStringAsync("https://localhost:7258/api/Employees");
                var employees = JsonConvert.DeserializeObject<List<Employee>>(employeesResponse);
                EmployeeIdComboBox.ItemsSource = employees;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных для выпадающих списков: {ex.Message}");
            }
        }

        private void ApplyFiltersAndSearch()
        {
            var filteredRequests = _allRequests.AsQueryable();

            if (StatusFilterComboBox.SelectedItem is ComboBoxItem selectedStatus && selectedStatus.Content.ToString() != "Все")
            {
                filteredRequests = filteredRequests.Where(r => r.Status == selectedStatus.Content.ToString());
            }

            var searchText = SearchTextBox.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(searchText))
            {
                filteredRequests = filteredRequests.Where(r => r.Description != null && r.Description.ToLower().Contains(searchText));
            }

            RequestsGrid.ItemsSource = filteredRequests.ToList();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFiltersAndSearch();
        }

        private void StatusFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFiltersAndSearch();
        }

        private void AddRequest_Click(object sender, RoutedEventArgs e)
        {
            _editingRequest = null;
            DialogTitle.Text = "Новая заявка";
            ElevatorIdComboBox.SelectedIndex = -1;
            ClientIdComboBox.SelectedIndex = -1;
            EmployeeIdComboBox.SelectedIndex = -1;
            RequestDatePicker.SelectedDate = null;
            DescriptionTextBox.Text = "";
            StatusComboBox.SelectedIndex = -1;
            PriorityComboBox.SelectedIndex = -1;
            RequestDialog.IsOpen = true;
        }

        private void EditRequest_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is ServiceRequest request)
            {
                _editingRequest = request;
                DialogTitle.Text = "Редактировать заявку";
                ElevatorIdComboBox.SelectedValue = request.ElevatorId;
                ClientIdComboBox.SelectedValue = request.ClientId;
                EmployeeIdComboBox.SelectedValue = request.EmployeeId;
                RequestDatePicker.SelectedDate = request.RequestDate;
                DescriptionTextBox.Text = request.Description;
                StatusComboBox.SelectedItem = StatusComboBox.Items.Cast<ComboBoxItem>().FirstOrDefault(x => x.Content.ToString() == request.Status) ?? StatusComboBox.Items[0];
                PriorityComboBox.SelectedItem = PriorityComboBox.Items.Cast<ComboBoxItem>().FirstOrDefault(x => x.Content.ToString() == request.Priority) ?? PriorityComboBox.Items[1];
                RequestDialog.IsOpen = true;
            }
        }

        private async void SaveRequest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(ElevatorIdComboBox.SelectedItem is Elevator selectedElevator))
                {
                    MessageBox.Show("Выберите лифт.");
                    return;
                }
                if (!(ClientIdComboBox.SelectedItem is Client selectedClient))
                {
                    MessageBox.Show("Выберите клиента.");
                    return;
                }
                if (!(EmployeeIdComboBox.SelectedItem is Employee selectedEmployee))
                {
                    MessageBox.Show("Выберите сотрудника.");
                    return;
                }
                if (RequestDatePicker.SelectedDate == null)
                {
                    MessageBox.Show("Выберите дату заявки.");
                    return;
                }

                var request = new ServiceRequest
                {
                    RequestId = _editingRequest?.RequestId ?? 0,
                    ElevatorId = selectedElevator.ElevatorId,
                    ClientId = selectedClient.ClientId,
                    EmployeeId = selectedEmployee.EmployeeId,
                    RequestDate = RequestDatePicker.SelectedDate.Value,
                    Description = DescriptionTextBox.Text,
                    Status = StatusComboBox.SelectedItem is ComboBoxItem statusItem ? statusItem.Content.ToString() : "Open",
                    Priority = PriorityComboBox.SelectedItem is ComboBoxItem priorityItem ? priorityItem.Content.ToString() : "Medium"
                };

                HttpResponseMessage response;
                if (_editingRequest == null)
                {
                    var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                    response = await _httpClient.PostAsync(ApiBaseUrl, content);
                }
                else
                {
                    var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                    response = await _httpClient.PutAsync($"{ApiBaseUrl}/{request.RequestId}", content);
                }

                if (response.IsSuccessStatusCode)
                {
                    RequestDialog.IsOpen = false;
                    LoadRequests();
                    MessageBox.Show(_editingRequest == null ? "Заявка добавлена." : "Заявка обновлена.");
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

        private async void DeleteRequest_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is ServiceRequest request)
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить эту заявку?", "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        var response = await _httpClient.DeleteAsync($"{ApiBaseUrl}/{request.RequestId}");
                        if (response.IsSuccessStatusCode)
                        {
                            LoadRequests();
                            MessageBox.Show("Заявка удалена.");
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
            LoadRequests();
        }

        private void ExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var requests = RequestsGrid.ItemsSource as IEnumerable<ServiceRequest>;
                if (requests == null || !requests.Any())
                {
                    MessageBox.Show("Нет данных для экспорта.");
                    return;
                }

                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    DefaultExt = "xlsx",
                    FileName = $"ServiceRequests_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Requests");
                        var headers = new[] { "ID", "Лифт", "Клиент", "Сотрудник", "Дата", "Описание", "Статус", "Приоритет" };
                        for (int i = 0; i < headers.Length; i++)
                        {
                            worksheet.Cell(1, i + 1).Value = headers[i];
                        }

                        int row = 2;
                        foreach (var request in requests)
                        {
                            worksheet.Cell(row, 1).Value = request.RequestId;
                            worksheet.Cell(row, 2).Value = request.ElevatorId;
                            worksheet.Cell(row, 3).Value = request.ClientId;
                            worksheet.Cell(row, 4).Value = request.EmployeeId;
                            worksheet.Cell(row, 5).Value = request.RequestDate.ToString("dd.MM.yyyy HH:mm");
                            worksheet.Cell(row, 6).Value = request.Description;
                            worksheet.Cell(row, 7).Value = request.Status;
                            worksheet.Cell(row, 8).Value = request.Priority;
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