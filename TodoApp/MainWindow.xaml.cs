using System.Windows;
using TodoApp.Models;
using TodoApp.ViewModels;

namespace TodoApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        // 🔹 ADD
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel)?.AddTask();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MainViewModel;
            var task = (sender as FrameworkElement)?.DataContext as TaskItem;
            vm?.DeleteTask(task);
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MainViewModel;
            var task = (sender as FrameworkElement)?.DataContext as TaskItem;
            vm?.ToggleDone(task); ;
        }

        private void FilterAll_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).SelectedFilter = "All";
        }

        private void FilterDone_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).SelectedFilter = "Done";
        }

        private void FilterPending_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).SelectedFilter = "Pending";
        }
    }
}