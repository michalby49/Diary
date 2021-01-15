using Diary.Model;
using Diary.ViewModels;
using MahApps.Metro.Controls;

namespace Diary.Views
{
    /// <summary>
    /// Logika interakcji dla klasy AddEditStudent.xaml
    /// </summary>
    public partial class AddEditStudentView : MetroWindow
    {
        public AddEditStudentView(Student student = null)
        {
            InitializeComponent();
            DataContext = new AddEditStudentViewModel(student);
        }

        
    }
}
