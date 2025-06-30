using SchoolManagementWithCRUD;
using System;
using System.Linq;
using SchoolManagementWithCRUD.Models;

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

            var subject1 = new Subject { Title = "Mathematics", Teacher = "Mr. Stoyanov" };
            var subject2 = new Subject { Title = "Chemistry", Teacher = "Mrs. Tsvetkova" };

            context.Students.AddRange(student1, student2);
            context.Subjects.AddRange(subject1, subject2);

            context.SaveChanges();

            // Записване на учениците в предметите
            context.Enrollments.Add(new Enrollment { StudentId = student1.Id, SubjectId = subject1.Id });
            context.Enrollments.Add(new Enrollment { StudentId = student1.Id, SubjectId = subject2.Id });
            context.Enrollments.Add(new Enrollment { StudentId = student2.Id, SubjectId = subject1.Id });

            context.SaveChanges();
        }

        // Извеждане на учениците
        Console.WriteLine("Students:");
        foreach (var student in context.Students)
        {
            Console.WriteLine($"ID: {student.Id} | Name: {student.Name} | Grade: {student.Grade}");
        }

        // Извеждане на предметите
        Console.WriteLine("\nSubjects:");
        foreach (var subject in context.Subjects)
        {
            Console.WriteLine($"ID: {subject.Id} | Title: {subject.Title} | Teacher: {subject.Teacher}");
        }

        // Изтриване на първия ученик и съответните му записи
        var firstStudent = context.Students.FirstOrDefault();
        if (firstStudent != null)
        {
            // Премахваме записите за него в Enrollments
            var enrollments = context.Enrollments.Where(e => e.StudentId == firstStudent.Id);
            context.Enrollments.RemoveRange(enrollments);

            context.Students.Remove(firstStudent);
            context.SaveChanges();
            Console.WriteLine($"\nDeleted student ID {firstStudent.Id}");
        }

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
