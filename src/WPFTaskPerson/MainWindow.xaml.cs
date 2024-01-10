using System.Windows;
using WPFTaskPerson.ViewModals;

namespace WPFTaskPerson
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PersonViewModal viewModal;
        public MainWindow()
        {
            InitializeComponent();
            viewModal = new PersonViewModal();
            this.DataContext=viewModal; 
        }
    }
}