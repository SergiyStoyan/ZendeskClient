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

namespace Cliver.ZendeskClient
{
    public partial class ScreenshotTabItemControl : TabItem
    {
        public ScreenshotTabItemControl(string header, string file)
        {
            InitializeComponent();

            Header = header;
            image.Source = new BitmapImage(new Uri(file));
        }
    }
}
