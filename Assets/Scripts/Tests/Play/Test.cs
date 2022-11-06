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
    public class Test
    {
        #region Test Data
        internal class TestDatatableObjectClass : IDatabaseObject
        {
            private long _id;
            public long ID { get => _id; set => _id = value; }
        } 

        static int[] values = new int[]
        {
            1, 100, 200, 300, 500
        };
         
        static List<List<Employee>> employees =
            new List<List<Employee>>
        {
            {
                new List<Employee>
                {
                    new Employee { ID = 1, Name = "employee1", PosID = 1}
                }
             },
            { 
                new List<Employee>
                {
                    new Employee { ID = 1, Name = "employee1", PosID = 2},
                    new Employee { ID = 2, Name = "employee1", PosID = 3},
                    new Employee { ID = 3, Name = "employee1", PosID = 4}
                }
            },
            { 
                new List<Employee>
                {
                    new Employee { ID = 1, Name = "employee1", PosID = 2},
                    new Employee { ID = 2, Name = "employee2", PosID = 2},
                    new Employee { ID = 3, Name = "employee3", PosID = 4},
                    new Employee { ID = 4, Name = "employee4", PosID = 4},
                    new Employee { ID = 5, Name = "employee5", PosID = 4},
                    new Employee { ID = 6, Name = "employee6", PosID = 8},
                    new Employee { ID = 7, Name = "employee7", PosID = 13},
                    new Employee { ID = 8, Name = "employee8", PosID = 11},
                    new Employee { ID = 9, Name = "employee9", PosID = 10},
                    new Employee { ID = 10, Name = "employee10", PosID = 6}
                } 
            }

        };

        #endregion

        [UnityTest, Order(1)]
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

         
        [UnityTest, Order(3)]
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

        [UnityTest, Order(4)]
        public IEnumerator Create_Employee_Database_And_Then_Load_Data_And_Check_If_Correct([ValueSource("employees")] List<Employee> employees)
        {

            yield return Helpers.LoadTestScene();
            EmployeeManager EmployeeMgr = Helpers.GetTestEmployeeManager();

            EmployeeMgr.DB.ExecuteNonQuery(SQL.CREATE_TABLE_QUERY);
            EmployeeMgr.DB.ExecuteNonQuery(SQL.SALARY_INSERT_DATA);
            EmployeeMgr.DB.ExecuteNonQuery(SQL.RAISE_INSERT_DATA);
             
            List<PropertyInfo> properties = Employee.GetDBProperties();
            EmployeeMgr.DB.InsertData<Employee>(employees, true,  properties , "Employee");
            var newEmployeeData = EmployeeMgr.DB.LoadData<Employee>();


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
                    foreach(PropertyInfo prop in properties)
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

        [UnityTest, Order(2)]
        public IEnumerator Get_Employee_Data_From_Database()
        { 
            yield return Helpers.LoadTestScene();
            EmployeeManager EmployeeMgr = Helpers.GetTestEmployeeManager();
             
            EmployeeMgr.DB.ExecuteNonQuery(SQL.CREATE_TABLE_QUERY);
            EmployeeMgr.DB.ExecuteNonQuery(SQL.EMPLOYEE_INSERT_DATA);
             
            Assert.AreEqual(251, EmployeeMgr.DB.LoadData<Employee>().Count);

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

