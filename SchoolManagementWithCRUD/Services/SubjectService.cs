using SchoolManagementWithCRUD.Models;

namespace SchoolManagementWithCRUD.Services
{
    public class SubjectService
    {
        private readonly SchoolDbContext _context;

        public SubjectService(SchoolDbContext context)
        {
            _context = context;
        }

        public void AddSubject(string title, string teacher)
        {
            var subject = new Subject { Title = title, Teacher = teacher };
            _context.Subjects.Add(subject);
            _context.SaveChanges();
        }

        public void ListSubjects()
        {
            var subjects = _context.Subjects.ToList();
            Console.WriteLine("Subjects:");
            foreach (var subject in subjects)
            {
                Console.WriteLine($"ID: {subject.Id} | Title: {subject.Title} | Teacher: {subject.Teacher}");
            }
        }

        public void EditSubjectTitle(int id, string newTitle)
        {
            var subject = _context.Subjects.Find(id);
            if (subject != null)
            {
                subject.Title = newTitle;
                _context.SaveChanges();
            }
        }

        public void DeleteSubject(int id)
        {
            var subject = _context.Subjects.Find(id);
            if (subject == null)
            {
                Console.WriteLine("Subject not found.");
                return;
            }

            _context.Subjects.Remove(subject);
            _context.SaveChanges();
            Console.WriteLine("Subject deleted.");
        }
    }
}
