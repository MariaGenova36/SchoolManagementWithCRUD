using System.Windows;

namespace SchoolManagementWPF
{
    public partial class SubjectsEditorWindow : Window
    {
        public string SubjectTitle { get; private set; }
        public string TeacherName { get; private set; }

        public SubjectsEditorWindow(string title = "", string teacher = "", string name = "Subject")
        {
            InitializeComponent();
            this.Title = name;
            TitleInput.Text = title;
            TeacherInput.Text = teacher;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string subjectTitle = TitleInput.Text.Trim();
            string teacherName = TeacherInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(subjectTitle))
            {
                MessageBox.Show("Please enter a valid subject title.", "Invalid Title", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(teacherName))
            {
                MessageBox.Show("Please enter a valid teacher name.", "Invalid Teacher Name", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(subjectTitle, @"^[\p{L} ]+$"))
            {
                MessageBox.Show("Subject title must contain only letters.", "Invalid Title", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(teacherName, @"^[\p{L} ]+$"))
            {
                MessageBox.Show("Teacher name must contain only letters.", "Invalid Teacher Name", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SubjectTitle = subjectTitle;
            TeacherName = teacherName;

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
