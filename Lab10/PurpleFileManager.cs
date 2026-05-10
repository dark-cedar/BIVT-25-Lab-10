using System.IO;

namespace Lab10.Purple
{
    public abstract class PurpleFileManager<T> : Lab10.MyFileManager, Lab10.ISerializer<T>
        where T : Lab9.Purple.Purple
    {
        public PurpleFileManager(string name) : base(name) {}

        public PurpleFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
            : base(name, folderPath, fileName, fileExtension) {}

        public abstract void Serialize(T obj);

        public abstract T Deserialize();

        public override void EditFile(string content)
        {
            File.WriteAllText(FullPath, content ?? string.Empty);
        }

        public override void ChangeFileExtension(string fileExtension)
        {
            string oldPath = FullPath;
            string oldContent = string.Empty;

            if (File.Exists(oldPath))
            {
                oldContent = File.ReadAllText(oldPath);
                File.Delete(oldPath);
            }

            ChangeFileFormat(fileExtension);

            if (oldContent != string.Empty)
            {
                File.WriteAllText(FullPath, oldContent);
            }
        }
    }
}