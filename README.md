# 📝 WPF Task Manager

A task management application built with **C# and WPF using MVVM**.

---

## 🚀 Features

* Add / delete tasks
* Mark tasks as done / undone
* 🔍 Search tasks (live filtering)
* 🔽 Filter tasks (All / Done / Pending)
* 🎨 Visual feedback for selected filter
* 💾 Persistent storage (JSON)

---

## 🧠 Architecture

* MVVM pattern
* ObservableCollection for dynamic UI updates
* ICollectionView for filtering

---

## 💾 Data Storage

Tasks are stored locally in:

%AppData%/TodoApp/tasks.json

---

## ▶️ How to Run

1. Clone repository
2. Open `.sln` file in Visual Studio
3. Run project

---

## 📌 Future Improvements

* Priority system
* Due dates
* Full ICommand (remove code-behind)
* UI redesign (modern look)
