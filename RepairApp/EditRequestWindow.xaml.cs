using System;
using System.Linq;
using System.Windows;
using RepairApp;

namespace RepairApp
{
    public partial class EditRequestWindow : Window
    {
        private Entities _context;
        private Request _request;

        public EditRequestWindow(int requestId)
        {
            InitializeComponent();
            _context = new Entities();

            // Загружаем заявку по ID
            _request = _context.Request
                                .Include("Client")
                                .Include("Equipment")
                                .Include("RequestStatus")
                                .FirstOrDefault(r => r.RequestID == requestId);

            if (_request != null)
            {
                LoadData();
            }
            else
            {
                MessageBox.Show("Заявка не найдена.");
                Close();
            }
        }


        private void LoadData()
        {
            // Загружаем данные из базы
            ClientComboBox.ItemsSource = _context.Client.ToList();
            EquipmentComboBox.ItemsSource = _context.Equipment.ToList();
            StatusComboBox.ItemsSource = _context.RequestStatus.ToList();

            // Устанавливаем текущие значения для редактирования
            ClientComboBox.SelectedItem = _request.Client;
            EquipmentComboBox.SelectedItem = _request.Equipment;
            StatusComboBox.SelectedItem = _request.RequestStatus;
            FaultTypeTextBox.Text = _request.FaultType;
            ProblemDescriptionTextBox.Text = _request.ProblemDescription;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Сохраняем изменения в объекте заявки
            _request.ClientID = (ClientComboBox.SelectedItem as Client)?.ClientID ?? 0;
            _request.EquipmentID = (EquipmentComboBox.SelectedItem as Equipment)?.EquipmentID ?? 0;
            _request.StatusID = (StatusComboBox.SelectedItem as RequestStatus)?.StatusID ?? 0;
            _request.FaultType = FaultTypeTextBox.Text;
            _request.ProblemDescription = ProblemDescriptionTextBox.Text;

            // Обновляем данные в базе
            _context.SaveChanges();

            // Информируем пользователя
            MessageBox.Show("Запрос обновлен!");

            // Закрываем окно редактирования
            DialogResult = true; // Устанавливаем, что редактирование прошло успешно
            Close();
        }


    }
}
