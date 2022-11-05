using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using SalaryApp;
using System.IO;
using NSubstitute;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using System.Reflection;

namespace SalaryApp.Test
{
    public class DatabaseTest
    {
        internal class TestDatatableObjectClass : IDatabaseObject
        {
            private long _id;
            public long ID { get => _id; set => _id = value; }
        }


        static int[] values = new int[]
        {
            1, 100, 200, 300, 500
        };

        //static Dictionary<Dictionary<Department, Seniority>, int> employees =
        //    new Dictionary<Dictionary<Department, Seniority>, int> 
        //    {
        //        { new Dictionary<Department, Seniority> { { Department.HR, Seniority.Junior } }, 13 },
        //        { new Dictionary<Department, Seniority> { { Department.HR, Seniority.SemiSenior } }, 2 },
        //        { new Dictionary<Department, Seniority> { { Department.HR, Seniority.Senior } }, 5 },
        //        { new Dictionary<Department, Seniority> { { Department.Engineer, Seniority.Junior } }, 32 },
        //        { new Dictionary<Department, Seniority> { { Department.Engineer, Seniority.SemiSenior } }, 68 },
        //        { new Dictionary<Department, Seniority> { { Department.Engineer, Seniority.Senior } }, 50 },
        //        { new Dictionary<Department, Seniority> { { Department.Artist, Seniority.SemiSenior } }, 20 },
        //        { new Dictionary<Department, Seniority> { { Department.Artist, Seniority.Senior } }, 5 },
        //        { new Dictionary<Department, Seniority> { { Department.Design, Seniority.Junior} }, 15 },
        //        { new Dictionary<Department, Seniority> { { Department.Design, Seniority.Senior } }, 10 },
        //        { new Dictionary<Department, Seniority> { { Department.PM, Seniority.SemiSenior } }, 20 },
        //        { new Dictionary<Department, Seniority> { { Department.PM, Seniority.Senior } }, 10 },
        //        { new Dictionary<Department, Seniority> { { Department.CEO, Seniority.CEO } }, 1 }

        //    };

        static List<List<Employee>> employees =
            new List<List<Employee>>
        {
            {
                new List<Employee>
                {
                    new Employee { ID = 1, Name = "employee1", Department = Department.CEO.ToString(), Seniority = Seniority.CEO.ToString()}
                }
             },
            { 
                new List<Employee>
                {
                    new Employee { ID = 1, Name = "employee1", Department = Department.Engineer.ToString(), Seniority = Seniority.Junior.ToString()},
                    new Employee { ID = 2, Name = "employee1", Department = Department.Artist.ToString(), Seniority = Seniority.Junior.ToString()},
                    new Employee { ID = 3, Name = "employee1", Department = Department.PM.ToString(), Seniority = Seniority.Senior.ToString()}
                }
            },
            { 
                new List<Employee>
                {
                    new Employee { ID = 1, Name = "employee1", Department = Department.HR.ToString(), Seniority = Seniority.Senior.ToString()},
                    new Employee { ID = 2, Name = "employee2", Department = Department.HR.ToString(), Seniority = Seniority.SemiSenior.ToString()},
                    new Employee { ID = 3, Name = "employee3", Department = Department.Design.ToString(), Seniority = Seniority.Junior.ToString()},
                    new Employee { ID = 4, Name = "employee4", Department = Department.Design.ToString(), Seniority = Seniority.Senior.ToString()},
                    new Employee { ID = 5, Name = "employee5", Department = Department.PM.ToString(), Seniority = Seniority.Senior.ToString()},
                    new Employee { ID = 6, Name = "employee6", Department = Department.Artist.ToString(), Seniority = Seniority.Junior.ToString()},
                    new Employee { ID = 7, Name = "employee7", Department = Department.Engineer.ToString(), Seniority = Seniority.Junior.ToString()},
                    new Employee { ID = 8, Name = "employee8", Department = Department.Engineer.ToString(), Seniority = Seniority.Junior.ToString()},
                    new Employee { ID = 9, Name = "employee9", Department = Department.Engineer.ToString(), Seniority = Seniority.SemiSenior.ToString()},
                    new Employee { ID = 10, Name = "employee10", Department = Department.Engineer.ToString(), Seniority = Seniority.Senior.ToString()}
                } 
            }

        };

