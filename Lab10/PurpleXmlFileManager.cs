using System;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

namespace Lab10.Purple
{
    public class PurpleXmlFileManager<T> : PurpleFileManager<T>
        where T : Lab9.Purple.Purple
    {
        public PurpleXmlFileManager(string name) : base(name)
        {
            ChangeFileFormat("xml");
        }

        public PurpleXmlFileManager(string name, string folderPath, string fileName, string fileExtension = "xml")
            : base(name, folderPath, fileName, fileExtension)
        {
            ChangeFileFormat("xml");
        }

        public override void Serialize(T obj)
        {
            if (obj == null) return;
            if (FullPath == null || FullPath == string.Empty) return;

            DTOPurple dto = new DTOPurple();

            dto.Type = obj.GetType().AssemblyQualifiedName;
            dto.Input = obj.Input;
            dto.Object = JsonSerializer.Serialize(obj, obj.GetType());
            dto.Codes = GetCodesJson(obj);

            XmlSerializer serializer = new XmlSerializer(typeof(DTOPurple));

            using (FileStream stream = new FileStream(FullPath, FileMode.Create))
            {
                serializer.Serialize(stream, dto);
            }
        }

        public override T Deserialize()
        {
            if (FullPath == null || FullPath == string.Empty) return null;
            if (!File.Exists(FullPath)) return null;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(DTOPurple));

                DTOPurple dto;

                using (FileStream stream = new FileStream(FullPath, FileMode.Open))
                {
                    dto = serializer.Deserialize(stream) as DTOPurple;
                }

                if (dto == null) return null;

                return CreateTask(dto.Type, dto.Input, dto.Object, dto.Codes);
            }
            catch
            {
                return null;
            }
        }

        private string GetCodesJson(T obj)
        {
            if (obj == null) return string.Empty;

            Type type = obj.GetType();

            if (!type.Name.Contains("Task4")) return string.Empty;

            var fields = type.GetFields(
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Public
            );

            for (int i = 0; i < fields.Length; i++)
            {
                object value = fields[i].GetValue(obj);

                if (value is (string pair, char code)[] codes)
                {
                    DTOCode[] dtoCodes = new DTOCode[codes.Length];

                    for (int j = 0; j < codes.Length; j++)
                    {
                        dtoCodes[j] = new DTOCode(codes[j].pair, codes[j].code);
                    }

                    return JsonSerializer.Serialize(dtoCodes);
                }
            }

            return string.Empty;
        }

        private T CreateTask(string typeName, string input, string objectJson, string codesJson)
        {
            if (typeName == null || typeName == string.Empty) return null;
            if (input == null) input = string.Empty;

            Lab9.Purple.Purple task = null;

            if (typeName.Contains("Task1"))
            {
                task = new Lab9.Purple.Task1(input);
            }
            else if (typeName.Contains("Task2"))
            {
                task = new Lab9.Purple.Task2(input);
            }
            else if (typeName.Contains("Task3"))
            {
                task = new Lab9.Purple.Task3(input);
            }
            else if (typeName.Contains("Task4"))
            {
                DTOCode[] dtoCodes = JsonSerializer.Deserialize<DTOCode[]>(codesJson);

                if (dtoCodes == null) return null;

                (string pair, char code)[] codes = new (string pair, char code)[dtoCodes.Length];

                for (int i = 0; i < dtoCodes.Length; i++)
                {
                    codes[i] = (dtoCodes[i].Pair, dtoCodes[i].Code);
                }

                task = new Lab9.Purple.Task4(input, codes);
            }

            if (task == null) return null;

            task.Review();

            return task as T;
        }

        public override void EditFile(string content)
        {
            if (FullPath == null || FullPath == string.Empty) return;

            T obj = Deserialize();

            if (obj == null) return;

            obj.ChangeText(content);

            Serialize(obj);
        }

        public override void ChangeFileExtension(string fileExtension)
        {
            if (FullPath == null || FullPath == string.Empty) return;

            T obj = Deserialize();

            ChangeFileFormat("xml");

            if (obj != null)
            {
                Serialize(obj);
            }
        }
    }
}