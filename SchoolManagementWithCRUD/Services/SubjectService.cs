using Microsoft.EntityFrameworkCore;
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

        public async Task AddSubject(string title, string teacher)
        {
            try
            {
                var subject = new Subject { Title = title, Teacher = teacher };
                await _context.Subjects.AddAsync(subject);
                await _context.SaveChangesAsync();
                Console.WriteLine($"Subject {title} added.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding subject: {ex.Message}");
            }
        }
        public async Task<List<Subject>> GetAllSubjectsAsync()
        {
            return await _context.Subjects.ToListAsync();
        }
        public async Task<string> GetSubjectsText()
        {
            var subjects = await GetAllSubjectsAsync();
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("Subjects:");
            foreach (var s in subjects)
            {
                sb.AppendLine($"ID: {s.Id} | Title: {s.Title} | Teacher: {s.Teacher}");
            }
            return sb.ToString();
        }

        public async Task ListSubjects()
        {
            Console.WriteLine(await GetSubjectsText());
        }


        public async Task EditSubject(int id, string newTitle, string newTeacher)
        {
            try
            {
                var subject = await _context.Subjects.FindAsync(id);
                if (subject == null)
                {
                    Console.WriteLine("Subject not found.");
                    return;
                }
                    subject.Title = newTitle;
                subject.Teacher = newTeacher;
                await _context.SaveChangesAsync();
                Console.WriteLine($"Subject {id} updated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating subject: {ex.Message}");
            }
        }

        public async Task DeleteSubject(int id)
        {
            try
            {
                var subject = await _context.Subjects.FindAsync(id);
                if (subject == null)
                {
                    Console.WriteLine("Subject not found.");
                    return;
                }

                _context.Subjects.Remove(subject);
                await _context.SaveChangesAsync();
                Console.WriteLine($"Subject {id} deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting subject: {ex.Message}");
            }
    }   }
}
