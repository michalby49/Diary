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
using Diary.Model.Domains;
using System;

namespace Diary.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private Repository _repository = new Repository();
        public MainViewModel()
        {
            using (var context = new ApplicationDbContext())
            {
                var students = context.Students.ToList();
            }

            AddStudentCommand = new RelayCommand(AddEditStudent);
            EditStudentCommand = new RelayCommand(AddEditStudent, CanEditDeleteStudent);
            DeleteStudentCommand = new AsyncRelayCommand(DeleteStudent, CanEditDeleteStudent);
            RefreshStudentsCommand = new RelayCommand(RefreshStudents);
            SettingsStudentsCommand = new RelayCommand(OpenSettings);
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

        private StudentWrapper _selectedStudent;

        public StudentWrapper SelectedStudent
        {
            get { return _selectedStudent; }
            set 
            { 
                _selectedStudent = value;
                onPropertyChanged();
            }
        }
        private ObservableCollection<StudentWrapper> _students;
                
        public ObservableCollection<StudentWrapper> Students
        {
            get { return _students; }
            set 
            { 
                _students = value;
                onPropertyChanged();
            }
        }
        private void OpenSettings(object obj)
        {
            //var openSettings = new SettingsView();
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

            _repository.DeleteStudent(SelectedStudent.Id);

            RefreshDiary();
        }
        

        private void AddEditStudent(object obj)
        {
            var addEditStudentWindow = new AddEditStudentView(SelectedStudent);

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
            var groups = _repository.GetGroups();
            groups.Insert(0, new Group { Id = 0, Name = "Wszystkie" });

            Groups = new ObservableCollection<Group>(groups);
            
            SelectedGroupId = 0;
        }
        private void RefreshDiary()
        {

            Students = new ObservableCollection<StudentWrapper>
                (_repository.GetStudents(SelectedGroupId));
            
        }

        public ICommand AddStudentCommand { get; set; }
        public ICommand EditStudentCommand { get; set; }
        public ICommand DeleteStudentCommand { get; set; }
        public ICommand RefreshStudentsCommand { get; set; }
        public ICommand SettingsStudentsCommand { get; set; }
    }
}
