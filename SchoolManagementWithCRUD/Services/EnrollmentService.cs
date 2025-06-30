using SchoolManagementWithCRUD.Models;

namespace SchoolManagementConsole.Services
{
    public class EnrollmentService
    {
        private readonly SchoolDbContext _context;

        public EnrollmentService(SchoolDbContext context)
        {
            _context = context;
        }

        public void AddEnrollment(int studentId, int subjectId)
        {
            var enrollment = new Enrollment
            {
                StudentId = studentId,
                SubjectId = subjectId,
                EnrollmentDate = DateTime.Now
            };
            _context.Enrollments.Add(enrollment);
            _context.SaveChanges();
        }

        public void ListEnrollments()
        {
            var enrollments = _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Subject)
                .ToList();

            Console.WriteLine("Enrollments:");
            foreach (var e in enrollments)
            {
                Console.WriteLine($"ID: {e.Id} | Student: {e.Student.Name} | Subject: {e.Subject.Title} | Date: {e.EnrollmentDate.ToShortDateString()}");
            }
        }

        public void DeleteEnrollment(int id)
        {
            var enrollment = _context.Enrollments.Find(id);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
                _context.SaveChanges();
            }
        }
    }
}
