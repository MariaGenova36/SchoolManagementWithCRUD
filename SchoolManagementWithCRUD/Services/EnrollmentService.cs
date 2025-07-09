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

        public async Task AddEnrollment(int studentId, int subjectId)
        {
            try
            {
                var studentExists = await _context.Students.AnyAsync(s => s.Id == studentId);
                var subjectExists = await _context.Subjects.AnyAsync(s => s.Id == subjectId);

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

                var exists = await _context.Enrollments.AnyAsync(e => e.StudentId == studentId && e.SubjectId == subjectId);
                if (exists)
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
                await _context.Enrollments.AddAsync(enrollment);
                await _context.SaveChangesAsync();
                Console.WriteLine($"Enrollment added: Student {studentId} -> Subject {subjectId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding enrollment: {ex.Message}");
            }
        }

        public async Task<string> GetEnrollmentsText()
        {
            var enrollments = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Subject)
                .ToListAsync();

            var sb = new System.Text.StringBuilder();
            sb.AppendLine("Enrollments:");
            foreach (var e in enrollments)
            {
                sb.AppendLine($"Student: {e.Student.Name} | Subject: {e.Subject.Title} | Date: {e.EnrollmentDate.ToShortDateString()}");
            }
            return sb.ToString();
        }

        public async Task ListEnrollments()
        {
            Console.WriteLine(await GetEnrollmentsText());
        }

        public async Task EditEnrollment(int oldStudentId, int oldSubjectId, int newStudentId, int newSubjectId)
        {
            try
            {
                var enrollment = await _context.Enrollments.FindAsync(oldStudentId, oldSubjectId);
                if (enrollment == null)
                {
                    Console.WriteLine("Enrollment not found.");
                    return;
                }

                var exists = await _context.Enrollments.AnyAsync(e =>
            e.StudentId == newStudentId && e.SubjectId == newSubjectId);
                if (exists)
                {
                    Console.WriteLine("New enrollment already exists.");
                    return;
                }

                // Изтриваме старото записване
                _context.Enrollments.Remove(enrollment);
                await _context.SaveChangesAsync();

                // Добавяме ново записване с новите стойности
                var newEnrollment = new Enrollment
                {
                    StudentId = newStudentId,
                    SubjectId = newSubjectId,
                    EnrollmentDate = DateTime.Now
                };

                await _context.Enrollments.AddAsync(newEnrollment);
                await _context.SaveChangesAsync();

                Console.WriteLine($"Enrollment updated: ({oldStudentId}, {oldSubjectId}) -> ({newStudentId}, {newSubjectId})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error editing enrollment: {ex.Message}");
            }
        }

        public async Task DeleteEnrollment(int studentId, int subjectId)
        {
            try
            {
                var enrollment = await _context.Enrollments.FindAsync(studentId, subjectId);
                if (enrollment == null)
                {
                    Console.WriteLine("Enrollment not found.");
                    return;
                }
                _context.Enrollments.Remove(enrollment);
                await _context.SaveChangesAsync();
                Console.WriteLine($"Enrollment ({studentId}, {subjectId}) deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting enrollment: {ex.Message}");
            }
        }
    }
}

