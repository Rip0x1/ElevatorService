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
    public partial class ClientsPage : Page
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string ApiBaseUrl = "https://localhost:7258/api/Clients";
        private Client _editingClient = null;
        private List<Client> _allClients = new List<Client>();

        public ClientsPage()
        {
            InitializeComponent();
            LoadClients();
        }

        private async void LoadClients()
        {
            try
            {
                ClientsGrid.IsEnabled = false;
                var response = await _httpClient.GetStringAsync(ApiBaseUrl);
                _allClients = JsonConvert.DeserializeObject<List<Client>>(response);
                ApplyFiltersAndSearch();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
            finally
            {
                ClientsGrid.IsEnabled = true;
            }
        }

        private void ApplyFiltersAndSearch()
        {
            IEnumerable<Client> filteredClients = _allClients;

            var searchText = SearchTextBox.Text?.Trim().ToLower();
            if (!string.IsNullOrEmpty(searchText))
            {
                filteredClients = filteredClients.Where(c =>
                    (!string.IsNullOrEmpty(c.CompanyName) && c.CompanyName.ToLower().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(c.ContactPerson) && c.ContactPerson.ToLower().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(c.Phone) && c.Phone.ToLower().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(c.Email) && c.Email.ToLower().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(c.Address) && c.Address.ToLower().Contains(searchText)));
            }

            ClientsGrid.ItemsSource = filteredClients.ToList();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFiltersAndSearch();
        }

        private void StatusFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFiltersAndSearch();
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            _editingClient = null;
            DialogTitle.Text = "Новый клиент";
            CompanyNameTextBox.Text = "";
            ContactPersonTextBox.Text = "";
            PhoneTextBox.Text = "";
            EmailTextBox.Text = "";
            AddressTextBox.Text = "";
            StatusComboBox.SelectedIndex = -1;
            ClientDialog.IsOpen = true;
        }

        private void EditClient_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Client client)
            {
                _editingClient = client;
                DialogTitle.Text = "Редактировать клиента";
                CompanyNameTextBox.Text = client.CompanyName;
                ContactPersonTextBox.Text = client.ContactPerson;
                PhoneTextBox.Text = client.Phone;
                EmailTextBox.Text = client.Email;
                AddressTextBox.Text = client.Address;
                ClientDialog.IsOpen = true;
            }
        }

        private async void SaveClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(CompanyNameTextBox.Text))
                {
                    MessageBox.Show("Введите название компании.");
                    return;
                }

                var client = new Client
                {
                    ClientId = _editingClient?.ClientId ?? 0,
                    CompanyName = CompanyNameTextBox.Text,
                    ContactPerson = ContactPersonTextBox.Text,
                    Phone = PhoneTextBox.Text,
                    Email = EmailTextBox.Text,
                    Address = AddressTextBox.Text,
                };

                HttpResponseMessage response;
                if (_editingClient == null)
                {
                    var content = new StringContent(JsonConvert.SerializeObject(client), Encoding.UTF8, "application/json");
                    response = await _httpClient.PostAsync(ApiBaseUrl, content);
                }
                else
                {
                    var content = new StringContent(JsonConvert.SerializeObject(client), Encoding.UTF8, "application/json");
                    response = await _httpClient.PutAsync($"{ApiBaseUrl}/{client.ClientId}", content);
                }

                if (response.IsSuccessStatusCode)
                {
                    ClientDialog.IsOpen = false;
                    LoadClients();
                    MessageBox.Show(_editingClient == null ? "Клиент добавлен." : "Клиент обновлен.");
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

        private async void DeleteClient_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Client client)
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить этого клиента?", "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        var response = await _httpClient.DeleteAsync($"{ApiBaseUrl}/{client.ClientId}");
                        if (response.IsSuccessStatusCode)
                        {
                            LoadClients();
                            MessageBox.Show("Клиент удален.");
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
            LoadClients();
        }

        private void ExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var clients = ClientsGrid.ItemsSource as IEnumerable<Client>;
                if (clients == null || !clients.Any())
                {
                    MessageBox.Show("Нет данных для экспорта.");
                    return;
                }

                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    DefaultExt = "xlsx",
                    FileName = $"Clients_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Clients");
                        var headers = new[] { "ID", "Название компании", "Контактное лицо", "Телефон", "Email", "Адрес"};
                        for (int i = 0; i < headers.Length; i++)
                        {
                            worksheet.Cell(1, i + 1).Value = headers[i];
                        }

                        int row = 2;
                        foreach (var client in clients)
                        {
                            worksheet.Cell(row, 1).Value = client.ClientId;
                            worksheet.Cell(row, 2).Value = client.CompanyName;
                            worksheet.Cell(row, 3).Value = client.ContactPerson;
                            worksheet.Cell(row, 4).Value = client.Phone;
                            worksheet.Cell(row, 5).Value = client.Email;
                            worksheet.Cell(row, 6).Value = client.Address;
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