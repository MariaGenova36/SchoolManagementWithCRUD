using System.Windows;


namespace SchoolManagementWPF
{
    public partial class SubjectsWindow : Window
    {
        public SubjectsWindow()
        {
            InitializeComponent();
            DataContext = new SubjectsViewModel();
        }
    }
}
