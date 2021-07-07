using Biblioteca.WPF.API.Client;
using Biblioteca.WPF.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace ModuleB.ViewModels
{
    public class ViewBViewModel : BindableBase
    {
        private ObservableCollection<BookModel> booksModel = new ObservableCollection<BookModel>();

        private BookModel bookPropToUpdate;
        public BookModel BookPropToUpdate
        {
            get
            {
                return bookPropToUpdate;
            }
            set
            {
                if(value!= bookPropToUpdate)
                {
                    bookPropToUpdate = value;
                    RaisePropertyChanged();
                }
            }
        }
        private BookModel bookPropToAdd;
        public BookModel BookPropToAdd
        {
            get
            {
                return bookPropToAdd;
            }
            set
            {
                if (value != bookPropToAdd)
                {
                    bookPropToAdd = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ViewBViewModel()
        {
            bookPropToUpdate = new BookModel();
            bookPropToAdd = new BookModel();
        }

        public DelegateCommand GetBooks
        {
            get
            {
                return new DelegateCommand(GetBooksAction);
            }
        }

        public DelegateCommand AddBook
        {
            get
            {
                return new DelegateCommand(AddBookAction);
            }
        }
        private DelegateCommand<BookModel> updateBookCommand;
        public DelegateCommand<BookModel> UpdateBookCommand =>
            updateBookCommand ?? (updateBookCommand = new DelegateCommand<BookModel>(UpdateBookAction));



        private DelegateCommand<BookModel> editBookCommand;
        public DelegateCommand<BookModel> EditBookCommand =>
            editBookCommand ?? (editBookCommand = new DelegateCommand<BookModel>(EditBookAction));

        private DelegateCommand<BookModel> deleteBookCommand;
        public DelegateCommand<BookModel> DeleteBookCommand =>
            deleteBookCommand ?? (deleteBookCommand = new DelegateCommand<BookModel>(DeleteBookAction));

        public ObservableCollection<BookModel> BooksModel
        {
            get
            {
                return booksModel;
            }
            set
            {
                if (value != booksModel)
                {
                    booksModel = value;
                    OnPropertyChanged();
                }
            }
        }

        public BookModel SelectedBookGrid
        {
            get
            {
                return selectedBookGrid;
            }
            set
            {
                if (value != selectedBookGrid)
                {
                    selectedBookGrid = value;
                    OnPropertyChanged();
                }
            }
        }
        public async void GetBooksAction()
        {
            try
            {
                BooksModel.Clear();
                var bookRestService = new BookRestService();
                var books = await bookRestService.GetBooks();
                foreach (var book in books)
                {
                    BooksModel.Add(book);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: '{ex}'");
            }
        }

        //Add method
        public async void AddBookAction()
        {
            try 
            {
                BookModel newBookModel = new BookModel()
                {
                    Title = BookPropToAdd.Title,
                    Author = BookPropToAdd.Author,
                    Publisher = BookPropToAdd.Publisher,
                    Year = BookPropToAdd.Year
                };

                var bookRestService = new BookRestService();
                await bookRestService.CreateBook(newBookModel);
                newBookModel = null;
                GetBooksAction();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: '{ex}'");
            }
        }

        //Update method(+EDIT)

        public async void UpdateBookAction(BookModel SelectedBookGrid2)
        {
            try
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var bookRestService = new BookRestService();
                    await bookRestService.UpdateBook(BookPropToUpdate.Id, BookPropToUpdate);
                    GetBooksAction();
                }                        
                else
                {
                    System.Windows.MessageBox.Show("Update operation terminated");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: '{ex}'");
            }
        }
        public void EditBookAction(BookModel SelectedBookGrid)
        {
            try
            {
                bookPropToUpdate.Id = SelectedBookGrid.Id;
                bookPropToUpdate.Title = SelectedBookGrid.Title;
                bookPropToUpdate.Author = SelectedBookGrid.Author;
                bookPropToUpdate.Publisher = SelectedBookGrid.Publisher;
                bookPropToUpdate.Year = SelectedBookGrid.Year;           
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: '{ex}'");
            }
        }

        //Delete method
        //BookModel bookToDelete = new BookModel();
        private BookModel selectedBookGrid;

        public async void DeleteBookAction(BookModel bookToDelete)
        {       
            try
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var bookRestService = new BookRestService();
                    await bookRestService.DeleteBook(bookToDelete.Id);
                    GetBooksAction();
                }
                else
                {
                    System.Windows.MessageBox.Show("Delete operation terminated");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: '{ex}'");
            }

        }

        //PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
