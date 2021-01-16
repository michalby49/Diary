using Diary.Commands;
using Diary.Model;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Diary.Views;
using Diary.Model.Wrappers;
using System.Linq;

namespace Diary.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            using (var context = new AplicationDbContext())
            {
                var students = context.Students.ToList();
            }

            AddStudentCommand = new RelayCommand(AddEditStudent);
            EditStudentCommand = new RelayCommand(AddEditStudent, CanEditDeleteStudent);
            DeleteStudentCommand = new AsyncRelayCommand(DeleteStudent, CanEditDeleteStudent);
            RefreshStudentsCommand = new RelayCommand(RefreshStudents);
            RefreshDiary();
            InitGroups();
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

        
        private ObservableCollection<GroupWrapper> _groups;

        public ObservableCollection<GroupWrapper> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                onPropertyChanged();
            }
        }

        private StudentWraaper _selectedStudent;

        public StudentWraaper SelectedStudent
        {
            get { return _selectedStudent; }
            set 
            { 
                _selectedStudent = value;
                onPropertyChanged();
            }
        }
        private ObservableCollection<StudentWraaper> _students;
                
        public ObservableCollection<StudentWraaper> Students
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
            var addEditStudentWindow = new AddEditStudentView(obj as StudentWraaper);
            addEditStudentWindow.Closed += AddEditStudentWindow_Closed;
            addEditStudentWindow.ShowDialog();
        }

        private void AddEditStudentWindow_Closed(object sender, System.EventArgs e)
        {
            RefreshDiary();
        }

        private bool CanEditDeleteStudent(object obj)
        {
            return SelectedStudent != null;
        }
        private void InitGroups()
        {
            Groups = new ObservableCollection<GroupWrapper>
            {
                new GroupWrapper {Id = 0, Name ="Wszystkie"},
                new GroupWrapper {Id = 1, Name ="1A"},
                new GroupWrapper {Id = 2, Name ="1B"},
            };
            SelectedGroupId = 0;
        }
        private void RefreshDiary()
        {

            Students = new ObservableCollection<StudentWraaper>
            {
                new StudentWraaper
                {
                    FirstName = "Michał",
                    LastName = "Beśka",
                    Group = new GroupWrapper { Id = 1 }
                },
                new StudentWraaper
                {
                    FirstName = "Wiki",
                    LastName = "Freus",
                    Group = new GroupWrapper { Id = 2 }
                }
            };


        }

        public ICommand AddStudentCommand { get; set; }
        public ICommand EditStudentCommand { get; set; }
        public ICommand DeleteStudentCommand { get; set; }
        public ICommand RefreshStudentsCommand { get; set; }
    }
}
