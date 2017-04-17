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
using System.Management;

namespace Cliver.ZendeskClient
{
    public partial class TicketForm : BaseForm// Form//  
    {
        public TicketForm()
        {
            InitializeComponent();

            //AutoScaleMode = AutoScaleMode.Dpi;

            subject.Items.AddRange(Settings.General.Subjects);

            screenshot_file = get_screenshot_file();

            HttpClientHandler handler = new HttpClientHandler();
            handler.Credentials = new System.Net.NetworkCredential(Settings.General.ZendeskUser, Settings.General.ZendeskPassword);
            http_client = new HttpClient(handler);

            FormClosed += delegate
            {
                http_client.CancelPendingRequests();
            };

            FormClosing += TicketForm_FormClosing;
        }

        private void TicketForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ok.Enabled)
                return;
            if (Message.YesNo("Posting the ticket is in progress. Do you want to cancel it?"))
            {
                //if (create_ticket_t != null && create_ticket_t.IsAlive)
                //    create_ticket_t.Abort();
                http_client.CancelPendingRequests();
            }
            e.Cancel = true;
        }

        readonly string screenshot_file;
        readonly HttpClient http_client;

        string get_screenshot_file()
        {
            string temp_dir = PathRoutines.CreateDirectory(Path.GetTempPath() + "\\" + ProgramRoutines.GetAppName());
            DateTime delete_time = DateTime.Now.AddDays(-3);
            foreach (FileInfo fi in (new DirectoryInfo(temp_dir)).GetFiles())
                if (fi.LastWriteTime < delete_time)
                    try
                    {
                        fi.Delete();
                    }
                    catch { }
            string file = temp_dir + "\\screenshot_" + DateTime.Now.ToString("yy-MM-dd-HH-mm-ss") + ".jpg";
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
                List<string> files = new List<string>();
                if (IncludeScreenshot.Checked)
                    files.Add(screenshot_file);
                foreach (AttachmentControl ac in attachments.Controls)
                    files.Add(ac.File);
                //create_ticket_t = ThreadRoutines.StartTry(() => {
                    create_ticket(Environment.UserName, Settings.General.UserEmail, subject.Text, description.Text, files);
                //});
            }
            catch (Exception ex)
            {
                Message.Exclaim(ex.Message);
            }
        }
        //System.Threading.Thread create_ticket_t = null;

        private dynamic get_windows_info()
        {
            return new
            {
                hostname = Dns.GetHostName(),
                currentuser = Environment.UserName,
                //currentuser = System.DirectoryServices.AccountManagement.UserPrincipal.Current.Name,
                os = Environment.OSVersion,
                os_uptime = Service.GetUpTime(),
                cpu = get_processor_info(),
                mem = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory,
                hdd = get_disk_info(),
                ip = get_local_ip().ToString(),
            };
        }

        static List<dynamic> get_processor_info()
        {
            List<dynamic> pis = new List<dynamic>();
            using (ManagementObjectSearcher win32Proc = new ManagementObjectSearcher("select * from Win32_Processor")
                //win32CompSys = new ManagementObjectSearcher("select * from Win32_ComputerSystem"),
                //win32Memory = new ManagementObjectSearcher("select * from Win32_PhysicalMemory")
                )
            {
                foreach (ManagementObject mo in win32Proc.Get())
                {
                    pis.Add(new
                    {
                        clockSpeed = mo["CurrentClockSpeed"].ToString(),
                        procName = mo["Name"].ToString(),
                        manufacturer = mo["Manufacturer"].ToString(),
                        version = mo["Version"].ToString(),
                    });
                }
            }
            return pis;
        }

        static Dictionary<string, dynamic> get_disk_info()
        {
            Dictionary<string, dynamic> dis = new Dictionary<string, dynamic>();
            foreach (DriveInfo d in DriveInfo.GetDrives())
            {
                if (!d.IsReady)
                    continue;
                dis[d.Name] = new { total = d.TotalSize, free = d.TotalFreeSpace };
            }
            return dis;
        }

        //string disk_size2string(int size)
        //{
        //    foreach(uint m in new uint[] {2^30, 2^20, 2^10, 1})
        //    string s = 
        //}

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


        static string userPrincipalEmail = null;

        async private void create_ticket(string user, string user_email, string subject, string description, List<string> files)
        {
            if (!ok.Enabled)
                return;
            ok.Enabled = false;

            try
            {
                Log.Main.Inform("Creating ticket.");

                if (string.IsNullOrWhiteSpace(user_email))
                {
                    if (userPrincipalEmail == null)
                    {//consumes a long time 
                        userPrincipalEmail = System.DirectoryServices.AccountManagement.UserPrincipal.Current.EmailAddress;
                        if (userPrincipalEmail == null)
                            userPrincipalEmail = string.Empty;
                    }
                    if (userPrincipalEmail != string.Empty)
                        user_email = userPrincipalEmail;
                }
                
                List<string> file_tockens = new List<string>();
                foreach (string f in files)
                    file_tockens.Add(await upload_file(f));

                var si = get_windows_info();
                List<string> ps = new List<string>();
                foreach (var p in si.cpu)
                    ps.Add(p.procName);
                uint hdd_total = 0;
                uint hdd_free = 0;
                foreach (var h in si.hdd.Values)
                {
                    hdd_total += h.total;
                    hdd_free += h.free;
                }
                string system_info = "url: " + "https://support.bomgar.com/api/client_script?type=rep&operation=generate&action=start_pinned_client_session&search_string=" + si.hostname +
                    "\r\nhostname: " + si.hostname +
                    "\r\ncurrentuser: " + si.currentuser +
                    "\r\nos: " + si.os.VersionString +
                    "\r\nos uptime: " + si.os_uptime.ToString() +
                    "\r\ncpu: " + string.Join("\r\ncpu:", ps) +
                    "\r\nmem: " + si.mem +
                    "\r\nhdd:" +
                    "\r\ntotal: " + hdd_total +
                    "\r\nfree: " + hdd_free +
                    //hs.Add("total: " + h.total + "\r\nfree: " + h.free); string.Join("\r\nhdd:\r\n", hs) +
                    "\r\nip: " + si.ip;
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
                        comment = new
                        {
                            body = description + "\r\n\r\n--------------\r\n" + system_info,
                            uploads = file_tockens,
                        },
                        //custom_fields = system_info,
                    }
                };
                string json_string = SerializationRoutines.Json.Serialize(data);
                Log.Main.Inform("Posting ticket: " + json_string);
                var post_data = new StringContent(json_string, Encoding.UTF8, "application/json");
                HttpResponseMessage rm = await http_client.PostAsync("https://" + Settings.General.ZendeskSubdomain + ".zendesk.com/api/v2/tickets.json", post_data);
                if (!rm.IsSuccessStatusCode)
                    throw new Exception("Could not create ticket: " + rm.ReasonPhrase);
                //if (rm.Content != null)
                //    var responseContent = await rm.Content.ReadAsStringAsync();

                ok.Enabled = true;
                Close();
                LogMessage.Inform("The ticket was succesfully created.");
            }
            catch (System.Threading.Tasks.TaskCanceledException e)
            {
                Log.Main.Warning(e);
            }
            catch (Exception e)
            {
                LogMessage.Error(e);
            }
            ok.Enabled = true;
        }

        async private Task<string> upload_file(string file)
        {
            Log.Main.Inform("Posting file: " + file);
            ByteArrayContent post_data = new ByteArrayContent(File.ReadAllBytes(file));
            post_data.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/binary");
            HttpResponseMessage rm = await http_client.PostAsync("https://" + Settings.General.ZendeskSubdomain + ".zendesk.com/api/v2/uploads.json?filename=" + PathRoutines.GetFileNameFromPath(file), post_data);
            if (!rm.IsSuccessStatusCode)
                throw new Exception("Could not upload file: " + rm.ReasonPhrase);
            if (rm.Content == null)
                throw new Exception("Response content is null.");
            string c = await rm.Content.ReadAsStringAsync();
            dynamic json = SerializationRoutines.Json.Deserialize<dynamic>(c);
            return (string)(((dynamic)json)["upload"])["token"];
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void add_attachment_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            if (d.ShowDialog() != DialogResult.OK)
                return;
            foreach (AttachmentControl c in attachments.Controls)
                if (c.File == d.FileName)
                    return;
            AttachmentControl ac = new AttachmentControl(d.FileName);
            attachments.Controls.Add(ac);
        }
    }
}