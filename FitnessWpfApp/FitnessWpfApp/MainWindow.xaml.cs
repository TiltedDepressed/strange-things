using FitnessWpfApp.Model;
using FitnessWpfApp.View.Windows;
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

namespace FitnessWpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Core db = new Core();
        public MainWindow()
        {
            InitializeComponent();
            
            DataGridRegistrarion.ItemsSource = db.context.CourseRegistration.ToList();


        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            CourseRegistration newRegistation = new CourseRegistration();
            db.context.CourseRegistration.Add(newRegistation);
            var win = new CourseRegistrationWindow(db.context,newRegistation);
            win.ShowDialog();
            ShowTable();
           
        }

        private void ShowTable()
        {
            DataGridRegistrarion.ItemsSource = db.context.CourseRegistration.ToList();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var row = DataGridRegistrarion.SelectedItem as CourseRegistration;

            if (row == null)
            {
                MessageBox.Show("Вы не выбрали ни одной строки");
                return;
            }
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить строку?", "Удаление", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                db.context.CourseRegistration.Remove(row);
                db.context.SaveChanges();
                ShowTable();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button btnEdit = sender as Button;
            var currentRegistation = btnEdit.DataContext as CourseRegistration;
            var win = new CourseRegistrationWindow(db.context,currentRegistation);
            win.ShowDialog();
        }

        private void DataGridRegistrarion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

     
    }
}
