using System.IO;

namespace SearchFolder.Model
{
    public class FileModel
    {
        public FileModel(string path)
        {
            Name = Path.GetFileName(path);
            FilePath = path;
        }

        public string Name { get; set; }
        public string FilePath { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
