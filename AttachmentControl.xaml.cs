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
    /// <summary>
    /// Interaction logic for AttachmentControl.xaml
    /// </summary>
    public partial class AttachmentControl : UserControl
    {
        public AttachmentControl(string file)
        {
            InitializeComponent();

            this.file.Text = file;
            this.file.CaretIndex = this.file.Text.Length;
        }

        public string File
        {
            get
            {
                return file.Text;
            }
        }

        private void delete(object sender, EventArgs e)
        {
            StackPanel ats = this.Parent as StackPanel;
            ats.Children.Remove(this);
        }
    }
}
