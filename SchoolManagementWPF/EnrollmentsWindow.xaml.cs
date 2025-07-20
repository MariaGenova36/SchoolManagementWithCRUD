using System.Windows;

namespace SchoolManagementWPF
{
    public partial class EnrollmentsWindow : Window
    {
        public EnrollmentsWindow()
        {
            InitializeComponent();
            DataContext = new EnrollmentViewModel();
        }
    }
}
