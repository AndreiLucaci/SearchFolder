using SearchFolder.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SearchFolder.Service
{
    public class SerachFolderService : ISerachFolderService
    {
        private readonly string FolderPath = AppDomain.CurrentDomain.BaseDirectory;

        public List<FileModel> FilesInFolder { get; } = new List<FileModel>();

        public SerachFolderService()
        {
            InitFiles();
        }

        private void InitFiles()
        {
            FilesInFolder.Clear();
            var files = Directory.GetFiles(FolderPath);
            FilesInFolder.AddRange(files.Select(x => new FileModel(x)));
        }

        public IEnumerable<FileModel> Search(string file)
        {
            return FilesInFolder.Where(x => x.FilePath.IndexOf(file, StringComparison.InvariantCultureIgnoreCase) >= 0);
        }

        public void RefreshFiles()
        {
            InitFiles();
        }
    }
}
