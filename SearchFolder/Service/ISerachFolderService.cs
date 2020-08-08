using SearchFolder.Model;
using System.Collections.Generic;

namespace SearchFolder.Service
{
    public interface ISerachFolderService
    {
        IEnumerable<FileModel> Search(string file);

        void RefreshFiles();

        List<FileModel> FilesInFolder { get; }
    }
}
