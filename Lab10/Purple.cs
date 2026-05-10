using System;
using System.IO;

namespace Lab10.Purple
{
    public class Purple<T> where T : Lab9.Purple.Purple
    {
        private T[] _tasks;
        private PurpleFileManager<T> _manager;

        public T[] Tasks => _tasks;
        public PurpleFileManager<T> Manager => _manager;

        public Purple(T[] tasks = null)
        {
            CopyTasks(tasks);
            _manager = null;
        }

        public Purple(PurpleFileManager<T> manager, T[] tasks = null)
        {
            CopyTasks(tasks);
            _manager = manager;
        }

        public Purple(T[] tasks, PurpleFileManager<T> manager)
        {
            CopyTasks(tasks);
            _manager = manager;
        }

        private void CopyTasks(T[] tasks)
        {
            if (tasks == null)
            {
                _tasks = new T[0];
                return;
            }

            _tasks = new T[tasks.Length];

            for (int i = 0; i < tasks.Length; i++) _tasks[i] = tasks[i];
        }

        public void Add(T task)
        {
            if (task == null) return;

            Array.Resize(ref _tasks, _tasks.Length + 1);
            _tasks[_tasks.Length - 1] = task;
        }

        public void Add(T[] tasks)
        {
            if (tasks == null) return;

            for (int i = 0; i < tasks.Length; i++) Add(tasks[i]);
        }

        public void Remove(T task)
        {
            if (task == null) return;

            T[] array = new T[0];

            for (int i = 0; i < _tasks.Length; i++)
            {
                if (_tasks[i] != task)
                {
                    Array.Resize(ref array, array.Length + 1);
                    array[array.Length - 1] = _tasks[i];
                }
            }

            _tasks = array;
        }

        public void Clear()
        {
            _tasks = new T[0];

            if (Directory.Exists(_manager.FolderPath)) Directory.Delete(_manager.FolderPath, true);
        }

        public void SaveTasks()
        {
            if (_tasks == null || _manager == null) return;

            for (int i = 0; i < _tasks.Length; i++)
            {
                if (_tasks[i] == null) continue;

                _manager.ChangeFileName(i.ToString());
                _manager.Serialize(_tasks[i]);
            }
        }

        public void LoadTasks()
        {
            if (_tasks == null || _manager == null) return;

            for (int i = 0; i < _tasks.Length; i++)
            {
                _manager.ChangeFileName(i.ToString());
                _tasks[i] = _manager.Deserialize();
            }
        }

        public void ChangeManager(PurpleFileManager<T> manager)
        {
            if (manager == null) return;

            _manager = manager; 

            if (!Directory.Exists(_manager.Name)) Directory.CreateDirectory(_manager.Name);

            _manager.SelectFolder(_manager.Name);
        }
    }
}