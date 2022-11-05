using UnityEngine;
using System.IO; 

namespace SalaryApp
{
    public enum Department
    {
        HR,
        Engineer,
        Artist,
        Design,
        PM,
        CEO
    }

    public enum Seniority
    {
        Junior,
        SemiSenior,
        Senior,
        CEO
    }
    public static class SQL
    {
        public const string DATABASE_NAME = "database";
        public const string DATABASE_TEST_NAME = "testdb";
        public static string EMPLOYEE_INSERT_DATA => Resources.Load<TextAsset>("EmployeeInsertData").text;
        public const string SALARY_INSERT_DATA =
                "INSERT OR REPLACE INTO Salary(department, seniority,amount) VALUES('hr', 'junior', 500);" +
                "INSERT OR REPLACE INTO Salary(department, seniority,amount) VALUES('hr', 'semisenior', 1000);" +
                "INSERT OR REPLACE INTO Salary(department, seniority,amount) VALUES('hr', 'senior', 1500);" +
                "INSERT OR REPLACE INTO Salary(department, seniority,amount) VALUES('engineer', 'junior', 1500);" +
                "INSERT OR REPLACE INTO Salary(department, seniority,amount) VALUES('engineer', 'semisenior', 3000);" +
                "INSERT OR REPLACE INTO Salary(department, seniority,amount) VALUES('engineer', 'senior', 5000);" +
                "INSERT OR REPLACE INTO Salary(department, seniority,amount) VALUES('artist', 'semisenior', 1200);" +
                "INSERT OR REPLACE INTO Salary(department, seniority,amount) VALUES('artist', 'senior', 2000);" +
                "INSERT OR REPLACE INTO Salary(department, seniority,amount) VALUES('design', 'junior', 800);" +
                "INSERT OR REPLACE INTO Salary(department, seniority,amount) VALUES('design', 'senior', 2000);" +
                "INSERT OR REPLACE INTO Salary(department, seniority,amount) VALUES('pm', 'semisenior', 2400);" +
                "INSERT OR REPLACE INTO Salary(department, seniority,amount) VALUES('pm', 'senior', 4000);" +
                "INSERT OR REPLACE INTO Salary(department, seniority,amount) VALUES('ceo', 'ceo', 20000);";

        public const string RAISE_INSERT_DATA =
                "INSERT OR REPLACE INTO Raise(department, seniority,amount) VALUES('hr', 'junior', 0.5);" +
                "INSERT OR REPLACE INTO Raise(department, seniority,amount) VALUES('hr', 'semisenior', 2);" +
                "INSERT OR REPLACE INTO Raise(department, seniority,amount) VALUES('hr', 'senior', 5);" +
                "INSERT OR REPLACE INTO Raise(department, seniority,amount) VALUES('engineer', 'junior', 5);" +
                "INSERT OR REPLACE INTO Raise(department, seniority,amount) VALUES('engineer', 'semisenior', 7);" +
                "INSERT OR REPLACE INTO Raise(department, seniority,amount) VALUES('engineer', 'senior', 10);" +
                "INSERT OR REPLACE INTO Raise(department, seniority,amount) VALUES('artist', 'semisenior', 2.5);" +
                "INSERT OR REPLACE INTO Raise(department, seniority,amount) VALUES('artist', 'senior', 5);" +
                "INSERT OR REPLACE INTO Raise(department, seniority,amount) VALUES('design', 'junior', 4);" +
                "INSERT OR REPLACE INTO Raise(department, seniority,amount) VALUES('design', 'senior', 7);" +
                "INSERT OR REPLACE INTO Raise(department, seniority,amount) VALUES('pm', 'semisenior', 5);" +
                "INSERT OR REPLACE INTO Raise(department, seniority,amount) VALUES('pm', 'senior', 10);" +
                "INSERT OR REPLACE INTO Raise(department, seniority,amount) VALUES('ceo', 'ceo', 100);";

        public const string CREATE_TABLE_QUERY =
                "CREATE TABLE IF NOT EXISTS Employee(id INTEGER PRIMARY KEY,name TEXT, department TEXT, seniority TEXT);" +
                "CREATE TABLE IF NOT EXISTS Salary(id INTEGER PRIMARY KEY AUTOINCREMENT, department TEXT, seniority TEXT, amount REAL,  UNIQUE (department,seniority));" +
                "CREATE TABLE IF NOT EXISTS Raise(id INTEGER PRIMARY KEY AUTOINCREMENT, department TEXT, seniority TEXT, amount REAL,  UNIQUE (department,seniority))";

    }
}
