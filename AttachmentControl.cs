using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliver.ZendeskClient
{
    public partial class AttachmentControl : UserControl
    {
        public AttachmentControl(string file)
        {
            InitializeComponent();

            this.Dock = DockStyle.Top;

            this.file.Text = file;
        }

        public string File
        {
            get
            {
                return file.Text;
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            Parent.Controls.Remove(this);
        }
    }
}
