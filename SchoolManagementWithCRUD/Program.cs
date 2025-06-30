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
        subjectService.AddSubject("English", "Mrs. Tsvetkova");

        // Добавяне на записвания
        var student1 = context.Students.FirstOrDefault(s => s.Name == "Ivan Petrov");
        var student2 = context.Students.FirstOrDefault(s => s.Name == "Elena Dimitrova");

        var subject1 = context.Subjects.FirstOrDefault(s => s.Title == "Mathematics");
        var subject2 = context.Subjects.FirstOrDefault(s => s.Title == "English");

        if (student1 != null && subject1 != null)
            enrollmentService.AddEnrollment(student1.Id, subject1.Id);

        if (student2 != null && subject2 != null)
            enrollmentService.AddEnrollment(student2.Id, subject2.Id);

        // Показване
        studentService.ListStudents();
        subjectService.ListSubjects();
        enrollmentService.ListEnrollments();

        // Изтриване на първия ученик (Id = 1)
        if (student1 != null)
            studentService.DeleteStudent(student1.Id);

        if (subject2 != null)
            subjectService.EditSubjectTitle(subject2.Id, "History");

        // Показване след промени
        Console.WriteLine("\nAfter changes:");
        studentService.ListStudents();
        subjectService.ListSubjects();
        enrollmentService.ListEnrollments();


        if (student2 != null && subject2 != null)
            enrollmentService.EditEnrollment(student2.Id, subject2.Id, student2.Id, subject1.Id);

        // Промяна на име на студент
        if (student2 != null)
            studentService.EditStudent(student2.Id, "Elena Georgieva", 11);


        Console.WriteLine("\nAfter second changes:");
        studentService.ListStudents();
        subjectService.ListSubjects();
        enrollmentService.ListEnrollments();

        if (subject2 != null)
            subjectService.DeleteSubject(subject2.Id);

        Console.WriteLine("\nAfter third changes:");
        studentService.ListStudents();
        subjectService.ListSubjects();
        enrollmentService.ListEnrollments();
    }
}
