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
            StudentName = NameInput.Text.Trim();

            if (!int.TryParse(GradeInput.Text.Trim(), out int grade))
            {
                MessageBox.Show("Not valid grade. Please enter a number.");
                return;
            }

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
