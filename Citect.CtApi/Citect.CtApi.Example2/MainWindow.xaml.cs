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

namespace Citect.CtApi.Example2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var ctapi = new CtApi())
                {
                    tbCtApi.Text = "Connexion...";
                    await ctapi.OpenAsync();
                }
            }
            catch (Exception error)
            {
                tbCtApi.Text = error.Message;
            }
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var ctapi = new CtApi())
                {
                    tbCtApi.Text = "Connexion...";
                    ctapi.Open();
                }
            }
            catch (Exception error)
            {
                tbCtApi.Text = error.Message;
            }
        }

        private async void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var ctapi = new CtApi())
                {
                    tbCtApi.Text = "Connexion...";
                    await ctapi.OpenAsync();
                    tbCtApi.Text = "Login...";
                    tbCtApi.Text = await ctapi.CicodeAsync("LoginForm()");
                }
            }
            catch (Exception error)
            {
                tbCtApi.Text = error.Message;
            }
        }

        private async void button_Copy2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var ctapi = new CtApi())
                {
                    tbCtApi.Text = "Connexion...";
                    await ctapi.OpenAsync();
                    tbCtApi.Text = "Find...";
                    var alarms = await ctapi.FindAsync("Alarm", "", "", "NAME", "DESC");
                    var tags = await ctapi.FindAsync("Tag", "", "", "TAG");

                    var text = new StringBuilder();
                    text.Append("ALARMES\n");
                    alarms.ToList().ForEach(x => text.Append($"NAME={x["NAME"]} - DESC={x["DESC"]}\n"));
                    text.Append("\n\n\n\n\nTAGS\n");
                    tags.ToList().ForEach(x => text.Append($"TAG={x["TAG"]}\n"));

                    tbCtApi.Text = text.ToString();
                }
            }
            catch (Exception error)
            {
                tbCtApi.Text = error.Message;
            }
        }
    }
}