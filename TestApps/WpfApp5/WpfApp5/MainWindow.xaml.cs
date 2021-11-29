using Nevron;
using Nevron.GraphicsCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;

using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp5
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            string NevronEnterpriseLicence = "16d40096-ce6f-b504-1802-5cd60201e59e";
            NLicense license = new NLicense(NevronEnterpriseLicence);
            NLicenseManager.Instance.SetLicense(license);
            NLicenseManager.Instance.LockLicense = true;

            NAdvancedGradientFillStyle gradient = new NAdvancedGradientFillStyle
            {
                BackgroundColor = Color.White
            };
            gradient.Points.Add(new NAdvancedGradientPoint(Color.Red, 14, 16, 0, 70, AGPointShape.Circle));
            gradient.Points.Add(new NAdvancedGradientPoint(Color.Yellow, 84, 36, 25, 70, AGPointShape.Rectangle));
            gradient.Points.Add(new NAdvancedGradientPoint(Color.Magenta, 60, 89, 0, 50, AGPointShape.Line));

            chart.Charts[0].BackgroundFillStyle = gradient;
        }
    }
}
