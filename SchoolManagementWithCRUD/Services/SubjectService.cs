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

        public string GetSubjectsText()
        {
            var subjects = _context.Subjects.ToList();
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("Subjects:");
            foreach (var s in subjects)
            {
                sb.AppendLine($"ID: {s.Id} | Title: {s.Title} | Teacher: {s.Teacher}");
            }
            return sb.ToString();
        }

        public void ListSubjects()
        {
            Console.WriteLine(GetSubjectsText());
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
