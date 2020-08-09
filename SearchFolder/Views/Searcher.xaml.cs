using SearchFolder.Model;
using SearchFolder.Service;
using SearchFolder.ViewModels;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace SearchFolder.Views
{
    /// <summary>
    /// Interaction logic for Searcher.xaml
    /// </summary>
    public partial class Searcher : UserControl, INotifyPropertyChanged
    {
        public SearcherViewModel ViewModel { get; set; }

        public Searcher()
        {
            InitializeComponent();

            ViewModel = new SearcherViewModel(new SerachFolderService());
            NotifyPropertyChanged(nameof(ViewModel));

            Loaded += (a, b) =>
            {
                searchBox.Focus();
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    ViewModel.Reset();
                    list.SelectedIndex = -1;
                }
                else
                {
                    ViewModel.Search(textBox.Text);
                    if (ViewModel.FilteredFiles.Any())
                    {
                        list.SelectedIndex = 0;
                    }
                }
            }
        }

        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return && list.SelectedItem is FileModel fileModel)
            {
                ViewModel.Execute(fileModel);
            }
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up && ViewModel.FilteredFiles.Any())
            {
                var idx = list.SelectedIndex < 0 ? 0 : list.SelectedIndex == 0 ? ViewModel.FilteredFiles.Count - 1 : list.SelectedIndex - 1;
                list.SelectedIndex = idx;
            }

            if (e.Key == Key.Down && ViewModel.FilteredFiles.Any())
            {
                var idx = list.SelectedIndex < 0 ? 0 : list.SelectedIndex == ViewModel.FilteredFiles.Count - 1 ? 0 : list.SelectedIndex + 1;
                list.SelectedIndex = idx;
            }
        }

        private void List_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (list.SelectedItem is FileModel fileModel)
            {
                ViewModel.Execute(fileModel);
            }
        }
    }
}
