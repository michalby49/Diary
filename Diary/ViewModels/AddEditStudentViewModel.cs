using Diary.Commands;
using Diary.Model;
using Diary.Model.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Diary.ViewModels
{
    class AddEditStudentViewModel : ViewModelBase
    {
        public AddEditStudentViewModel(StudentWraaper student = null)
        {
            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);

            if(student == null)
            {
                Student = new StudentWraaper();
            }
            else
            {
                Student = student;
                IsUpdate = true;
            }

            InitGroups();
        }
        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }

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

        private StudentWraaper _student;

        public StudentWraaper Student
        {
            get { return _student; }
            set { _student = value; }
        }

        private bool _isUpdate;

        public bool IsUpdate
        {
            get { return _isUpdate; }
            set { _isUpdate = value; }
        }
        private void InitGroups()
        {
            Groups = new ObservableCollection<GroupWrapper>
            {
                new GroupWrapper {Id = 0, Name ="-Brak-"},
                new GroupWrapper {Id = 1, Name ="1A"},
                new GroupWrapper {Id = 2, Name ="1B"},
            };
            Student.Group.Id = 0;
        }
        private void Confirm(object obj)
        {
            if(!IsUpdate)
            {
                AddStudent();
            }
            else
            {
                UpdateStudent();
            }
            CloseWindow(obj as Window);
        }

        private void UpdateStudent()
        {
            //BazaDanych
        }

        private void AddStudent()
        {
            //BazaDanych
        }

        private void Close(object obj)
        {
            CloseWindow(obj as Window);
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }
    }
}
