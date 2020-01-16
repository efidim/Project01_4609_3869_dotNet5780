﻿using PLWPF.Order;
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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        public OrderWindow()
        {
            InitializeComponent();
        }

        private void addUnitButton_Click(object sender, RoutedEventArgs e)
        {
            new CreateOrderWindow().Show();
            //Hide();
        }

        private void existedUnitButton_Click(object sender, RoutedEventArgs e)
        {
            new UpdateOrderWindow().Show();
            //Hide();
        }
    }
}
