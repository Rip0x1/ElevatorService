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
    public partial class EmployeesPage : Page
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string ApiBaseUrl = "https://localhost:7258/api/Employees";
        private Employee _editingEmployee = null;
        private List<Employee> _allEmployees = new List<Employee>();

        public EmployeesPage()
        {
            InitializeComponent();
            LoadEmployees();
        }

        private async void LoadEmployees()
        {
            try
            {
                EmployeesGrid.IsEnabled = false;
                var response = await _httpClient.GetStringAsync(ApiBaseUrl);
                _allEmployees = JsonConvert.DeserializeObject<List<Employee>>(response);
                ApplyFiltersAndSearch();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
            finally
            {
                EmployeesGrid.IsEnabled = true;
            }
        }

        private void ApplyFiltersAndSearch()
        {
            IEnumerable<Employee> filteredEmployees = _allEmployees;

            var searchText = SearchTextBox.Text?.Trim().ToLower();
            if (!string.IsNullOrEmpty(searchText))
            {
                filteredEmployees = filteredEmployees.Where(e =>
                    (!string.IsNullOrEmpty(e.FirstName) && e.FirstName.ToLower().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(e.LastName) && e.LastName.ToLower().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(e.Position) && e.Position.ToLower().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(e.Phone) && e.Phone.ToLower().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(e.Email) && e.Email.ToLower().Contains(searchText)));
            }

            EmployeesGrid.ItemsSource = filteredEmployees.ToList();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFiltersAndSearch();
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            _editingEmployee = null;
            DialogTitle.Text = "Новый сотрудник";
            FirstNameTextBox.Text = "";
            LastNameTextBox.Text = "";
            PositionTextBox.Text = "";
            PhoneTextBox.Text = "";
            EmailTextBox.Text = "";
            EmployeeDialog.IsOpen = true;
        }

        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Employee employee)
            {
                _editingEmployee = employee;
                DialogTitle.Text = "Редактировать сотрудника";
                FirstNameTextBox.Text = employee.FirstName;
                LastNameTextBox.Text = employee.LastName;
                PositionTextBox.Text = employee.Position;
                PhoneTextBox.Text = employee.Phone;
                EmailTextBox.Text = employee.Email;
                EmployeeDialog.IsOpen = true;
            }
        }

        private async void SaveEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text) || string.IsNullOrWhiteSpace(LastNameTextBox.Text))
                {
                    MessageBox.Show("Введите имя и фамилию сотрудника.");
                    return;
                }

                var employee = new Employee
                {
                    EmployeeId = _editingEmployee?.EmployeeId ?? 0,
                    FirstName = FirstNameTextBox.Text,
                    LastName = LastNameTextBox.Text,
                    Position = PositionTextBox.Text,
                    Phone = PhoneTextBox.Text,
                    Email = EmailTextBox.Text
                };

                HttpResponseMessage response;
                if (_editingEmployee == null)
                {
                    var content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
                    response = await _httpClient.PostAsync(ApiBaseUrl, content);
                }
                else
                {
                    var content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
                    response = await _httpClient.PutAsync($"{ApiBaseUrl}/{employee.EmployeeId}", content);
                }

                if (response.IsSuccessStatusCode)
                {
                    EmployeeDialog.IsOpen = false;
                    LoadEmployees();
                    MessageBox.Show(_editingEmployee == null ? "Сотрудник добавлен." : "Сотрудник обновлен.");
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

        private async void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Employee employee)
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить этого сотрудника?", "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        var response = await _httpClient.DeleteAsync($"{ApiBaseUrl}/{employee.EmployeeId}");
                        if (response.IsSuccessStatusCode)
                        {
                            LoadEmployees();
                            MessageBox.Show("Сотрудник удален.");
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
            LoadEmployees();
        }

        private void ExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var employees = EmployeesGrid.ItemsSource as IEnumerable<Employee>;
                if (employees == null || !employees.Any())
                {
                    MessageBox.Show("Нет данных для экспорта.");
                    return;
                }

                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    DefaultExt = "xlsx",
                    FileName = $"Employees_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Employees");
                        var headers = new[] { "ID", "Имя", "Фамилия", "Должность", "Телефон", "Email" };
                        for (int i = 0; i < headers.Length; i++)
                        {
                            worksheet.Cell(1, i + 1).Value = headers[i];
                        }

                        int row = 2;
                        foreach (var employee in employees)
                        {
                            worksheet.Cell(row, 1).Value = employee.EmployeeId;
                            worksheet.Cell(row, 2).Value = employee.FirstName;
                            worksheet.Cell(row, 3).Value = employee.LastName;
                            worksheet.Cell(row, 4).Value = employee.Position;
                            worksheet.Cell(row, 5).Value = employee.Phone;
                            worksheet.Cell(row, 6).Value = employee.Email;
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