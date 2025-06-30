using SchoolManagementConsole.Data;
using SchoolManagementConsole.Services;
using SchoolManagementWithCRUD.Services;

class Program
{
    static void Main(string[] args)
    {
        using var context = new SchoolDbContext();
        var studentService = new StudentService(context);
        var subjectService = new SubjectService(context);
        var enrollmentService = new EnrollmentService(context);

        // Добавяне на ученици
        studentService.AddStudent("Ivan Petrov", 11);
        studentService.AddStudent("Elena Dimitrova", 12);

        // Добавяне на предмети
        subjectService.AddSubject("Mathematics", "Mr. Stoyanov");
        subjectService.AddSubject("Chemistry", "Mrs. Tsvetkova");

        // Добавяне на записвания
        enrollmentService.AddEnrollment(1, 1); // Иван - Математика
        enrollmentService.AddEnrollment(2, 2); // Елена - Химия

        // Показване
        studentService.ListStudents();
        subjectService.ListSubjects();
        enrollmentService.ListEnrollments();

        // Изтриване на първия ученик (Id = 1)
        studentService.DeleteStudent(1);

        // Преименуване на предмет с Id = 1
        subjectService.EditSubjectTitle(1, "English");

        // Показване след промени
        Console.WriteLine("\nAfter changes:");
        studentService.ListStudents();
        subjectService.ListSubjects();
        enrollmentService.ListEnrollments();
    }
}
