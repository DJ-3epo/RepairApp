using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RepairApp
{
    public partial class EditRequestWindow : Window
    {
        private Entities _context;
        private Request _request;

        public EditRequestWindow(Entities context, Request request)
        {
            InitializeComponent();
            _context = context;
            _request = request;
        }

        private void SaveEdit_Click(object sender, RoutedEventArgs e)
        {
            _request.FaultType = EditFaultTypeText.Text;
            _context.SaveChanges();
            this.Close();
        }
    }
}