        [UnityTest]
        public IEnumerator Create_Database_And_Check_If_CorrectlyAdded()
        {
            //Arrange
            yield return Helpers.LoadTestScene();
            EmployeeManager EmployeeMgr = Helpers.GetTestEmployeeManager();

            //Act
            EmployeeMgr.DB.ExecuteNonQuery(SQL.CREATE_TABLE_QUERY);

            //Assert
            Assert.IsTrue(File.Exists(EmployeeMgr.DB.DBPath));
            yield return null;
        }

        [UnityTest]
        public IEnumerator Get_Employee_Data_From_Database()
        {
            //Arrange
            yield return Helpers.LoadTestScene();
            EmployeeManager EmployeeMgr = Helpers.GetTestEmployeeManager();

            //Act
            EmployeeMgr.DB.ExecuteNonQuery(SQL.CREATE_TABLE_QUERY);
            EmployeeMgr.DB.ExecuteNonQuery(SQL.EMPLOYEE_INSERT_DATA);

            //Assert
            Assert.AreEqual(251, EmployeeMgr.DB.LoadData<Employee>().Count);

            yield return null;
        }


        [UnityTest]
        public IEnumerator Create_New_DatabaseObject_Save_And_Check_If_Exists([ValueSource("values")] int amount)
        {
            yield return Helpers.LoadTestScene();
            EmployeeManager EmployeeMgr = Helpers.GetTestEmployeeManager();

            EmployeeMgr.DB.ExecuteNonQuery("CREATE TABLE IF NOT EXISTS test(id INTEGER PRIMARY KEY);");

            string insertValueCommand = "";

            for (int i = 0; i < amount; i++)
            {
                insertValueCommand += $"INSERT INTO test(id) VALUES({i});";
            }

            EmployeeMgr.DB.ExecuteNonQuery(insertValueCommand);

            Assert.GreaterOrEqual(amount, EmployeeMgr.DB.LoadData<TestDatatableObjectClass>("test").Count);
            yield return null;
        }

        [UnityTest]
        public IEnumerator Create_Employee_Database_And_Perform_Raises([ValueSource("employees")] List<Employee> employees)
        {

            yield return Helpers.LoadTestScene();
            EmployeeManager EmployeeMgr = Helpers.GetTestEmployeeManager();

            EmployeeMgr.DB.ExecuteNonQuery(SQL.CREATE_TABLE_QUERY);
            EmployeeMgr.DB.ExecuteNonQuery(SQL.SALARY_INSERT_DATA);
            EmployeeMgr.DB.ExecuteNonQuery(SQL.RAISE_INSERT_DATA);

            EmployeeMgr.DB.InsertData<Employee>(employees, true, "Employee");
            var newEmployeeData = EmployeeMgr.DB.LoadData<Employee>();

            Type t = typeof(Employee);

            bool isCorrect = true;
            foreach (Employee employee in newEmployeeData)
            {
                var temp = employees?.FirstOrDefault(x => x.ID == employee.ID);

                if (temp == null)
                {
                    isCorrect = false;
                }
                else
                {
                    foreach(PropertyInfo prop in t.GetProperties())
                    { 
                        if (prop.GetValue(employee).ToString() != prop.GetValue(temp).ToString())
                        {
                            isCorrect = false;
                            break;
                        }
                    }
                }
            }


            Assert.IsTrue(isCorrect);

            yield return null;
        }

    }

    public static class Helpers
    {
        public static IEnumerator LoadTestScene()
        {
            var operation = SceneManager.LoadSceneAsync("TestScene");
            while (!operation.isDone)
                yield return null;
        }

        public static EmployeeManager GetTestEmployeeManager()
        {
            var employeeMgr = GameObject.FindObjectOfType<EmployeeManager>();
            Database db = new Database(SQL.DATABASE_TEST_NAME);
            db.DeleteDatabase();
            employeeMgr.DB = db;

            return employeeMgr;
        }



    }


}

