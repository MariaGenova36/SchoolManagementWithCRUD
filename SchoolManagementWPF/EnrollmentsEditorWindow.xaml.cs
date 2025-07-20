using System.Windows;
using SchoolManagementWithCRUD.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SchoolManagementWPF
{
    public partial class EnrollmentsEditorWindow : Window
    {
        private readonly SchoolDbContext _context = new();

        public int SelectedStudentId => (int)StudentComboBox.SelectedValue;
        public int SelectedSubjectId => (int)SubjectComboBox.SelectedValue;

        public EnrollmentsEditorWindow(int selectedStudentId = -1, int selectedSubjectId = -1)
        {
            InitializeComponent();

            Title = selectedStudentId != -1 && selectedSubjectId != -1 ? "Edit Enrollment" : "Add Enrollment";

            _ = LoadDataAsync(selectedStudentId, selectedSubjectId);
        }

        private async Task LoadDataAsync(int selectedStudentId, int selectedSubjectId)
        {
            var students = await _context.Students.ToListAsync();
            var subjects = await _context.Subjects.ToListAsync();

            StudentComboBox.ItemsSource = students;
            SubjectComboBox.ItemsSource = subjects;

            if (selectedStudentId != -1)
                StudentComboBox.SelectedValue = selectedStudentId;

            if (selectedSubjectId != -1)
                SubjectComboBox.SelectedValue = selectedSubjectId;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (StudentComboBox.SelectedValue == null || SubjectComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please select both a student and a subject.");
                return;
            }

            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
