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

        public static string POSITION_INSERT_DATA => Resources.Load<TextAsset>("PositionInsertData").text; 
        public static string SALARY_INSERT_DATA => Resources.Load<TextAsset>("SalaryInsertData").text;  
        public static string RAISE_INSERT_DATA => Resources.Load<TextAsset>("RaiseInsertData").text; 

        public static string DROP_TABLE_QUERY => Resources.Load<TextAsset>("DropTableQuery").text; 

        public static string CREATE_TABLE_QUERY => Resources.Load<TextAsset>("CreateTableQuery").text; 
    }
}
