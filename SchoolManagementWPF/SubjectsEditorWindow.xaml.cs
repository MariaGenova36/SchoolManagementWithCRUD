using System.Windows;

namespace SchoolManagementWPF
{
    public partial class SubjectsEditorWindow : Window
    {
        public string SubjectTitle => TitleInput.Text;
        public string TeacherName => TeacherInput.Text;

        public SubjectsEditorWindow(string title = "", string teacher = "", string name = "Subject")
        {
            InitializeComponent();
            this.Title = name;
            TitleInput.Text = title;
            TeacherInput.Text = teacher;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
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
