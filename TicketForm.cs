using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net;
using System.Diagnostics;
using System.IO;


namespace Cliver.ZendeskClient
{
    public partial class TicketForm : Form
    {
        public TicketForm()
        {
            InitializeComponent();

            subject.Items.AddRange(Settings.General.Subjects);

            screenshot_file = get_screenshot_file();
        }
        string screenshot_file;

        string get_screenshot_file()
        {
            string file = Path.GetTempPath() + "\\screenshot.jpg";
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }
                bitmap.Save(file, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            return file;
        }

        private void ok_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(subject.Text))
                    throw new Exception("Subject is empty.");
                if (string.IsNullOrWhiteSpace(description.Text))
                    throw new Exception("Description is empty.");
                send_ticket(Environment.UserName, null, subject.Text, description.Text, get_windows_info(), null);
                //Close();
            }
            catch (Exception ex)
            {
                Message.Exclaim(ex.Message);
            }
        }

        private object get_windows_info()
        {
            return new
            {
                hostname = Dns.GetHostName(),
                currentuser = Environment.UserName,
                os = Environment.OSVersion,
                os_uptime = Service.GetUpTime(),
                cpu = Environment.ProcessorCount,
                mem = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory,
                hdd = get_disk_info(),
                ip = get_local_ip(),
            };
        }

        private static string get_disk_info()
        {
            List<string> dis = new List<string>();
            foreach (DriveInfo d in DriveInfo.GetDrives())
            {
                if (!d.IsReady)
                    continue;
                dis.Add(d.Name + ": " + d.TotalFreeSpace + "/" + d.TotalSize);
            }
            return string.Join("\r\n", dis);
        }

        static IPAddress get_local_ip()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    return ip;
            }
            return null;
        }

        /*
         https://sandboxed.zendesk.com
yugonian@gmail.com
UpW0rk17
             */

        async private void send_ticket(string user, string user_email, string subject, string description, object windows_info, string[] files)
        {
            if (!ok.Enabled)
                return;
            ok.Enabled = false;

            try
            {
                var data = new
                {
                    ticket = new
                    {
                        requester = new
                        {
                            name = user,
                            email = user_email
                        },
                        subject = subject,
                        //description = description.Text,
                        comment = new
                        {
                            body = description,
                            //attachments =
                        },
                        custom_fields = windows_info,
                    }
                };
                string json_string = SerializationRoutines.Json.Serialize(data);
                Log.Main.Inform("Posting ticket: " + json_string);
                HttpClientHandler handler = new HttpClientHandler();
                handler.Credentials = new System.Net.NetworkCredential(Settings.General.ZendeskUser, Settings.General.ZendeskPassword);
                HttpClient hc = new HttpClient(handler);
                var post_data = new StringContent(json_string, Encoding.UTF8, "application/json");
                HttpResponseMessage rm = await hc.PostAsync("https://" + Settings.General.ZendeskSubdomain + ".zendesk.com/api/v2/tickets.json", post_data);
                ok.Enabled = true;
                if (!rm.IsSuccessStatusCode)
                {
                    Log.Main.Error2("Post: " + rm.ReasonPhrase);
                    Message.Error("Could not create the ticket: " + rm.ReasonPhrase);
                    return;
                }

                //if (rm.Content != null)
                //    var responseContent = await rm.Content.ReadAsStringAsync();

                Close();
            }
            catch (Exception e)
            {
                Log.Main.Error(e);
            }

            //          curl "https://{subdomain}.zendesk.com/api/v2/uploads.json?filename=myfile.dat&token={optional_token}" \
            //-v - u { email_address}:{ password} \
            //-H "Content-Type: application/binary" \
            //--data - binary @file.dat - X POST
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void add_attachment_Click(object sender, EventArgs e)
        {

        }
    }
}