
using Biblioteca.WPF.Models;
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
           
        }
        BookModel bookToEdit = new BookModel();
        public void EditBook(object s, RoutedEventArgs e)
        {
            try
            {
                bookToEdit = (s as FrameworkElement).DataContext as BookModel;
                UpdateBookGrid.DataContext = bookToEdit;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: '{ex}'");
            }
        }
    }
}
