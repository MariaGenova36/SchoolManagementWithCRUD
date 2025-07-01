using SchoolManagementWithCRUD.Models;

namespace SchoolManagementWithCRUD.Services
{
    public class StudentService
    {
        private readonly SchoolDbContext _context;

        public StudentService(SchoolDbContext context)
        {
            _context = context;
        }

        public void AddStudent(string name, int grade)
        {
            var student = new Student { Name = name, Grade = grade };
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public string GetStudentsText()
        {
            var students = _context.Students.ToList();
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("Students:");
            foreach (var s in students)
            {
                sb.AppendLine($"ID: {s.Id} | Name: {s.Name} | Grade: {s.Grade}");
            }
            return sb.ToString();
        }

        public void ListStudents()
        {
            Console.WriteLine(GetStudentsText());
        }

        public void EditStudent(int id, string newName, int newGrade)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            student.Name = newName;
            student.Grade = newGrade;
            _context.SaveChanges();
            Console.WriteLine($"Student {id} updated.");
        }

        public void DeleteStudent(int id)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }
    }
}
