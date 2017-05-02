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
using System.Threading;

namespace Cliver.ZendeskClient
{
    public partial class TicketWindow : Window
    {
        public TicketWindow()
        {
            InitializeComponent();

            Icon = AssemblyRoutines.GetAppIconImageSource();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            //string temp_dir = Path.GetTempPath() + "\\" + ProgramRoutines.GetAppName();
            //DateTime delete_time = DateTime.Now.AddDays(-3);
            //foreach (FileInfo fi in (new DirectoryInfo(temp_dir)).GetFiles())
            //    if (fi.LastWriteTime < delete_time)
            //        try
            //        {
            //            fi.Delete();
            //        }
            //        catch { }
            screenshot_files = SystemInfo.GetScreenshotFiles(
                PathRoutines.CreateDirectory(Log.WorkDir) + "\\" + Dns.GetHostName() + "_" + Environment.UserName + "_" + DateTime.Now.ToString("yy-MM-dd-HH-mm-ss") + ".png",
                System.Drawing.Imaging.ImageFormat.Png
                );

            HttpClientHandler handler = new HttpClientHandler();
            handler.Credentials = new System.Net.NetworkCredential(Settings.General.ZendeskUser, Settings.General.ZendeskPassword);
            http_client = new HttpClient(handler);

            Closing += delegate (object sender, System.ComponentModel.CancelEventArgs e)
            {
                if (ok.IsEnabled)
                    return;
                if (Message.YesNo("Posting the ticket is in progress. Do you want to cancel it?"))
                {
                    create_ticket_t = null;
                    http_client.CancelPendingRequests();
                    Log.Main.Inform("Cancelling...");
                }
                e.Cancel = true;
            };

            Closed += delegate
            {
                http_client.CancelPendingRequests();
            };
        }

        readonly List<string> screenshot_files;
        readonly HttpClient http_client;
        
        void submit(object sender, EventArgs e)
        {
            try
            {
                //if (string.IsNullOrWhiteSpace(subject.Text))
                //    throw new Exception("Subject is empty.");
                if (string.IsNullOrWhiteSpace(this.description.Text))
                    throw new Exception("Description is empty.");
                List<string> files = new List<string>();
                //if (include_screenshot.IsChecked == true)
                files.AddRange(screenshot_files);
                foreach (AttachmentControl ac in attachments.Children)
                    files.Add(ac.File);

                if (!ok.IsEnabled)
                    return;
                ok.IsEnabled = false;
                Cursor = Cursors.Wait;
                string description = this.description.Text;
                create_ticket_t = ThreadRoutines.StartTry(
                    () =>
                    {
                        create_ticket(Environment.UserName, Settings.General.UserEmail, "Request from support app", description, files);
                    }
                );
            }
            catch (Exception ex)
            {
                Message.Exclaim(ex.Message);
            }
        }
        Thread create_ticket_t = null;

        /*
         https://sandboxed.zendesk.com
yugonian@gmail.com
UpW0rk17
             */

        static string userPrincipalEmail = null;

