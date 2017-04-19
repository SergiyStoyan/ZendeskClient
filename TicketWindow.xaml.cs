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
    public partial class TicketWindow : Window
    {
        public TicketWindow()
        {
            InitializeComponent();

            Icon = AssemblyRoutines.GetAppIconImageSource();
        }

        void add_attachment(object sender, EventArgs e)
        {
            Microsoft.Win32.OpenFileDialog d = new Microsoft.Win32.OpenFileDialog();
            if (d.ShowDialog() != true)
                return;
            foreach (AttachmentControl2 c in attachments.Children)
                if (c.File == d.FileName)
                    return;
            AttachmentControl2 ac = new AttachmentControl2(d.FileName);
            attachments.Children.Add(ac);
        }

        void submit(object sender, EventArgs e)
        {

        }

        void include_screenshot(object sender, EventArgs e)
        {

        }
    }
}
