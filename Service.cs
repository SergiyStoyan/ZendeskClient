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

        static void set_reboot_notificator(bool on)
        {
            if (!on)
            {
                if (reboot_notifier_t != null && reboot_notifier_t.IsAlive)
                    reboot_notifier_t.Abort();
                return;
            }
            if (reboot_notifier_t != null && reboot_notifier_t.IsAlive)
                return;
            reboot_notifier_t = ThreadRoutines.StartTry(() =>
            {
                while (true)
                {
                    TimeSpan ut = GetUpTime();
                    if (ut < Settings.General.MaxUpTime)
                        Thread.Sleep(Settings.General.MaxUpTime - ut);
                    MessageForm mf = new MessageForm(
                        Application.ProductName,
                        System.Drawing.SystemIcons.Exclamation,
                        "It's time to reboot the system...",
                        new string[1] { "OK" },
                        0,
                        SysTray.This
                        );
                    mf.TopLevel = true;
                    mf.TopMost = true;
                    mf.ShowDialog();
                    //Message.Exclaim();
                    Thread.Sleep(1000);
                }
            });
        }
        static Thread reboot_notifier_t = null;

        static public  TimeSpan GetUpTime()
        {
            using (var uptime = new PerformanceCounter("System", "System Up Time"))
            {
                uptime.NextValue();       //Call this an extra time before reading its value
                return TimeSpan.FromSeconds(uptime.NextValue());
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
            if (Settings.General.TicketKey != System.Windows.Input.Key.None)
            {
                key_manager = new HotKeyManager();
                System.Windows.Input.ModifierKeys mks;
                if (Settings.General.TicketModifierKey1 != ModifierKeys.None)
                {
                    mks = Settings.General.TicketModifierKey1;
                    if (Settings.General.TicketModifierKey2 != ModifierKeys.None)
                        mks |= Settings.General.TicketModifierKey2;
                }
                else
                    mks = ModifierKeys.None;
                var hotKey = key_manager.Register(Settings.General.TicketKey, mks);
                key_manager.KeyPressed += delegate (object sender, KeyPressedEventArgs e)
                {
                    create_ticket();
                };
            }
        }
        static HotKeyManager key_manager = null;

        static void create_ticket()
        {
            TicketForm tf = new TicketForm();
            tf.Show();
        }
    }
}