        async void create_ticket(string user, string user_email, string subject, string description, List<string> files)
        {
            try
            {
                Log.Main.Inform("Creating ticket.");

                if (create_ticket_t == null)
                    return;

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

                if (create_ticket_t == null)
                    return;

                List<string> file_tockens = new List<string>();
                foreach (string f in files)
                    file_tockens.Add(await upload_file(f));

                if (create_ticket_t == null)
                    return;

                List<string> ps = new List<string>();
                foreach (SystemInfo.ProcessorInfo p in SystemInfo.GetProcessorInfo())
                    ps.Add(p.procName);
                long hdd_total = 0;
                long hdd_free = 0;
                foreach (SystemInfo.DiskInfo h in SystemInfo.GetDiskInfo().Values)
                {
                    hdd_total += h.total;
                    hdd_free += h.free;

                    if (create_ticket_t == null)
                        return;
                }
                string hostname = Dns.GetHostName();
                List<string> sils = new List<string>();
                sils.Add("hostname: <a href='https://support.bomgar.com/api/client_script?type=rep&operation=generate&action=start_pinned_client_session&search_string=" + hostname + "'>" + hostname + "</a>");

                if (create_ticket_t == null)
                    return;

                sils.Add("currentuser: " + Environment.UserName); //System.DirectoryServices.AccountManagement.UserPrincipal.Current.Name

                if (create_ticket_t == null)
                    return;

                sils.Add("os: " + SystemInfo.GetWindowsVersion());

                if (create_ticket_t == null)
                    return;

                sils.Add("os uptime: " + SystemInfo.GetUpTime().ToString());

                if (create_ticket_t == null)
                    return;

                sils.Add("cpu: " + string.Join("\r\ncpu:", ps));

                if (create_ticket_t == null)
                    return;

                sils.Add("mem: " + SystemInfo.GetTotalPhysicalMemory());

                if (create_ticket_t == null)
                    return;

                sils.Add("hdd:");
                sils.Add("total: " + hdd_total);
                sils.Add("free: " + hdd_free);
                //sils.Add("total: " + h.total + "\r\nfree: " + h.free); string.Join("\r\nhdd:\r\n", hs) +);
                sils.Add("ip: " + SystemInfo.GetLocalIp().ToString());
                string system_info = string.Join("<br>", sils);
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

                if (create_ticket_t == null)
                    return;

                string json_string = SerializationRoutines.Json.Serialize(data);

                if (create_ticket_t == null)
                    return;

                Log.Main.Inform("Posting ticket: " + json_string);
                var post_data = new StringContent(json_string, Encoding.UTF8, "application/json");
                HttpResponseMessage rm = await http_client.PostAsync("https://" + Settings.General.ZendeskSubdomain + ".zendesk.com/api/v2/tickets.json", post_data);
                if (!rm.IsSuccessStatusCode)
                    throw new Exception("Could not create ticket: " + rm.ReasonPhrase);
                //if (rm.Content != null)
                //    var responseContent = await rm.Content.ReadAsStringAsync();

                this.Dispatcher.Invoke(() =>
                {
                    ok.IsEnabled = true;
                    Close();
                });
                LogMessage.Inform("The ticket was succesfully created.");
            }
            catch (System.Threading.Tasks.TaskCanceledException e)
            {
                if (create_ticket_t == null)
                    return;
                Log.Main.Warning(e);
            }
            catch (Exception e)
            {
                LogMessage.Error(e);
            }
            finally
            {
                ok.Dispatcher.Invoke(() => {
                    Cursor = Cursors.Arrow;
                    ok.IsEnabled = true;
                });
            }
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

        void add_attachment_clicked(object sender, EventArgs e)
        {
            error.Visibility = Visibility.Collapsed;
            Microsoft.Win32.OpenFileDialog d = new Microsoft.Win32.OpenFileDialog();
            if (d.ShowDialog() != true)
                return;
            add_attachment(d.FileName);
        }

        void add_attachment(string file)
        {
            foreach (AttachmentControl c in attachments.Children)
                if (c.File == file)
                {
                    //Message.Exclaim("File: " + file + " has already been attached.");
                    error2.Content = "File: " + PathRoutines.GetFileNameFromPath(file) + " has already been attached.";
                    error.Visibility = Visibility.Visible;
                    return;
                }
            FileInfo fi = new FileInfo(file);
            if (fi.Length < 1)
            {
                //Message.Exclaim("File: " + file + " is empty.");
                error2.Content = "File: " + PathRoutines.GetFileNameFromPath(file) + " is empty.";
                error.Visibility = Visibility.Visible;
                return;
            }
            error.Visibility = Visibility.Collapsed;
            AttachmentControl ac = new AttachmentControl(file);
            attachments.Children.Add(ac);
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            error.Visibility = Visibility.Collapsed;
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
                return;
            foreach (string file in (string[])e.Data.GetData(DataFormats.FileDrop))
                add_attachment(file);
        }

        private void show_screenshots(object sender, EventArgs e)
        {
            ThreadRoutines.StartTry(() =>
            {
                Cursor c = Cursors.Arrow;
                Dispatcher.Invoke(() => {
                    c = Cursor;
                    Cursor = Cursors.Wait;
                });
                Thread.Sleep(1000);
                Dispatcher.Invoke(() => { Cursor = c; });
            });
            
            //foreach (string f in )
                //   System.Diagnostics.Process.Start(f);
            ScreenshotWindow sw = new ScreenshotWindow(screenshot_files);
            sw.ShowDialog();
        }

        private void remove_screenshots_click(object sender, EventArgs e)
        {
            if (!Message.YesNo("Remove screenshots from the ticket?"))
                return;
            StackPanel ats = screenshot_header.Parent as StackPanel;
            ((StackPanel)screenshot_header.Parent).Children.Remove(screenshot_header);
            ((Grid)remove_screenshots.Parent).Children.Remove(remove_screenshots);
            screenshot_files.Clear();
        }
        
        private void Window_UserActivity(object sender, EventArgs e)
        {
            error.Visibility = Visibility.Collapsed;
            if (((RoutedEventArgs)e).Source is Button)//if collapse then the event cannot find the target button anymore (weird!)
                ok.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
    }
}
