using SchoolManagementWithCRUD.Models;
using SchoolManagementWithCRUD.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SchoolManagementWPF
{
    public class StudentsViewModel : INotifyPropertyChanged
    {
        private readonly StudentService _studentService;
        private readonly SchoolDbContext _context;

        public ObservableCollection<Student> Students { get; set; }
        private Student _selectedStudent;

        public Student SelectedStudent
        {
            get => _selectedStudent;
            set { _selectedStudent = value; OnPropertyChanged(); }
        }

        public ICommand AddStudentCommand { get; }
        public ICommand EditStudentCommand { get; }
        public ICommand DeleteStudentCommand { get; }

        public StudentsViewModel()
        {
            _context = new SchoolDbContext();
            _studentService = new StudentService(_context);

            AddStudentCommand = new RelayCommand(async _ => await AddStudent());
            EditStudentCommand = new RelayCommand(async _ => await EditStudent(), _ => SelectedStudent != null);
            DeleteStudentCommand = new RelayCommand(async _ => await DeleteStudent(), _ => SelectedStudent != null);

           _= LoadStudents();
        }

        private async Task LoadStudents()
        {
            var studentsList = await _studentService.ListStudents();
            Students = new ObservableCollection<Student>(studentsList);
            OnPropertyChanged(nameof(Students));
        }

        private async Task AddStudent()
        {
            var dialog = new StudentsEditorWindow(title: "Add a student");

            if (dialog.ShowDialog() == true)
            {
                await _studentService.AddStudent(dialog.StudentName, dialog.StudentGrade);
                await LoadStudents();
            }
        }

        private async Task EditStudent()
        {
            if (SelectedStudent == null)
                return;

            var dialog = new StudentsEditorWindow(SelectedStudent.Name, SelectedStudent.Grade, "Edit a student");

            if (dialog.ShowDialog() == true)
            {
                await _studentService.EditStudent(SelectedStudent.Id, dialog.StudentName, dialog.StudentGrade);
                await LoadStudents();
            }
        }

        private async Task DeleteStudent()
        {
            if (SelectedStudent == null)
                return;

            var result = MessageBox.Show($"Delete student {SelectedStudent.Name}?", "Confirm Delete", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                await _studentService.DeleteStudent(SelectedStudent.Id);
               await LoadStudents();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            if (propertyName != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
