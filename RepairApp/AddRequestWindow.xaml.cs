using System;
using System.Linq;
using System.Windows;
using RepairApp;

namespace RepairApp
{
    public partial class AddRequestWindow : Window
    {
        private Entities _context;

        public AddRequestWindow()
        {
            InitializeComponent();
            _context = new Entities();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var clients = _context.Client.ToList();
                var equipment = _context.Equipment.ToList();
                var statuses = _context.RequestStatus.ToList();
                var workers = _context.Worker.ToList();

                // Привязываем данные
                ClientComboBox.ItemsSource = clients;
                EquipmentComboBox.ItemsSource = equipment;
                StatusComboBox.ItemsSource = statuses;
                WorkerComboBox.ItemsSource = workers;

                // Устанавливаем индекс по умолчанию
                ClientComboBox.SelectedIndex = 0;
                EquipmentComboBox.SelectedIndex = 0;
                StatusComboBox.SelectedIndex = 0;
                WorkerComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }


        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var newRequest = new Request
            {
                RequestDate = DateTime.Now,
                ClientID = (ClientComboBox.SelectedItem as Client)?.ClientID ?? 0,
                EquipmentID = (EquipmentComboBox.SelectedItem as Equipment)?.EquipmentID ?? 0,
                StatusID = (StatusComboBox.SelectedItem as RequestStatus)?.StatusID ?? 0,
                FaultType = FaultTypeText.Text,
                ProblemDescription = ProblemDescriptionText.Text,
                WorkerID = null 
            };

            _context.Request.Add(newRequest);
            _context.SaveChanges();
            MessageBox.Show("Запрос добавлен!");
            Close();
        }
    }
}
