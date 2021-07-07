
using Biblioteca.WPF.Models;
using ModuleB.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ModuleB.Views
{
    /// <summary>
    /// Interaction logic for ViewB
    /// </summary>
    public partial class ViewB : UserControl
    {
        public ViewB()
        {
            InitializeComponent();
            DataContext = new ViewBViewModel();
        }

    
    }
}
