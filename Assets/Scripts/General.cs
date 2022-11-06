using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine; 

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
    public static class Util
    {
        public static List<PropertyInfo> GetPropertyListWithout(Type type, string[] propertiesToHide)
        {
            List<PropertyInfo> properties =
                            type.
                            GetProperties().
                            ToList();

            properties.RemoveAll(x => propertiesToHide.Any(y => y == x.Name)); 

            return properties;

        }
    }
    public static class SQL
    {
        public const string DATABASE_NAME = "database";
        public const string DATABASE_TEST_NAME = "testdb";
        public static string EMPLOYEE_INSERT_DATA => Resources.Load<TextAsset>("EmployeeInsertData").text;

        public const string POSITION_INSERT_DATA =
                "INSERT OR REPLACE INTO POSITION(id, department, seniority) VALUES(1,'hr', 'junior');" +
                "INSERT OR REPLACE INTO POSITION(id, department, seniority) VALUES(2,'hr', 'semisenior');" +
                "INSERT OR REPLACE INTO POSITION(id, department, seniority) VALUES(3,'hr', 'senior');" +
                "INSERT OR REPLACE INTO POSITION(id, department, seniority) VALUES(4,'engineer', 'junior');" +
                "INSERT OR REPLACE INTO POSITION(id, department, seniority) VALUES(5,'engineer', 'semisenior');" +
                "INSERT OR REPLACE INTO POSITION(id, department, seniority) VALUES(6,'engineer', 'senior');" +
                "INSERT OR REPLACE INTO POSITION(id, department, seniority) VALUES(7,'artist', 'semisenior');" +
                "INSERT OR REPLACE INTO POSITION(id, department, seniority) VALUES(8,'artist', 'senior');" +
                "INSERT OR REPLACE INTO POSITION(id, department, seniority) VALUES(9,'design', 'junior');" +
                "INSERT OR REPLACE INTO POSITION(id, department, seniority) VALUES(10,'design', 'senior');" +
                "INSERT OR REPLACE INTO POSITION(id, department, seniority) VALUES(11,'pm', 'semisenior');" +
                "INSERT OR REPLACE INTO POSITION(id, department, seniority) VALUES(12,'pm', 'senior');" +
                "INSERT OR REPLACE INTO POSITION(id, department, seniority) VALUES(13,'ceo', 'ceo');";
        public const string SALARY_INSERT_DATA =
                "INSERT OR REPLACE INTO Salary(posid,amount) VALUES(1, 500);" +
                "INSERT OR REPLACE INTO Salary(posid,amount) VALUES(2, 1000);" +
                "INSERT OR REPLACE INTO Salary(posid,amount) VALUES(3, 1500);" +
                "INSERT OR REPLACE INTO Salary(posid,amount) VALUES(4, 1500);" +
                "INSERT OR REPLACE INTO Salary(posid,amount) VALUES(5, 3000);" +
                "INSERT OR REPLACE INTO Salary(posid,amount) VALUES(6, 5000);" +
                "INSERT OR REPLACE INTO Salary(posid,amount) VALUES(7, 1200);" +
                "INSERT OR REPLACE INTO Salary(posid,amount) VALUES(8, 2000);" +
                "INSERT OR REPLACE INTO Salary(posid,amount) VALUES(9, 800);" +
                "INSERT OR REPLACE INTO Salary(posid,amount) VALUES(10, 2000);" +
                "INSERT OR REPLACE INTO Salary(posid,amount) VALUES(11, 2400);" +
                "INSERT OR REPLACE INTO Salary(posid,amount) VALUES(12, 4000);" +
                "INSERT OR REPLACE INTO Salary(posid,amount) VALUES(13, 20000);";

        public const string RAISE_INSERT_DATA =
                "INSERT OR REPLACE INTO Raise(posid,amount) VALUES(1, 0.5);" +
                "INSERT OR REPLACE INTO Raise(posid,amount) VALUES(2, 2);" +
                "INSERT OR REPLACE INTO Raise(posid,amount) VALUES(3, 5);" +
                "INSERT OR REPLACE INTO Raise(posid,amount) VALUES(4, 5);" +
                "INSERT OR REPLACE INTO Raise(posid,amount) VALUES(5, 7);" +
                "INSERT OR REPLACE INTO Raise(posid,amount) VALUES(6, 10);" +
                "INSERT OR REPLACE INTO Raise(posid,amount) VALUES(7, 2.5);" +
                "INSERT OR REPLACE INTO Raise(posid,amount) VALUES(8, 5);" +
                "INSERT OR REPLACE INTO Raise(posid,amount) VALUES(9, 4);" +
                "INSERT OR REPLACE INTO Raise(posid,amount) VALUES(10, 7);" +
                "INSERT OR REPLACE INTO Raise(posid,amount) VALUES(11, 5);" +
                "INSERT OR REPLACE INTO Raise(posid,amount) VALUES(12, 10);" +
                "INSERT OR REPLACE INTO Raise(posid,amount) VALUES(13, 100);";

        public const string DROP_TABLE_QUERY =
            "DROP TABLE IF EXISTS Salary;" +
            "DROP TABLE IF EXISTS Raise;" +
            "DROP TABLE IF EXISTS Employee;" +
            "DROP TABLE IF EXISTS Positoin;";

        public const string CREATE_TABLE_QUERY =
                "CREATE TABLE IF NOT EXISTS Position(id INTEGER PRIMARY KEY, department TEXT, seniority TEXT);" +
                "CREATE TABLE IF NOT EXISTS Employee(id INTEGER PRIMARY KEY,name TEXT, posid INTEGER, FOREIGN KEY(posid) REFERENCES Position(id));" +
                "CREATE TABLE IF NOT EXISTS Salary(id INTEGER PRIMARY KEY AUTOINCREMENT,amount REAL, posid INTEGER,  FOREIGN KEY(posid) REFERENCES Position(id), UNIQUE(posid));" +
                "CREATE TABLE IF NOT EXISTS Raise(id INTEGER PRIMARY KEY AUTOINCREMENT, amount REAL, posid INTEGER,  FOREIGN KEY(posid) REFERENCES Position(id), UNIQUE(posid))";
        
    }
}
