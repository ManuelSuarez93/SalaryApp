using UnityEditor;
using UnityEngine;

namespace SalaryApp
{
    [CustomEditor(typeof(EmployeeManager))]
    public class EmployeeManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EmployeeManager script = (EmployeeManager)target;


            if (GUILayout.Button("Create Database"))
                script.InitializeDatabase();

            if (GUILayout.Button("Load Employee Data")) 
                script.LoadEmployeeData();
            if (GUILayout.Button("Load Salary Data"))
                script.LoadSalaryData();
            if (GUILayout.Button("Load Raise Data"))
                script.LoadRaiseData();

            if (GUILayout.Button("Update Employee Table")) 
                script.UpdateEmployeeTable(); 
            if (GUILayout.Button("Update Salary Table")) 
                script.UpdateSalaryTable(); 
            if (GUILayout.Button("Update Raise Table")) 
                script.UpdateRaiseTable();

            if (GUILayout.Button("Apply All Raises"))
                script.ApplyAllRaises();
            if (GUILayout.Button("Update Employee Salary"))
                script.UpdateEmployeeSalary();
            if (GUILayout.Button("Insert Salary Data"))
                script.InsertSalaryDataIntoDatabase();

        }
    }

}