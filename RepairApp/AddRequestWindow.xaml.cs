using System.Windows;
using System;

namespace RepairApp
{
    public partial class AddRequestWindow : Window
    {
        private Entities _context;

        public AddRequestWindow(Entities context)
        {
            InitializeComponent();
            _context = context;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var newRequest = new Request
            {
                RequestDate = DateTime.Now,
                FaultType = FaultTypeText.Text,
                ProblemDescription = ProblemDescriptionText.Text,
                EquipmentID = 1, // Пример оборудования
                ClientID = 1, // Пример клиента
                StatusID = 1 // В ожидании
            };

            _context.Request.Add(newRequest);
            _context.SaveChanges();
            this.Close();
        }
    }
}
