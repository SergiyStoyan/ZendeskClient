using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Script.Serialization;

namespace Cliver.ZendeskClient
{
    public partial class Settings
    {
        [Cliver.Settings.Obligatory]
        public static readonly GeneralSettings General;

        public class GeneralSettings : Cliver.Settings
        {
            public string[] Subjects = new string[] { "test", "test2" };
            public string ZendeskSubdomain = "sandboxed";
            public string ZendeskUser = "yugonian@gmail.com";
            public string ZendeskPassword = "UpW0rk17";
            public TimeSpan MaxUpTime = new TimeSpan(3 * 24, 0, 0);
            public System.Windows.Input.Key TicketKey = System.Windows.Input.Key.F8;
            public System.Windows.Input.ModifierKeys TicketModifierKey1 = System.Windows.Input.ModifierKeys.None;
            public System.Windows.Input.ModifierKeys TicketModifierKey2 = System.Windows.Input.ModifierKeys.None;

            //[Newtonsoft.Json.JsonIgnore]
            //public System.Text.Encoding Encoding = System.Text.Encoding.Unicode;

            //public override void Loaded()
            //{
            //    Encoding = System.Text.Encoding.GetEncoding(EncodingCodePage);
            //}

            //public override void Saving()
            //{
            //    EncodingCodePage = Encoding.CodePage;
            //}
        }
    }
}