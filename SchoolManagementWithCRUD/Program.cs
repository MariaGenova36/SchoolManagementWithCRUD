using SchoolManagementConsole.Data;
using SchoolManagementConsole.Services;
using SchoolManagementWithCRUD.Services;

class Program
{
    static void Main()
    {
        using var context = new SchoolDbContext();

        context.Database.EnsureCreated();

        if (!context.Students.Any())
        {
            var student1 = new Student { Name = "Ivan Petrov", Grade = 11 };
            var student2 = new Student { Name = "Elena Dimitrova", Grade = 12 };

            // Добавяне на записвания
            enrollmentService.AddEnrollment(1, 1); // Иван - Математика
            enrollmentService.AddEnrollment(2, 2); // Елена - Химия

            // Извеждане на предметите
            Console.WriteLine("\nSubjects:");
            foreach (var subject in context.Subjects)
            {
                Console.WriteLine($"ID: {subject.Id} | Title: {subject.Title} | Teacher: {subject.Teacher}");
            }

            // Изтриване на първия ученик (Id = 1)
            studentService.DeleteStudent(1);

            // Преименуване на предмет с Id = 1
            subjectService.EditSubjectTitle(1, "English");

            // Редактиране на името на първия предмет (Mathematics -> English)
            var firstSubject = context.Subjects.FirstOrDefault();
            if (firstSubject != null)
            {
                firstSubject.Title = "English";
                context.SaveChanges();
                Console.WriteLine($"Updated subject ID {firstSubject.Id} title to {firstSubject.Title}");
            }
        }
    }
}