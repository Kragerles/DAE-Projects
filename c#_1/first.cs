using System;
using System.Collections.Generic;

public class StudentManager
{
    // Private fields for encapsulation
    private List<string> studentNames;
    private List<int> studentGrades;

    // Constructor
    public StudentManager()
    {
        studentNames = new List<string>();
        studentGrades = new List<int>();
    }

    // Public method to add a student
    public void AddStudent(string name, int grade)
    {
        if (grade < 0 || grade > 100)
        {
            Console.WriteLine("Grade must be between 0 and 100.");
            return;
        }

        studentNames.Add(name);
        studentGrades.Add(grade);
        Console.WriteLine($"Student {name} added with grade {grade}.");
    }

    // Public method to calculate the average grade
    public double CalculateAverageGrade()
    {
        if (studentGrades.Count == 0)
        {
            Console.WriteLine("No students to calculate an average grade.");
            return 0;
        }

        int total = 0;
        foreach (int grade in studentGrades)
        {
            total += grade;
        }
        return (double)total / studentGrades.Count;
    }

    // Public method to print all students and their grades
    public void PrintAllStudents()
    {
        if (studentNames.Count == 0)
        {
            Console.WriteLine("No students to display.");
            return;
        }

        Console.WriteLine("Students and their grades:");
        for (int i = 0; i < studentNames.Count; i++)
        {
            Console.WriteLine($"{studentNames[i]}: {studentGrades[i]}");
        }
    }

    // Private method to get the highest grade
    private int GetHighestGrade()
    {
        int highest = -1;
        foreach (int grade in studentGrades)
        {
            if (grade > highest)
            {
                highest = grade;
            }
        }
        return highest;
    }

    // Public method to display the top-performing student
    public void DisplayTopStudent()
    {
        if (studentNames.Count == 0)
        {
            Console.WriteLine("No students to evaluate.");
            return;
        }

        int highestGrade = GetHighestGrade();
        Console.WriteLine($"Top-performing students with a grade of {highestGrade}:");

        for (int i = 0; i < studentNames.Count; i++)
        {
            if (studentGrades[i] == highestGrade)
            {
                Console.WriteLine(studentNames[i]);
            }
        }
    }
}

class first
{
    static void Main(string[] args)
    {
        StudentManager manager = new StudentManager();

        manager.AddStudent("Alice", 85);
        manager.AddStudent("Bob", 92);
        manager.AddStudent("Charlie", 78);

        manager.PrintAllStudents();

        Console.WriteLine($"Average Grade: {manager.CalculateAverageGrade():F2}");

        manager.DisplayTopStudent();
    }
}