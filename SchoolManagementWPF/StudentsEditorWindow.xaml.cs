using System.Windows;

namespace SchoolManagementWPF
{
    public partial class StudentsEditorWindow : Window
    {
        public string StudentName { get; private set; }
        public int StudentGrade { get; private set; }

        public StudentsEditorWindow(string name = "", int grade = 1, string title = "Student")
        {
            InitializeComponent();
            Title = title;
            NameInput.Text = name;
            GradeInput.Text = grade.ToString();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var studentName = NameInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(studentName))
            {
                MessageBox.Show("Please enter a valid name.", "Invalid Name", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(studentName, @"^[\p{L} ]+$"))
            {
                MessageBox.Show("Name must contain only letters.", "Invalid Name", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(GradeInput.Text.Trim(), out int grade))
            {
                MessageBox.Show("Not valid grade. Please enter a number.", "Invalid Grade", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (StudentGrade < 1 || StudentGrade > 12)
            {
                MessageBox.Show("Grade must be between 1 and 12.", "Invalid Grade", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            StudentName = studentName;
            StudentGrade = grade;
            DialogResult = true;
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
