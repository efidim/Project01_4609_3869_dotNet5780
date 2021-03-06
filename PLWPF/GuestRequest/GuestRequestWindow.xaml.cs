﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for GuestRequestWindow.xaml
    /// </summary>
    public partial class GuestRequestWindow : Window
    {
        BE.GuestRequest guest;
        BL.IBL bl;
        private System.Windows.Controls.Calendar MyCalendar;

        public GuestRequestWindow()
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);

            guest = new BE.GuestRequest();
            this.GuestRequestGrid.DataContext = guest;
            bl = BL.FactoryBl.getBl();
            this.DataContext = guest;

            this.areaComboBox.ItemsSource =Enum.GetValues(typeof(Enums.Area));
            this.Type.ItemsSource = Enum.GetValues(typeof(Enums.HostingUnitType));
            this.poolComboBox.ItemsSource = Enum.GetValues(typeof(Enums.Response));
            this.jacuzziComboBox.ItemsSource = Enum.GetValues(typeof(Enums.Response));
            this.attractionComboBox.ItemsSource = Enum.GetValues(typeof(Enums.Response));

            MyCalendar = CreateCalendar();
            vbCalendar.Child = null;
            vbCalendar.Child = MyCalendar;                                 
        }

        private System.Windows.Controls.Calendar CreateCalendar()
        {
            System.Windows.Controls.Calendar MonthlyCalendar = new System.Windows.Controls.Calendar();
            MonthlyCalendar.Name = "MonthlyCalendar";
            MonthlyCalendar.DisplayMode = CalendarMode.Month;
            MonthlyCalendar.SelectionMode = CalendarSelectionMode.SingleRange;
            MonthlyCalendar.IsTodayHighlighted = true;
            return MonthlyCalendar;
        }
        private void addCurrentList(List<DateTime> tList)
        {
            guest.EntryDate = tList.First();
            guest.ReleaseDate = tList.Last();
        }


        private void sentDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<DateTime> myList = MyCalendar.SelectedDates.ToList();
                MyCalendar = CreateCalendar();
                vbCalendar.Child = null;
                vbCalendar.Child = MyCalendar;
                addCurrentList(myList);

                if (poolComboBox.SelectedItem.ToString() == "אפשרי")
                    guest.Pool = 0;
                else if (poolComboBox.SelectedItem.ToString() == "לא_מעוניין")
                    guest.Pool = 1;
                else if (poolComboBox.SelectedItem.ToString() == "הכרחי")
                    guest.Pool = 2;

                if (jacuzziComboBox.SelectedItem.ToString() == "אפשרי")
                    guest.Jacuzzi = 0;
                else if (jacuzziComboBox.SelectedItem.ToString() == "לא_מעוניין")
                    guest.Jacuzzi = 1;
                else if (jacuzziComboBox.SelectedItem.ToString() == "הכרחי")
                    guest.Jacuzzi = 2;

                if (attractionComboBox.SelectedItem.ToString() == "אפשרי")
                    guest.ChildrenAttractions = 0;
                else if (attractionComboBox.SelectedItem.ToString() == "לא_מעוניין")
                    guest.ChildrenAttractions = 1;
                else if (attractionComboBox.SelectedItem.ToString() == "הכרחי")
                    guest.ChildrenAttractions = 2;

                guest.RegistrationDate = DateTime.Now;
                guest.Status = true;

                int value;
                try
                {
                    value = int.Parse(this.Adults.Text);
                    if (value <= 0)
                        throw new Exception(" נא הגדר מספר תקין עבור המבוגרים");
                    value = int.Parse(this.Children.Text);
                    if (value < 0)
                        throw new Exception(" נא הגדר מספר תקין עבור הילדים");
                    string str = this.PrivateName.Text;
                    CheckStr(str);
                    string str1 = this.FamilyName.Text;
                    CheckStr(str1);
                    string mail = this.Mail.Text;
                    CheckMail(mail);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(" הקלט לא תקין-"+ ex.Message);
                    return;
                }
                


                bl.AddGuestRequest(guest);
                MessageBox.Show("The Guest Request has been successfully added.");

                new MainWindow().Show();
                this.Close();
            }

            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
        }

        ///// <summary>
        ///// A function that ensures that the first click will be updated on dates
        ///// </summary>
        ///// <param name="e">Mouse click event</param>
        //protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        //{
        //    base.OnPreviewMouseUp(e);
        //    if (Mouse.Captured is System.Windows.Controls.Calendar || Mouse.Captured is System.Windows.Controls.Primitives.CalendarItem)
        //    {
        //        Mouse.Capture(null);
        //    }
        //}

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
        private void CheckStr(string str)
        {
            bool hasNUmber = str.Any(char.IsDigit);
            if (hasNUmber)
            {
                throw new Exception("  יש להכניס אותיות בלבד בשדות השם");
            }
        }
        private void CheckMail(string str)
        {
            if (!(Regex.IsMatch(str, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")))
                throw new KeyNotFoundException("המייל שהוזן אינו תקין");
        }
    }

   

}


