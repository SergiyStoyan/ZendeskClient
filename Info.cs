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
    class Info
    {
      public static string GetScreenshotFile()
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

        public static dynamic GetWindowsInfo()
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
    }
}
