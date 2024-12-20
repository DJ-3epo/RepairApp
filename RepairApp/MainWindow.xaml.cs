using System;
using System.Linq;
using System.Windows;
using RepairApp;
using System.Data.Entity;

namespace RepairApp
{
    public partial class MainWindow : Window
    {
        private Entities _context;

        public MainWindow()
        {
            InitializeComponent();
            _context = new Entities();
            LoadRequests();
        }

        // Метод для загрузки заявок
        private void LoadRequests()
        {
            try
            {
                var requests = _context.Request
                                      .Include(r => r.Client)
                                      .Include(r => r.Equipment)
                                      .Include(r => r.RequestStatus)
                                      .ToList();

                RequestsDataGrid.ItemsSource = requests;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        // Редактирование существующей заявки
        private void EditRequestButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRequest = RequestsDataGrid.SelectedItem as Request;
            if (selectedRequest != null)
            {
                var editWindow = new EditRequestWindow(selectedRequest.RequestID);
                bool? result = editWindow.ShowDialog();

                if (result == true)
                {
                    LoadRequests();
                }
            }
            else
            {
                MessageBox.Show("Выберите запрос для редактирования.");
            }
        }

        // Обновление списка запросов после добавления нового
        private void AddRequestButton_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddRequestWindow();
            addWindow.ShowDialog();
            LoadRequests();
        }

        private void ViewStatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            var statisticsWindow = new StatisticsWindow();
            statisticsWindow.ShowDialog();
        }

        

    }
}
