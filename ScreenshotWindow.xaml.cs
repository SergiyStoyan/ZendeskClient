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
using System.Text.RegularExpressions;

namespace Cliver.ZendeskClient
{
    /// <summary>
    /// Interaction logic for ScreenshotWindow.xaml
    /// </summary>
    public partial class ScreenshotWindow : Window
    {
        public ScreenshotWindow(List<string> screenshot_files)
        {
            InitializeComponent();

            int i = 0;
            foreach (string f in screenshot_files)
            {
                ScreenshotTabItemControl stic;
                if (i++ == 0)
                    stic = new ScreenshotTabItemControl("Primary", f);
                else
                {
                    string fn = PathRoutines.GetFileNameWithoutExtentionFromPath(f);
                    Match m = Regex.Match(fn, @"(?:[^_]*_+){3}(.*)");
                    if (!m.Success || string.IsNullOrWhiteSpace(m.Groups[1].Value))
                        stic = new ScreenshotTabItemControl(i.ToString(), f);
                    else
                        stic = new ScreenshotTabItemControl(m.Groups[1].Value, f);
                }
                tabs.Items.Add(stic);
            }
        }
    }
}