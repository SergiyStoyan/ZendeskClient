using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliver.ZendeskClient
{
    public partial class SettingsForm : BaseForm// Form// 
    {
        public SettingsForm()
        {
            InitializeComponent();

            FormClosed += delegate
              {
                  sf = null;
              };
            
            //Encoding.DisplayMember = "Text";
            //Encoding.ValueMember = "CodePage";
            //List<EncodingItem> its = new List<EncodingItem>();
            //foreach (EncodingInfo ei in System.Text.Encoding.GetEncodings())
            //    its.Add(new EncodingItem { Text = ei.Name, CodePage = ei.GetEncoding().CodePage });
            //Encoding.Items.AddRange(its.ToArray());
            //EncodingItem si = its.Where(i => i.CodePage == Settings.General.EncodingCodePage).First();
            //if (si != null)
            //    Encoding.SelectedIndex = its.IndexOf(si);

            foreach (System.Windows.Input.Key k in Enum.GetValues(typeof(System.Windows.Input.Key)))
                TicketKey.Items.Add(k);
            TicketKey.SelectedItem = Settings.General.TicketKey;

            foreach (System.Windows.Input.ModifierKeys k in Enum.GetValues(typeof(System.Windows.Input.ModifierKeys)))
                TicketModifierKey1.Items.Add(k);
            TicketModifierKey1.SelectedItem = Settings.General.TicketModifierKey1;

            foreach (System.Windows.Input.ModifierKeys k in Enum.GetValues(typeof(System.Windows.Input.ModifierKeys)))
                TicketModifierKey2.Items.Add(k);
            TicketModifierKey2.SelectedItem = Settings.General.TicketModifierKey2;
        }

        //public class EncodingItem
        //{
        //    public string Text { get; set; }
        //    public int CodePage { get; set; }
        //}

        static public void Open()
        {
            if (sf == null)
                sf = new SettingsForm();
            sf.Show();
            sf.Activate();
        }
        static SettingsForm sf = null;

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            try
            {
                System.Text.RegularExpressions.RegexOptions ros = System.Text.RegularExpressions.RegexOptions.None;
                if (DumpRegexIgnoreCase.Checked)
                    ros |= System.Text.RegularExpressions.RegexOptions.IgnoreCase;
                if (DumpRegexSingleLine.Checked)
                    ros |= System.Text.RegularExpressions.RegexOptions.Singleline;
                //Settings.General.EncodingCodePage = ((EncodingItem)Encoding.SelectedItem).CodePage;
                Settings.General.TicketKey = (System.Windows.Input.Key)TicketKey.SelectedItem;
                Settings.General.TicketModifierKey1 = (System.Windows.Input.ModifierKeys)TicketModifierKey1.SelectedItem;
                Settings.General.TicketModifierKey2 = (System.Windows.Input.ModifierKeys)TicketModifierKey2.SelectedItem;
                Settings.General.Save();

                Close();

                bool running = Service.Running;
                Service.Running = false;
                Service.Running = running;
            }
            catch (Exception ex)
            {
                Message.Exclaim(ex.Message);
            }
        }
    }
}