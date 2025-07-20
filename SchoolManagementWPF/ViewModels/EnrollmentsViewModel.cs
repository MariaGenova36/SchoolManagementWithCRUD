using Microsoft.EntityFrameworkCore;
using SchoolManagementWithCRUD.Models;
using SchoolManagementWithCRUD.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SchoolManagementWPF
{
    public class EnrollmentViewModel : INotifyPropertyChanged
    {
        private readonly EnrollmentService _enrollmentService;
        private readonly SchoolDbContext _context;

        public ObservableCollection<Enrollment> Enrollments { get; set; } = new();
        private Enrollment _selectedEnrollment;
        public Enrollment SelectedEnrollment
        {
            get => _selectedEnrollment;
            set { _selectedEnrollment = value; OnPropertyChanged(); }
        }

        public ICommand AddEnrollmentCommand { get; }
        public ICommand EditEnrollmentCommand { get; }
        public ICommand DeleteEnrollmentCommand { get; }

        public EnrollmentViewModel()
        {
            _context = new SchoolDbContext();
            _enrollmentService = new EnrollmentService(_context);

            AddEnrollmentCommand = new RelayCommand(async _ => await AddEnrollment());
            EditEnrollmentCommand = new RelayCommand(async _ => await EditEnrollment(), _ => SelectedEnrollment != null);
            DeleteEnrollmentCommand = new RelayCommand(async _ => await DeleteEnrollment(), _ => SelectedEnrollment != null);

            _ = LoadEnrollments();
        }

        private async Task LoadEnrollments()
        {
            Enrollments.Clear();
            var list = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Subject)
                .ToListAsync();

            foreach (var enrollment in list)
            {
                Enrollments.Add(enrollment);
            }
        }

        private async Task AddEnrollment()
        {
            var dialog = new EnrollmentsEditorWindow();
            if (dialog.ShowDialog() == true)
            {
                await _enrollmentService.AddEnrollment(dialog.SelectedStudentId, dialog.SelectedSubjectId);
                await LoadEnrollments();
            }
        }

        private async Task EditEnrollment()
        {
            if (SelectedEnrollment == null) return;

            var dialog = new EnrollmentsEditorWindow(SelectedEnrollment.StudentId, SelectedEnrollment.SubjectId);
            if (dialog.ShowDialog() == true)
            {
                await _enrollmentService.EditEnrollment(
                    oldStudentId: SelectedEnrollment.StudentId,
                    oldSubjectId: SelectedEnrollment.SubjectId,
                    newStudentId: dialog.SelectedStudentId,
                    newSubjectId: dialog.SelectedSubjectId
                );

                await LoadEnrollments();
            }
        }


        private async Task DeleteEnrollment()
        {
            if (SelectedEnrollment == null) return;

            var confirm = MessageBox.Show(
                $"Delete enrollment for student {SelectedEnrollment.Student.Name} and subject {SelectedEnrollment.Subject.Title}?",
                "Confirm Delete", MessageBoxButton.YesNo);

            if (confirm == MessageBoxResult.Yes)
            {
                await _enrollmentService.DeleteEnrollment(SelectedEnrollment.StudentId, SelectedEnrollment.SubjectId);
                await LoadEnrollments();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
