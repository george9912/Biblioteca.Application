
using Biblioteca.WPF.Models;
using ModuleB.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

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
       
        //public void EditBook(object s, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        bookToEdit = (s as FrameworkElement).DataContext as BookModel;
        //        UpdateBookGrid.DataContext = bookToEdit;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error: '{ex}'");
        //    }
        //}
    }
}
