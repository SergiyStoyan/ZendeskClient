//********************************************************************************************
//Author: Sergey Stoyan, CliverSoft.com
//        http://cliversoft.com
//        stoyan@cliversoft.com
//        sergey.stoyan@gmail.com
//        27 February 2007
//Copyright: (C) 2007, Sergey Stoyan
//********************************************************************************************

using System;
using System.Linq;
using System.Net;
using System.Text;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Diagnostics;
using Cliver;
using System.Configuration;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Windows.Input;
using GlobalHotKey;

namespace Cliver.ZendeskClient
{
    public class Service
    {
        public delegate void OnStateChanged();
        public static event OnStateChanged StateChanged = null;

        public static bool Running
        {
            set
            {
                set_hot_keys(value);
                StateChanged?.Invoke();
            }
            get
            {
                return key_manager != null;
            }
        }

        static void set_hot_keys(bool listen)
        {
            if (key_manager != null)
            {
                key_manager.Dispose();
                key_manager = null;
            }
            if (!listen)
                return;
            if (Settings.General.TerminatingKey != System.Windows.Input.Key.None)
            {
                key_manager = new HotKeyManager();
                System.Windows.Input.ModifierKeys mks;
                if (Settings.General.TerminatingModifierKey1 != ModifierKeys.None)
                {
                    mks = Settings.General.TerminatingModifierKey1;
                    if (Settings.General.TerminatingModifierKey2 != ModifierKeys.None)
                        mks |= Settings.General.TerminatingModifierKey2;
                }
                else
                    mks = ModifierKeys.None;
                var hotKey = key_manager.Register(Settings.General.TerminatingKey, mks);
                key_manager.KeyPressed += delegate (object sender, KeyPressedEventArgs e)
                {
                    if (!Message.YesNo("Do you want to terminate " + ProgramRoutines.GetAppName() + "?"))
                        return;
                    Log.Main.Exit2("Keys pressed.");
                };
            }
        }
        static HotKeyManager key_manager = null;
    }
}