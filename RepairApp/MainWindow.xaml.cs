using System.Linq;
using System.Windows;

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

        private void LoadRequests()
        {
            var requests = _context.Request
                .Include("Client")
                .Include("Equipment")
                .Include("RequestStatus")
                .Include("Worker")
                .ToList();

            RequestsGrid.ItemsSource = requests;
        }

        private void AddRequest_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddRequestWindow(_context);
            addWindow.ShowDialog();
            LoadRequests();
        }

        private void EditRequest_Click(object sender, RoutedEventArgs e)
        {
            if (RequestsGrid.SelectedItem is Request selectedRequest)
            {
                var editWindow = new EditRequestWindow(_context, selectedRequest);
                editWindow.ShowDialog();
                LoadRequests();
            }
            else
            {
                MessageBox.Show("Выберите заявку для редактирования.");
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadRequests();
        }
    }
}
