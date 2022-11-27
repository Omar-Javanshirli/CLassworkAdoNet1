using CLassworkAdoNet1.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CLassworkAdoNet1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            App.IdTexBox = idTb;
            App.FirstnameTextBox = firstnameTb;
            App.LastnameTextBox= lastnameTb;
            App.MyDataGrid = myDataGrid;
            var vm = new AppViewModel();
            this.DataContext = vm;
        }
    }
}
