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
//using System.Windows.Shapes;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Management;

namespace Cliver.ZendeskClient
{
    public partial class TicketWindow : Window
    {
        public TicketWindow()
        {
            InitializeComponent();

            Icon = AssemblyRoutines.GetAppIconImageSource();

            screenshot_file = SystemInfo.GetScreenshotFile();

            HttpClientHandler handler = new HttpClientHandler();
            handler.Credentials = new System.Net.NetworkCredential(Settings.General.ZendeskUser, Settings.General.ZendeskPassword);
            http_client = new HttpClient(handler);

            Closing += delegate(object sender, System.ComponentModel.CancelEventArgs e)
            {
                if (ok.IsEnabled)
                    return;
                if (Message.YesNo("Posting the ticket is in progress. Do you want to cancel it?"))
                {
                    //if (create_ticket_t != null && create_ticket_t.IsAlive)
                    //    create_ticket_t.Abort();
                    http_client.CancelPendingRequests();
                }
                e.Cancel = true;
            };

            Closed += delegate {
                http_client.CancelPendingRequests();
            };
        }

        readonly string screenshot_file;
        readonly HttpClient http_client;

        void submit(object sender, EventArgs e)
        {
            try
            {
                //if (string.IsNullOrWhiteSpace(subject.Text))
                //    throw new Exception("Subject is empty.");
                if (string.IsNullOrWhiteSpace(description.Text))
                    throw new Exception("Description is empty.");
                List<string> files = new List<string>();
                if (include_screenshot.IsChecked == true)
                    files.Add(screenshot_file);
                foreach (AttachmentControl ac in attachments.Children)
                    files.Add(ac.File);
                //create_ticket_t = ThreadRoutines.StartTry(() => {
                create_ticket(Environment.UserName, Settings.General.UserEmail, "Request from support app", description.Text, files);
                //});
            }
            catch (Exception ex)
            {
                Message.Exclaim(ex.Message);
            }
        }
        //System.Threading.Thread create_ticket_t = null;

        /*
         https://sandboxed.zendesk.com
yugonian@gmail.com
UpW0rk17
             */

        static string userPrincipalEmail = null;

        async private void create_ticket(string user, string user_email, string subject, string description, List<string> files)
        {
            if (!ok.IsEnabled)
                return;
            ok.IsEnabled = false;

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

                List<string> ps = new List<string>();
                foreach (SystemInfo.ProcessorInfo p in SystemInfo.GetProcessorInfo())
                    ps.Add(p.procName);
                long hdd_total = 0;
                long hdd_free = 0;
                foreach (SystemInfo.DiskInfo h in SystemInfo.GetDiskInfo().Values)
                {
                    hdd_total += h.total;
                    hdd_free += h.free;
                }
                string hostname = Dns.GetHostName();
                string system_info = "hostname: <a href='https://support.bomgar.com/api/client_script?type=rep&operation=generate&action=start_pinned_client_session&search_string=" + hostname + "'>" + hostname + "</a>" +
                    "<br>currentuser: " + Environment.UserName + //System.DirectoryServices.AccountManagement.UserPrincipal.Current.Name
                    "<br>os: " + SystemInfo.GetWindowsVersion() +
                    "<br>os uptime: " + SystemInfo.GetUpTime().ToString() +
                    "<br>cpu: " + string.Join("\r\ncpu:", ps) +
                    "<br>mem: " + SystemInfo.GetTotalPhysicalMemory() +
                    "<br>hdd:" +
                    "<br>total: " + hdd_total +
                    "<br>free: " + hdd_free +
                    //hs.Add("total: " + h.total + "\r\nfree: " + h.free); string.Join("\r\nhdd:\r\n", hs) +
                    "<br>ip: " + SystemInfo.GetLocalIp().ToString();
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
                            //body = ,
                            html_body = description + "<br><br>--------------<br>" + system_info,
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

                ok.IsEnabled = true;
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
            ok.IsEnabled = true;
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

        void add_attachment(object sender, EventArgs e)
        {
            Microsoft.Win32.OpenFileDialog d = new Microsoft.Win32.OpenFileDialog();
            if (d.ShowDialog() != true)
                return;
            foreach (AttachmentControl c in attachments.Children)
                if (c.File == d.FileName)
                    return;
            AttachmentControl ac = new AttachmentControl(d.FileName);
            attachments.Children.Add(ac);
        }
    }
}
