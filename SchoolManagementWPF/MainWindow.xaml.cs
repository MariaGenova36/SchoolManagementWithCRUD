using SchoolManagementWPF;
using System.Windows;

namespace SchoolManagementWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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

