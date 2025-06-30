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
        enrollmentService.AddEnrollment(1, 1); 
        enrollmentService.AddEnrollment(2, 2); 

        // Показване
        studentService.ListStudents();
        subjectService.ListSubjects();
        enrollmentService.ListEnrollments();

        // Изтриване на първия ученик
        studentService.DeleteStudent(1);

        // Преименуване на предмет
        subjectService.EditSubjectTitle(1, "English");

        // Показване след промени
        Console.WriteLine("\nAfter changes:");
        studentService.ListStudents();
        subjectService.ListSubjects();
        enrollmentService.ListEnrollments();
    }
}
