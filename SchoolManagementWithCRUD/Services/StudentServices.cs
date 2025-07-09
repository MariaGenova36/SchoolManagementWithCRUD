using Microsoft.EntityFrameworkCore;
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

        public async Task AddStudent(string name, int grade)
        {
            try
            {
                var student = new Student { Name = name, Grade = grade };
               await _context.Students.AddAsync(student);
               await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding student : {ex.Message}");
            }
        }

        public async Task<string> GetStudentsText()
        {
            var students = await ListStudents();
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("Students:");
            foreach (var s in students)
            {
                sb.AppendLine($"ID: {s.Id} | Name: {s.Name} | Grade: {s.Grade}");
            }
            return sb.ToString();
        }
        public async Task ShowStudents()
        {
            var students = await ListStudents();
            Console.WriteLine("Students:");
            foreach (var s in students)
            {
                Console.WriteLine($"ID: {s.Id} | Name: {s.Name} | Grade: {s.Grade}");
            }
        }
        public async Task <List<Student>> ListStudents()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task EditStudent(int id, string newName, int newGrade)
        {
            try
            {
                var student = await _context.Students.FindAsync(id);
                if (student == null)
                {
                    Console.WriteLine("Student not found.");
                    return;
                }

                student.Name = newName;
                student.Grade = newGrade;
                await _context.SaveChangesAsync();
                Console.WriteLine($"Student {id} updated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating student: {ex.Message}");
            }
        }

        public async Task DeleteStudent(int id)
        {
            try
            {
                var student = await _context.Students.FindAsync(id);
                if (student == null)
                {
                    Console.WriteLine("Student not found.");
                    return;
                }
                    _context.Students.Remove(student);
                    await _context.SaveChangesAsync();
                Console.WriteLine($"Student {id} deleted.");
                }
            catch(Exception ex) 
            {
                Console.WriteLine($"Error deleting student: {ex.Message}");
            }
        }
    }
}
