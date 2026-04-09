using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using TodoApp.Models;
using System.ComponentModel;
using System.Windows.Data;

namespace TodoApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                TasksView.Refresh(); // 🔥 updates filtering live
            }
        }
        private string _selectedFilter = "All";
        public string SelectedFilter
        {
            get => _selectedFilter;
            set
            {
                _selectedFilter = value;
                OnPropertyChanged();
                TasksView.Refresh(); // 🔥 important
            }
        }
        public ObservableCollection<TaskItem> Tasks { get; set; } = new();
        public ICollectionView TasksView { get; }
        private string _taskInput;
        public string TaskInput
        {
            get => _taskInput;
            set
            {
                _taskInput = value;
                OnPropertyChanged();
            }
        }

        private string path = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "TodoApp",
            "tasks.json"
        );

        public MainViewModel()
        {
            LoadTasks();

            TasksView = CollectionViewSource.GetDefaultView(Tasks);
            TasksView.Filter = FilterTasks;
        }
        private bool FilterTasks(object obj)
        {
            if (obj is not TaskItem task) return false;

            // 🔍 SEARCH FILTER
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                if (!task.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            // 🔽 STATUS FILTER
            return SelectedFilter switch
            {
                "Done" => task.IsDone,
                "Pending" => !task.IsDone,
                _ => true
            };
        }

        // ── Add Task ─────────────────────
        public void AddTask()
        {
            if (string.IsNullOrWhiteSpace(TaskInput)) return;

            Tasks.Add(new TaskItem { Title = TaskInput });
            TaskInput = string.Empty;
            SaveTasks();
        }

        // ── Delete ─────────────────────
        public void DeleteTask(TaskItem task)
        {
            if (task == null) return;

            Tasks.Remove(task);
            SaveTasks();
        }

        // ── Mark Done ──────────────────
        //public void MarkDone(TaskItem task)
        //{
        //    if (task == null) return;

        //    task.IsDone = true;
        //    OnPropertyChanged(nameof(Tasks));
        //    SaveTasks();
        //}

        // ── Save ───────────────────────
        public void ToggleDone(TaskItem task)
        {
            if (task == null) return;

            task.IsDone = !task.IsDone;
            TasksView.Refresh(); // 🔥 important
            SaveTasks();
        }
        private void SaveTasks()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            var json = JsonSerializer.Serialize(Tasks);
            File.WriteAllText(path, json);
        }

        // ── Load ───────────────────────
        private void LoadTasks()
        {
            if (!File.Exists(path)) return;

            var json = File.ReadAllText(path);
            var items = JsonSerializer.Deserialize<List<TaskItem>>(json);

            if (items != null)
            {
                foreach (var item in items)
                    Tasks.Add(item);
            }
        }
    }
}