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
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for UpdateUnitWindow2.xaml
    /// </summary>
    public partial class UpdateUnitWindow2 : Window
    {
        HostingUnit unit;
        IBL bl;
        public UpdateUnitWindow2(HostingUnit unit)
        {
            InitializeComponent();

            unit = new HostingUnit();
            bl = FactoryBl.getBl();




        }

     
    }

}
