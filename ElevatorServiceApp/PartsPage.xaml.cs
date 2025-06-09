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
    public partial class PartsPage : Page
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string ApiBaseUrl = "https://localhost:7258/api/Parts";
        private Part _editingPart = null;
        private List<Part> _allParts = new List<Part>();

        public PartsPage()
        {
            InitializeComponent();
            LoadParts();
        }

        private async void LoadParts()
        {
            try
            {
                PartsGrid.IsEnabled = false;
                var response = await _httpClient.GetStringAsync(ApiBaseUrl);
                _allParts = JsonConvert.DeserializeObject<List<Part>>(response);
                ApplyFiltersAndSearch();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
            finally
            {
                PartsGrid.IsEnabled = true;
            }
        }

        private void ApplyFiltersAndSearch()
        {
            IEnumerable<Part> filteredParts = _allParts;

            var searchText = SearchTextBox.Text?.Trim().ToLower();
            if (!string.IsNullOrEmpty(searchText))
            {
                filteredParts = filteredParts.Where(p =>
                    (!string.IsNullOrEmpty(p.PartName) && p.PartName.ToLower().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(p.PartNumber) && p.PartNumber.ToLower().Contains(searchText)));
            }

            if (int.TryParse(MinQuantityTextBox.Text, out int minQuantity) && minQuantity >= 0)
            {
                filteredParts = filteredParts.Where(p => p.QuantityInStock >= minQuantity);
            }

            // Фильтр по диапазону цен
            decimal? minPrice = null;
            decimal? maxPrice = null;

            if (decimal.TryParse(MinPriceTextBox.Text, out decimal minPriceValue) && minPriceValue >= 0)
            {
                minPrice = minPriceValue;
            }

            if (decimal.TryParse(MaxPriceTextBox.Text, out decimal maxPriceValue) && maxPriceValue >= 0)
            {
                maxPrice = maxPriceValue;
            }

            if (minPrice.HasValue)
            {
                filteredParts = filteredParts.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                filteredParts = filteredParts.Where(p => p.Price <= maxPrice.Value);
            }

            PartsGrid.ItemsSource = filteredParts.ToList();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFiltersAndSearch();
        }

        private void MinQuantityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFiltersAndSearch();
        }

        private void MinPriceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFiltersAndSearch();
        }

        private void MaxPriceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFiltersAndSearch();
        }

        private void AddPart_Click(object sender, RoutedEventArgs e)
        {
            _editingPart = null;
            DialogTitle.Text = "Новая запчасть";
            PartNameTextBox.Text = "";
            PartNumberTextBox.Text = "";
            QuantityInStockTextBox.Text = "";
            PriceTextBox.Text = "";
            PartDialog.IsOpen = true;
        }

        private void EditPart_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Part part)
            {
                _editingPart = part;
                DialogTitle.Text = "Редактировать запчасть";
                PartNameTextBox.Text = part.PartName;
                PartNumberTextBox.Text = part.PartNumber;
                QuantityInStockTextBox.Text = part.QuantityInStock.ToString();
                PriceTextBox.Text = part.Price.ToString();
                PartDialog.IsOpen = true;
            }
        }

        private async void SavePart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(PartNameTextBox.Text) || string.IsNullOrWhiteSpace(PartNumberTextBox.Text))
                {
                    MessageBox.Show("Введите название и номер детали.");
                    return;
                }

                if (!int.TryParse(QuantityInStockTextBox.Text, out int quantityInStock) || quantityInStock < 0)
                {
                    MessageBox.Show("Количество на складе должно быть положительным числом.");
                    return;
                }

                if (!decimal.TryParse(PriceTextBox.Text, out decimal price) || price < 0)
                {
                    MessageBox.Show("Цена должна быть положительным числом.");
                    return;
                }

                var part = new Part
                {
                    PartId = _editingPart?.PartId ?? 0,
                    PartName = PartNameTextBox.Text,
                    PartNumber = PartNumberTextBox.Text,
                    QuantityInStock = quantityInStock,
                    Price = price
                };

                HttpResponseMessage response;
                if (_editingPart == null)
                {
                    var content = new StringContent(JsonConvert.SerializeObject(part), Encoding.UTF8, "application/json");
                    response = await _httpClient.PostAsync(ApiBaseUrl, content);
                }
                else
                {
                    var content = new StringContent(JsonConvert.SerializeObject(part), Encoding.UTF8, "application/json");
                    response = await _httpClient.PutAsync($"{ApiBaseUrl}/{part.PartId}", content);
                }

                if (response.IsSuccessStatusCode)
                {
                    PartDialog.IsOpen = false;
                    LoadParts();
                    MessageBox.Show(_editingPart == null ? "Запчасть добавлена." : "Запчасть обновлена.");
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

        private async void DeletePart_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Part part)
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить эту запчасть?", "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        var response = await _httpClient.DeleteAsync($"{ApiBaseUrl}/{part.PartId}");
                        if (response.IsSuccessStatusCode)
                        {
                            LoadParts();
                            MessageBox.Show("Запчасть удалена.");
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
            LoadParts();
        }

        private void ExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var parts = PartsGrid.ItemsSource as IEnumerable<Part>;
                if (parts == null || !parts.Any())
                {
                    MessageBox.Show("Нет данных для экспорта.");
                    return;
                }

                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    DefaultExt = "xlsx",
                    FileName = $"Parts_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Parts");
                        var headers = new[] { "ID", "Название", "Номер детали", "Количество на складе", "Цена" };
                        for (int i = 0; i < headers.Length; i++)
                        {
                            worksheet.Cell(1, i + 1).Value = headers[i];
                        }

                        int row = 2;
                        foreach (var part in parts)
                        {
                            worksheet.Cell(row, 1).Value = part.PartId;
                            worksheet.Cell(row, 2).Value = part.PartName;
                            worksheet.Cell(row, 3).Value = part.PartNumber;
                            worksheet.Cell(row, 4).Value = part.QuantityInStock;
                            worksheet.Cell(row, 5).Value = part.Price;
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