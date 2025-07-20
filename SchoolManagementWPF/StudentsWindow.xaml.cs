using System.Windows;


namespace SchoolManagementWPF
{
    public partial class StudentsWindow : Window
    {
        public StudentsWindow()
        {
            InitializeComponent();
            DataContext = new StudentsViewModel();
        }
    }
}
