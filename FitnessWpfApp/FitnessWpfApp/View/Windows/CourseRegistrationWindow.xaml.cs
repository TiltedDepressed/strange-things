using FitnessWpfApp.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
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

namespace FitnessWpfApp.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для CourseRegistrationWindow.xaml
    /// </summary>
    public partial class CourseRegistrationWindow : Window

    {
        Core db = new Core();
         
        public CourseRegistrationWindow(FitnessEntities context,CourseRegistration currentRegistation)
        {
            InitializeComponent();

            this.db.context = context;

            TrainerComboBox.ItemsSource = context.Trainer.ToList();

            CourseComboBox.ItemsSource = context.course.ToList();

            this.DataContext = currentRegistation;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckRegistation()) {
                db.context.SaveChanges();
            MessageBox.Show("Данные сохраннены");
            this.Close();
                
            }
        }
        private bool CheckRegistation()
        {
            var reg = this.DataContext as CourseRegistration;
            string message = "";
            if(reg.Trainer == null)
            {
                message += "Тренер не выбран\n";
            }
            if(reg.course == null)
            {
                message += "Курс не выбран\n";
            }
            if (reg.createdDate == null)
            {
                message += "Дата не выбрана\n";
            }
            if(!int.TryParse(TxtTotalPoint.Text, out int result))
            {
                message += "Не внесен набранный бал\n";
            }
            if (message == "")
                return true;
           
            else
            {
                MessageBox.Show(message);
                return false;
            }
            
          
        }

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPEG FILES(*.jpeg)|*.jpeg|PNG Files(*.png)|*.png|JPG Files(*.jpg)|*.jpg|GIF Files(*.gif)|*.gif|JFIF FILES(*.jfif)|*.jfif";
            bool result = Convert.ToBoolean(ofd.ShowDialog());
            if (result)
            {
                string path = ofd.FileName;
                byte[] image = File.ReadAllBytes(path);
                var fileSize = image.Length;
                if((fileSize / 1032.0) > 150)
                {
                    MessageBox.Show("Большой размер");
                }
                else
                {
                    var reg = this.DataContext as CourseRegistration;
                    reg.Certificateid = image;
                }
            }
        }
    }
}
