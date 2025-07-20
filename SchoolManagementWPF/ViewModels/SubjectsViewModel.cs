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
    public class SubjectsViewModel : INotifyPropertyChanged
    {
        private readonly SubjectService _subjectService;
        private readonly SchoolDbContext _context;

        public ObservableCollection<Subject> Subjects { get; set; }
        private Subject _selectedSubject;

        public Subject SelectedSubject
        {
            get => _selectedSubject;
            set { _selectedSubject = value; OnPropertyChanged(); }
        }

        public ICommand AddSubjectCommand { get; }
        public ICommand EditSubjectCommand { get; }
        public ICommand DeleteSubjectCommand { get; }

        public SubjectsViewModel()
        {
            _context = new SchoolDbContext();
            _subjectService = new SubjectService(_context);

            AddSubjectCommand = new RelayCommand(async _ => await AddSubject());
            EditSubjectCommand = new RelayCommand(async _ => await EditSubject(), _ => SelectedSubject != null);
            DeleteSubjectCommand = new RelayCommand(async _ => await DeleteSubject(), _ => SelectedSubject != null);

           _= LoadSubjects();
        }

        private async Task LoadSubjects()
        {
            var subjectsList = await _subjectService.GetAllSubjectsAsync();
            Subjects = new ObservableCollection<Subject>(subjectsList);
            OnPropertyChanged(nameof(Subjects));
        }

        private async Task AddSubject()
        {
            var dialog = new SubjectsEditorWindow(name: "Add a subject");

            if (dialog.ShowDialog() == true)
            {
                await _subjectService.AddSubject(dialog.SubjectTitle, dialog.TeacherName);
                await LoadSubjects();
            }
        }

        private async Task EditSubject()
        {
            if (SelectedSubject == null)
                return;

            var dialog = new SubjectsEditorWindow(SelectedSubject.Title, SelectedSubject.Teacher, "Edit a subject");

            if (dialog.ShowDialog() == true)
            {
                await _subjectService.EditSubject(SelectedSubject.Id, dialog.SubjectTitle, dialog.TeacherName);
                await LoadSubjects();
            }
        }

        private async Task DeleteSubject()
        {
            if (SelectedSubject == null)
                return;

            var result = MessageBox.Show($"Delete subject {SelectedSubject.Title}?", "Confirm Delete", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                await _subjectService.DeleteSubject(SelectedSubject.Id);
               await LoadSubjects();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            if (propertyName != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
