﻿using Diary.Commands;
using Diary.Model;
using Diary.Model.Domains;
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
        private Repository _repository = new Repository();
        public AddEditStudentViewModel(StudentWrapper student = null)
        {
            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);

            if(student == null)
            {
                Student = new StudentWrapper();
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

        private StudentWrapper _student;

        public StudentWrapper Student
        {
            get { return _student; }
            set 
            { 
                _student = value;
                onPropertyChanged();
            }
        }

        private bool _isUpdate;

        public bool IsUpdate
        {
            get { return _isUpdate; }
            set 
            { 
                _isUpdate = value;
                onPropertyChanged();
            }
        }
        private void InitGroups()
        {
            var groups = _repository.GetGroups();
            groups.Insert(0, new Group { Id = 0, Name = "-Brak-" });

            Groups = new ObservableCollection<Group>(groups);

            SelectedGroupId = Student.Group.Id;
        }
        private void Confirm(object obj)
        {
            if (Student.IsValid)
                return;

            if (!IsUpdate)
                AddStudent();
            else
                UpdateStudent();

            CloseWindow(obj as Window);
        }

        private void UpdateStudent()
        {
            _repository.UpdateStudent(Student);
        }

        private void AddStudent()
        {
            _repository.AddStudent(Student);
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
