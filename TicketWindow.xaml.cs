﻿using System;
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

namespace Cliver.ZendeskClient
{
    public partial class TicketWindow : Window
    {
        public TicketWindow()
        {
            InitializeComponent();

            Icon = AssemblyRoutines.GetAppIconImageSource();
        }

        void add_attachment(object sender, RequestNavigateEventArgs e)
        {

        }

        void submit(object sender, object e)
        {

        }
    }
}