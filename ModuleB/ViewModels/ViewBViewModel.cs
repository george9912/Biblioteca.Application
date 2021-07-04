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
            //BookModel properties = new BookModel();
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

        //BookModel properties - since it doesn t recognise them from the BookModel I had to create them also here
        private string title;
        private string author;
        private string publisher;
        private int year;
        private Guid id;
        public Guid Id
        {
            get
            {
                return id;
            }
            set
            {
                if (id != value)
                {
                    id = value;
                    RaisePropertyChanged();
                }
            }
        }

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

        //Update method(+EDIT)

        public async void UpdateBookAction(BookModel SelectedBookGrid2)
        {
            try
            {
                
                SelectedBookGrid2 = new BookModel();

                SelectedBookGrid2.Title = Title;
                SelectedBookGrid2.Author = Author;
                SelectedBookGrid2.Publisher = Publisher;
                SelectedBookGrid2.Year = Year;
          

                var bookRestService = new BookRestService();
                await bookRestService.UpdateBook(SelectedBookGrid2.Id, SelectedBookGrid2);
                //bookToEdit = null;
                GetBooksAction();
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
                Title = SelectedBookGrid.Title;
                Author = SelectedBookGrid.Author;
                Publisher = SelectedBookGrid.Publisher;
                Year = SelectedBookGrid.Year;           
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
                var bookRestService = new BookRestService();
                await bookRestService.DeleteBook(bookToDelete.Id);
                GetBooksAction();
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
