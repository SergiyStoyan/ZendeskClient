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
                set_reboot_notificator(value);
                StateChanged?.Invoke();
            }
            get
            {
                return key_manager != null;
            }
        }

        static ManualResetEvent stop = new ManualResetEvent(false);

        static void set_reboot_notificator(bool on)
        {
            if (!on)
            {
                if (reboot_notifier_t != null && reboot_notifier_t.IsAlive)
                {
                    stop.Set();
                    if(!reboot_notifier_t.Join(1000))
                        reboot_notifier_t.Abort();
                }
                return;
            }
            if (reboot_notifier_t != null && reboot_notifier_t.IsAlive)
                return;
            stop.Reset();
            reboot_notifier_t = ThreadRoutines.StartTry(() =>
            {
                while (true)
                {
                    TimeSpan ut = SystemInfo.GetUpTime();
                    if (ut < Settings.General.MaxUpTime)
                    {
                        TimeSpan ts = Settings.General.MaxUpTime - ut;
                        try
                        {
                            if(stop.WaitOne(ts))
                                return;
                        }
                        catch(System.ArgumentOutOfRangeException e)
                        {
                            TimeSpan ts1 = new TimeSpan(0, 0, 100);
                            if (ts1 > ts)
                                throw new Exception("Settings.General.MaxUpTime - SystemInfo.GetUpTime() gave wrong number for timer: " + ts.TotalMilliseconds);
                            if (stop.WaitOne(100000))
                                return;
                            continue;
                        }
                    }
                    ControlRoutines.InvokeFromUiThread(() =>
                    {
                        //Message.ShowDialog(
                        //    Application.ProductName,
                        //    System.Drawing.SystemIcons.Exclamation,
                        //    "It's time to reboot the system...",
                        //    new string[1] { "OK" },
                        //    0,
                        //    SysTray.This,
                        //    null,
                        //    null,
                        //    true
                        //    );
                        Message.Exclaim("It's time to reboot the system...");
                    });
                    if (stop.WaitOne(1000))
                        return;
                }
            });
        }
        static Thread reboot_notifier_t = null;
        
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
                    CreateTicket();
                };
            }
        }
        static HotKeyManager key_manager = null;

        static public void CreateTicket()
        {
            //TicketForm tf = new TicketForm();
            //tf.Show();
            TicketWindow tw = new TicketWindow();
            System.Windows.Forms.Integration.ElementHost.EnableModelessKeyboardInterop(tw);
            tw.Show();
        }
    }
}