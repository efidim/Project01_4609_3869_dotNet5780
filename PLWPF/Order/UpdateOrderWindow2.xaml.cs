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
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);

            ord = order;
            this.DataContext = ord;
            bl = FactoryBl.getBl();
            this.StatusComboBox.ItemsSource = Enum.GetValues(typeof(Enums.OrderStatus));

            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        }

        private bool quit = false;
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(bl.GetRequest(ord.GuestRequestKey).MailAddress);
            mail.From = new MailAddress(bl.GetFromConfig("MailAddress"));
            mail.Subject = "!הצעה לחופשה הבאה שלך";
            mail.Body = "mailBody";
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential(bl.GetFromConfig("MailAddress"), bl.GetFromConfig("MailPassword"));
            smtp.EnableSsl = true;

            while (!quit)
            {
                try
                {
                    smtp.Send(mail);
                    quit = true;                  
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    System.Threading.Thread.Sleep(2000);
                    worker.RunWorkerAsync();
                }
            }

        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                MessageBox.Show("!המייל נשלח בהצלחה");
            }

            else
            {
                MessageBox.Show("שליחת המייל נכשלה");
                ord.Status = 0;
                bl.UpdateOrder(ord);
            }


        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (StatusComboBox.SelectedItem.ToString() == "טרם_טופל")
                {
                    ord.Status = 0;
                    bl.UpdateOrder(ord);
                    MessageBox.Show("ההזמנה עודכנה בהצלחה");
                }

                else if (StatusComboBox.SelectedItem.ToString() == "נשלח_מייל")
                {
                    ord.Status = 1;
                    worker.RunWorkerAsync();
                    bl.UpdateOrder(ord);
                    MessageBox.Show("סטטוס ההזמנה יעודכן ברגע שיישלח המייל");                   
                }


                else if (StatusComboBox.SelectedItem.ToString() == "נסגרה_מחוסר_הענות_של_הלקוח")
                {
                    ord.Status = 2;
                    bl.UpdateOrder(ord);
                    MessageBox.Show("ההזמנה עודכנה בהצלחה");
                }

                else if (StatusComboBox.SelectedItem.ToString() == "נסגרה_בהצלחה")
                {
                    ord.Status = 3;
                    bl.UpdateOrder(ord);
                    MessageBox.Show("ההזמנה עודכנה בהצלחה");
                }

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
                return "נסגרה בהצלחה";
            else if ((int)value == 4)
                return "נסגרה כי פג תוקף";
            return "טרם טופל";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
