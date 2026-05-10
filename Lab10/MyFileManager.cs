using System.IO;

namespace Lab10
{
    public interface IFileManager
    {
        string FolderPath { get; }
        string FileName { get; }
        string FileExtension { get; }
        string FullPath { get; }

        void SelectFolder(string folderPath);
        void ChangeFileName(string fileName);
        void ChangeFileFormat(string fileExtension);
    }

    public interface IFileLifeController
    {
        void CreateFile();
        void DeleteFile();
        void EditFile(string content);
        void ChangeFileExtension(string fileExtension);
    }

    public abstract class MyFileManager : IFileManager, IFileLifeController
    {
        private string _name;
        private string _folderPath;
        private string _fileName;
        private string _fileExtension;

        public MyFileManager(string name)
        {
            _name = name ?? string.Empty;
            _folderPath = string.Empty;
            _fileName = string.Empty;
            _fileExtension = "txt";
        }

        public MyFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
        {
            _name = name ?? string.Empty;
            _folderPath = folderPath ?? string.Empty;
            _fileName = fileName ?? string.Empty;
            _fileExtension = fileExtension ?? "txt";
        }

        public string Name => _name;
        public string FolderPath => _folderPath;
        public string FileName => _fileName;
        public string FileExtension => _fileExtension;

        public string FullPath => _folderPath + "\\" + _fileName + "." + _fileExtension;

        public void SelectFolder(string folderPath)
        {
            _folderPath = folderPath ?? string.Empty;
        }

        public void ChangeFileName(string fileName)
        {
            _fileName = fileName ?? string.Empty;
        }

        public void ChangeFileFormat(string fileExtension)
        {
            _fileExtension = fileExtension ?? string.Empty;
            
            if (!File.Exists(FullPath)) CreateFile();
        }

        public void CreateFile()
        {
            if (!File.Exists(FullPath)) File.Create(FullPath).Close();
        }

        public void DeleteFile()
        {
            if (File.Exists(FullPath)) File.Delete(FullPath);
        }

        public virtual void EditFile(string content)
        {
            File.WriteAllText(FullPath, content ?? string.Empty);
        }

        public virtual void ChangeFileExtension(string fileExtension)
        {
            string oldPath = FullPath;
            string content = string.Empty;

            if (File.Exists(oldPath))
            {
                content = File.ReadAllText(oldPath);
                File.Delete(oldPath);
            }

            _fileExtension = fileExtension ?? string.Empty;

            if (content != string.Empty)
            {
                CreateFile();
                File.WriteAllText(FullPath, content);
            }
        }
    }
}