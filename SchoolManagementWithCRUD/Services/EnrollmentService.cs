using SchoolManagementWithCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagementWithCRUD.Services
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
            var studentExists = _context.Students.Any(s => s.Id == studentId);
            var subjectExists = _context.Subjects.Any(s => s.Id == subjectId);

            if (!studentExists)
            {
                Console.WriteLine($"Student with ID {studentId} does not exist.");
                return;
            }

            if (!subjectExists)
            {
                Console.WriteLine($"Subject with ID {subjectId} does not exist.");
                return;
            }

            if (_context.Enrollments.Any(e => e.StudentId == studentId && e.SubjectId == subjectId))
            {
                Console.WriteLine("Enrollment already exists.");
                return;
            }

            var enrollment = new Enrollment
            {
                StudentId = studentId,
                SubjectId = subjectId,
                EnrollmentDate = DateTime.Now
            };
            _context.Enrollments.Add(enrollment);
            _context.SaveChanges();
        }

        public string GetEnrollmentsText()
        {
            var enrollments = _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Subject)
                .ToList();

            var sb = new System.Text.StringBuilder();
            sb.AppendLine("Enrollments:");
            foreach (var e in enrollments)
            {
                sb.AppendLine($"Student: {e.Student.Name} | Subject: {e.Subject.Title} | Date: {e.EnrollmentDate.ToShortDateString()}");
            }
            return sb.ToString();
        }

        public void ListEnrollments()
        {
            Console.WriteLine(GetEnrollmentsText());
        }

        public void EditEnrollment(int oldStudentId, int oldSubjectId, int newStudentId, int newSubjectId)
        {
            var enrollment = _context.Enrollments
                .FirstOrDefault(e => e.StudentId == oldStudentId && e.SubjectId == oldSubjectId);

            if (enrollment == null)
            {
                Console.WriteLine("Enrollment not found.");
                return;
            }

            var studentExists = _context.Students.Any(s => s.Id == newStudentId);
            var subjectExists = _context.Subjects.Any(s => s.Id == newSubjectId);

            if (!studentExists || !subjectExists)
            {
                Console.WriteLine("Invalid new student or subject.");
                return;
            }

            // Изтриваме старото записване
            _context.Enrollments.Remove(enrollment);
            _context.SaveChanges();

            // Добавяме ново записване с новите стойности
            var newEnrollment = new Enrollment
            {
                StudentId = newStudentId,
                SubjectId = newSubjectId,
                EnrollmentDate = DateTime.Now
            };

            _context.Enrollments.Add(newEnrollment);
            _context.SaveChanges();

            Console.WriteLine($"Enrollment updated: ({oldStudentId}, {oldSubjectId}) -> ({newStudentId}, {newSubjectId})");
        }


        public void DeleteEnrollment(int studentId, int subjectId)
        {
            var enrollment = _context.Enrollments.Find(studentId, subjectId);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
                _context.SaveChanges();
                Console.WriteLine($"Enrollment ({studentId}, {subjectId}) deleted.");
            }
            else
            {
                Console.WriteLine("Enrollment not found.");
            }
        }
     }
}
