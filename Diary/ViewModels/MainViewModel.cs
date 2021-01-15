using Diary.Commands;
using Diary.Model;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Diary.Views;

namespace Diary.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            AddStudentCommand = new RelayCommand(AddEditStudent);
            EditStudentCommand = new RelayCommand(AddEditStudent, CanEditDeleteStudent);
            DeleteStudentCommand = new AsyncRelayCommand(DeleteStudent, CanEditDeleteStudent);
            RefreshStudentsCommand = new RelayCommand(RefreshStudents);
            RefreshDiary();
            InitGroups();
        }

        private static ObservableCollection<Student> RefreshDiary()
        {
            return new ObservableCollection<Student>
            {
                new Student
                {
                    FirstName = "Michał",
                    LastName = "Beśka",
                    Group = new Group{ID = 1}
                },
                new Student
                {
                    FirstName = "Wiki",
                    LastName = "F",
                    Group = new Group{ID = 2}
                },
            };
        }

        private int _selectedGroupId;

        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set 
            { 
                _selectedGroupId = value;
                onPropertyChanged();
            }
        }

        private bool CanRefreshStudents(object obj)
        {
            return true;
        }
        private ObservableCollection<Group> _groups;

        public ObservableCollection<Group> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                onPropertyChanged();
            }
        }
        private Student _selectedStudent;

        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set 
            { 
                _selectedStudent = value;
                onPropertyChanged();
            }
        }
        private ObservableCollection<Student> _students;
                
        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set 
            { 
                _students = value;
                onPropertyChanged();
            }
        }

        private void RefreshStudents(object obj)
        {
            RefreshDiary();
        }
        private async Task DeleteStudent(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync("Usuwanie ucznia", $"Czy napewno chcesz usunąć ucznia {SelectedStudent.FirstName} {SelectedStudent.LastName}?", MessageDialogStyle.AffirmativeAndNegative);

            if (dialog != MessageDialogResult.Affirmative)
                return;
        }

        private void AddEditStudent(object obj)
        {
            var addEditStudent = new AddEditStudentView();
        }

       
        private bool CanEditDeleteStudent(object obj)
        {
            return SelectedStudent != null;
        }
        private void InitGroups()
        {
            Groups = new ObservableCollection<Group>
            {
                new Group {ID = 0, Name ="Wszystkie"},
                new Group {ID = 1, Name ="1A"},
                new Group {ID = 2, Name ="1B"},
            };
        }


        public ICommand AddStudentCommand { get; set; }
        public ICommand EditStudentCommand { get; set; }
        public ICommand DeleteStudentCommand { get; set; }
        public ICommand RefreshStudentsCommand { get; set; }
    }
}
