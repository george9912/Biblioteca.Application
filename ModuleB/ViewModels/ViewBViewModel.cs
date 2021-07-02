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

        public ViewBViewModel()
        {
            var bookRestService = new BookRestService();
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
        public DelegateCommand UpdateBookCommand
        {
            get
            {
                return new DelegateCommand(UpdateBookAction);
            }
        }

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

        //BookModel properties - since it doesn t recognise them from the BookModel I had to create them also here
        private string title;
        private string author;
        private string publisher;
        private int year;

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (title != value)
                {
                    title = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string Author
        {
            get
            {
                return author;
            }
            set
            {
                if (author != value)
                {
                    author = value;
                    RaisePropertyChanged();
                }
            }
        }
        public string Publisher
        {
            get
            {
                return publisher;
            }
            set
            {
                if (publisher != value)
                {
                    publisher = value;
                    RaisePropertyChanged();
                }
            }
        }
        public int Year
        {
            get
            {
                return year;
            }
            set
            {
                if (year != value)
                {
                    year = value;
                    RaisePropertyChanged();
                }
            }
        }

        //Add method
        public async void AddBookAction()
        {
            try 
            {
                BookModel newBookModel = new BookModel()
                {
                    Title = Title,
                    Author = Author,
                    Publisher = Publisher,
                    Year = Year
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

        //Update method
        BookModel bookToEdit = new BookModel();
        public async void UpdateBookAction()
        {
            try
            {
                var bookRestService = new BookRestService();
                await bookRestService.UpdateBook(bookToEdit.Id, bookToEdit);
                bookToEdit = null;
                GetBooksAction();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: '{ex}'");
            }
        }

        //Delete method
        BookModel bookToDelete = new BookModel();
        public async void DeleteBookAction()
        {
            try
            {
                var bookRestService = new BookRestService();
                await bookRestService.DeleteBook(bookToDelete.Id);
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
