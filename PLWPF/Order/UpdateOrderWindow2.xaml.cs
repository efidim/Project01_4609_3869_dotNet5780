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
using System.Net.Mail;
using System.ComponentModel;
using BE;
using BL;

namespace PLWPF.Order
{
    /// <summary>
    /// Interaction logic for UpdateOrderWindow2.xaml
    /// </summary>
    public partial class UpdateOrderWindow2 : Window
    {
        BE.Order ord;
        IBL bl;
        BackgroundWorker worker;
        
        public UpdateOrderWindow2(BE.Order order)
        {
            InitializeComponent();
            ord = order;
            this.DataContext = ord;
            bl = FactoryBl.getBl();
            this.StatusComboBox.ItemsSource = Enum.GetValues(typeof(Enums.OrderStatus));

            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;            
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            MailMessage mail = new MailMessage();            
            mail.To.Add(bl.GetRequest(ord.GuestRequestKey).MailAddress);            
            mail.From = new MailAddress("Tsimerim@gmail.com");
            mail.Subject = "!הצעה לחופשה הבאה שלך";
            mail.Body = "mailBody";
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential("Tsimerim@gmail.com", "myGmailPassword");
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (StatusComboBox.SelectedItem.ToString() == "טרם_טופל")
                    ord.Status = 0;
                else if (StatusComboBox.SelectedItem.ToString() == "נשלח_מייל")
                    ord.Status = 1;
                else if (StatusComboBox.SelectedItem.ToString() == "נסגרה_מחוסר_הענות_של_הלקוח")
                    ord.Status = 2;
                else if (StatusComboBox.SelectedItem.ToString() == "נסגרה_כי_פג_תוקף")
                    ord.Status = 3;

                bl.UpdateOrder(ord);
                MessageBox.Show("The Order has been successfully updated");
                new OrderWindow().Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           

        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            new UpdateOrderWindow().Show();
            this.Close();
        }

    }

    public class IntToStatus : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((int)value == 0)
                return "טרם טופל";
            else if ((int)value == 1)
                return "נשלח מייל";
            else if ((int)value == 2)
                return "נסגרה מחוסר הענות של הלקוח";
            else if ((int)value == 3)
                return "נסגרה כי פג תוקף";
            return "טרם טופל";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
