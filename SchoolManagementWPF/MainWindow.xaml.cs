using SchoolManagementWithCRUD.Services;
using SchoolManagementWPF;
using System.IO;
using System.Windows;

namespace SchoolManagementWPF
{
    public partial class MainWindow : Window
    {
        private readonly StudentService _studentService;
        private readonly SubjectService _subjectService;
        private readonly EnrollmentService _enrollmentService;

        public MainWindow()
        {
            InitializeComponent();

            var context = new SchoolDbContext();

            _studentService = new StudentService(context);
            _subjectService = new SubjectService(context);
            _enrollmentService = new EnrollmentService(context);   
        }

        private async void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            var studentsText = await _studentService.GetStudentsText();
            var subjectsText = await _subjectService.GetSubjectsText();
            var enrollmentsText = await _enrollmentService.GetEnrollmentsText();

            await File.WriteAllTextAsync("Students.txt", studentsText);
            await File.WriteAllTextAsync("Subjects.txt", subjectsText);
            await File.WriteAllTextAsync("Enrollments.txt", enrollmentsText);

            MessageBox.Show("All data exported to Students.txt, Subjects.txt, Enrollments.txt",
                            "Export Completed",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }


        private void OpenStudents_Click(object sender, RoutedEventArgs e)
        {
            var studentsWindow = new StudentsWindow();
            studentsWindow.Show();
        }

        private void OpenSubjects_Click(object sender, RoutedEventArgs e)
        {
            var subjectsWindow = new SubjectsWindow();
            subjectsWindow.Show();
        }

        private void OpenEnrollments_Click(object sender, RoutedEventArgs e)
        {
            var enrollmentsWindow = new EnrollmentsWindow();
            enrollmentsWindow.Show();
        }
    }
}

