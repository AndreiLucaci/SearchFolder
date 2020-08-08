using SearchFolder.Extensions;
using SearchFolder.Model;
using SearchFolder.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace SearchFolder.ViewModels
{
    public class SearcherViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<FileModel> FilteredFiles { get; set; } = new ObservableCollection<FileModel>();

        public SearcherViewModel(ISerachFolderService searchFolder)
        {
            SearchFolderService = searchFolder;
            SetFilteredFiles(SearchFolderService.FilesInFolder);
        }

        public void Search(string part)
        {
            var files = this.SearchFolderService.Search(part);
            SetFilteredFiles(files);
        }

        private void SetFilteredFiles(IEnumerable<FileModel> files)
        {
            FilteredFiles = new ObservableCollection<FileModel>(files);
            NotifyPropertyChanged(nameof(FilteredFiles));
        }

        public ISerachFolderService SearchFolderService { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void Reset()
        {
            SearchFolderService.RefreshFiles();
            SetFilteredFiles(SearchFolderService.FilesInFolder);
        }

        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        internal void Execute(FileModel fileModel)
        {
            var fileopener = new Process();
            fileopener.StartInfo.FileName = "explorer";
            fileopener.StartInfo.Arguments = "\"" + fileModel.FilePath + "\"";
            fileopener.Start();
        }
    }
}
