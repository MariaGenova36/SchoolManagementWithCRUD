using SchoolManagementWithCRUD.Services;
using System;

class Program
{
    static async Task Main(string[] args)
    {
        using var context = new SchoolDbContext();
        var studentService = new StudentService(context);
        var subjectService = new SubjectService(context);
        var enrollmentService = new EnrollmentService(context);

        while (true)
        {
            Console.WriteLine("\n=== School Management Menu ===");
            Console.WriteLine("1. List Students");
            Console.WriteLine("2. List Subjects");
            Console.WriteLine("3. List Enrollments");
            Console.WriteLine("4. Add Student");
            Console.WriteLine("5. Add Subject");
            Console.WriteLine("6. Add Enrollment");
            Console.WriteLine("7. Edit Student");
            Console.WriteLine("8. Edit Subject");
            Console.WriteLine("9. Edit Enrollment");
            Console.WriteLine("10. Delete Student");
            Console.WriteLine("11. Delete Subject");
            Console.WriteLine("12. Delete Enrollment");
            Console.WriteLine("13. Export all to TXT");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await studentService.ShowStudents();
                    break;
                case "2":
                   await subjectService.ListSubjects();
                    break;
                case "3":
                    await enrollmentService.ListEnrollments();
                    break;
                case "4":
                    Console.Write("Enter student name: ");
                    var studentName = Console.ReadLine();
                    Console.Write("Enter grade: ");
                    if (int.TryParse(Console.ReadLine(), out int grade))
                        await studentService.AddStudent(studentName, grade);
                    else Console.WriteLine("Invalid grade.");
                    break;
                case "5":
                    Console.Write("Enter subject title: ");
                    var subjectTitle = Console.ReadLine();
                    Console.Write("Enter teacher name: ");
                    var teacherName = Console.ReadLine();
                    await subjectService.AddSubject(subjectTitle, teacherName);
                    break;
                case "6":
                    Console.Write("Enter student ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int studentId))
                    {
                        Console.WriteLine("Invalid student ID.");
                        break;
                    }
                    Console.Write("Enter subject ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int subjectId))
                    {
                        Console.WriteLine("Invalid subject ID.");
                        break;
                    }
                    await enrollmentService.AddEnrollment(studentId, subjectId);
                    break;
                case "7":
                    Console.Write("Enter student ID to edit: ");
                    if (!int.TryParse(Console.ReadLine(), out int editStudentId))
                    {
                        Console.WriteLine("Invalid ID.");
                        break;
                    }
                    Console.Write("Enter new name: ");
                    var newName = Console.ReadLine();
                    Console.Write("Enter new grade: ");
                    if (!int.TryParse(Console.ReadLine(), out int newGrade))
                    {
                        Console.WriteLine("Invalid grade.");
                        break;
                    }
                   await studentService.EditStudent(editStudentId, newName, newGrade);
                    break;
                case "8":
                    Console.Write("Enter subject ID to edit: ");
                    if (!int.TryParse(Console.ReadLine(), out int editSubjectId))
                    {
                        Console.WriteLine("Invalid ID.");
                        break;
                    }
                    Console.Write("Enter new title: ");
                    var newTitle = Console.ReadLine();
                    Console.Write("Enter new teacher: ");
                    var newTeacher = Console.ReadLine();
                    await subjectService.EditSubject(editSubjectId, newTitle, newTeacher);
                    break;
                case "9":
                    Console.Write("Enter current student ID for enrollment: ");
                    if (!int.TryParse(Console.ReadLine(), out int oldStudentId))
                    {
                        Console.WriteLine("Invalid ID.");
                        break;
                    }
                    Console.Write("Enter current subject ID for enrollment: ");
                    if (!int.TryParse(Console.ReadLine(), out int oldSubjectId))
                    {
                        Console.WriteLine("Invalid ID.");
                        break;
                    }
                    Console.Write("Enter new student ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int newEnrollStudentId))
                    {
                        Console.WriteLine("Invalid ID.");
                        break;
                    }
                    Console.Write("Enter new subject ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int newEnrollSubjectId))
                    {
                        Console.WriteLine("Invalid ID.");
                        break;
                    }
                    await enrollmentService.EditEnrollment(oldStudentId, oldSubjectId, newEnrollStudentId, newEnrollSubjectId);
                    break;
                case "10":
                    Console.Write("Enter student ID to delete: ");
                    if (int.TryParse(Console.ReadLine(), out int delStudentId))
                        await studentService.DeleteStudent(delStudentId);
                    else Console.WriteLine("Invalid ID.");
                    break;
                case "11":
                    Console.Write("Enter subject ID to delete: ");
                    if (int.TryParse(Console.ReadLine(), out int delSubjectId))
                        await subjectService.DeleteSubject(delSubjectId);
                    else Console.WriteLine("Invalid ID.");
                    break;
                case "12":
                    Console.Write("Enter student ID for enrollment to delete: ");
                    if (!int.TryParse(Console.ReadLine(), out int delEnrollStudentId))
                    {
                        Console.WriteLine("Invalid ID.");
                        break;
                    }
                    Console.Write("Enter subject ID for enrollment to delete: ");
                    if (!int.TryParse(Console.ReadLine(), out int delEnrollSubjectId))
                    {
                        Console.WriteLine("Invalid ID.");
                        break;
                    }
                    await enrollmentService.DeleteEnrollment(delEnrollStudentId, delEnrollSubjectId);
                    break;
                case "13":
                    await ExportAllToTxt(studentService, subjectService, enrollmentService);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
    static async Task ExportAllToTxt(StudentService studentService,SubjectService subjectService,EnrollmentService enrollmentService)
    {
        var studentsText = await studentService.GetStudentsText();
        var subjectsText = await subjectService.GetSubjectsText();
        var enrollmentsText = await enrollmentService.GetEnrollmentsText();

        await System.IO.File.WriteAllTextAsync("Students.txt", studentsText);
        await System.IO.File.WriteAllTextAsync("Subjects.txt", subjectsText);
        await System.IO.File.WriteAllTextAsync("Enrollments.txt", enrollmentsText);

        Console.WriteLine("All data exported to Students.txt, Subjects.txt, Enrollments.txt");
    }
}
