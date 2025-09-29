# School Management System (CRUD)

## Description
A simple C# application using **.NET** and **Entity Framework Core** with **SQL Server** to manage students,subjects and enrollments in a SQL database. It demonstrates basic **CRUD operations** (Create, Read, Update, Delete) through the console interface and another WPF interface.

# Technologies

- C# (.NET 9)
- Entity Framework Core
- SQL Server / LocalDB
- Console Application
- WPF Application

## Getting Started

1. **Clone the repository**
   ```bash
   git clone https://github.com/MariaGenova36/SchoolManagementWithCRUD.git
   
2. Open in Visual Studio

3. Open the solution file SchoolManagementWithCRUD.sln.

4. Database setup

Update the connection string in SchoolDBContext.cs if necessary:

 protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
 {
     optionsBuilder.UseSqlServer(@"Server=localhost;Database=SchoolDb;Trusted_Connection=True;TrustServerCertificate=True;");
 }
    
Run the migrations:

- Update-Database
- Run the project

# Features

- Manage a list of students
  - Add new students
  - Display student list
  - Update a student
  - Delete a student
- Manage a list of subjects
  - Add new subjects
  - Display subject list
  - Edit subject details
  - Delete a subject
- Manage a list of enrollments
  - Add new enrollment
  - Display list of enrollments
  - Update an enrollment
  - Delete an enrollment
- Exporting the data to txt

## Interfaces

The project includes two interfaces:

1. **Console Application**
   - Simple command-line interface
   - Supports basic CRUD operations (Create, Read, Update, Delete)
   - Useful for demonstrating logic and database integration

2. **WPF Application**
   - Modern graphical interface
   - Provides user-friendly forms for managing Students, Teachers, and Courses
   - Demonstrates desktop application development with XAML and MVVM pattern

## Screenshots

### Console Interface

#### List of all Students
![List of all Students](/SchoolManagementWithCRUD/screenshots/Screenshot_2025-09-29_102517.png)

#### Adding a Student
![Adding a Student](/SchoolManagementWithCRUD/screenshots/Screenshot_2025-09-29_102517.png)

#### Editing a Student
![Editing a Student](/SchoolManagementWithCRUD/screenshots/Screenshot_2025-09-29_102915.png)

#### Removing a Student
![Removing a Student](/SchoolManagementWithCRUD/screenshots/Screenshot_2025-09-29_102946.png)

#### Export
![Export](/SchoolManagementWithCRUD/screenshots/Screenshot_2025-09-29_103010.png)

### WPF Interface

#### Menu
![Menu](/SchoolManagementWPF/screenshots/Screenshot_2025-09-29_134928.png)

#### List of all Students
![List of all Students](/SchoolManagementWPF/screenshots/Screenshot_2025-09-29_134947.png)

#### Adding a Student
![Adding a Student](/SchoolManagementWPF/screenshots/Screenshot_2025-09-29_135010.png)

#### Editing a Student
![Editing a Student](/SchoolManagementWPF/screenshots/Screenshot_2025-09-29_135040.png)

#### Removing a Student
![Removing a Student](/SchoolManagementWPF/screenshots/Screenshot_2025-09-29_135056.png)
