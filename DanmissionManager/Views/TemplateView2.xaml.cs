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
using System.Windows.Shapes;

namespace DanmissionManager.Views
{
    /// <summary>
    /// Interaction logic for TemplateView2.xaml
    /// </summary>
    public partial class TemplateView2 : Window
    {
        public TemplateView2()
        {
            InitializeComponent();
            ConsoleManager.Show();
        }
    }
}