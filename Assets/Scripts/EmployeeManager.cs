using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SalaryApp
{
    public class EmployeeManager : MonoBehaviour
    {
        [Header("Tables")] 
        [SerializeField] private List<Employee> _employees = new List<Employee>();
        [SerializeField] private List<Salary> _salaries = new List<Salary>();
        [SerializeField] private List<Raise> _raises = new List<Raise>();
        [SerializeField] private Table _table;
        [SerializeField] bool initializeOnStart;
        [SerializeField] bool roundSalaries;
        public IDatabase DB { get; set; } = new Database(SQL.DATABASE_NAME);
        public List<Employee> Employees => _employees;
        public List<Salary> Salaries => _salaries;
        public List<Raise> Raises => _raises;
        public static EmployeeManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            } 

            Instance = this;
        }
        void Start()
        {
            if (initializeOnStart)
                InitializeDatabase(); 
        }

        [ContextMenu("Create Database")]
        public void InitializeDatabase()
        { 
            DB.ExecuteNonQuery(SQL.CREATE_TABLE_QUERY);
            DB.ExecuteNonQuery(Resources.Load<TextAsset>("EmployeeInsertData").text);
            DB.ExecuteNonQuery(SQL.SALARY_INSERT_DATA);
            DB.ExecuteNonQuery(SQL.RAISE_INSERT_DATA);
        }

        public float Raise(Department position, Seniority seniority)
        {
            float raise = 0f; 
            
            return raise;
        }


        [ContextMenu("Load Employee Data")]
        public void LoadEmployeeData() 
            => _employees = DB.LoadData<Employee>();

        [ContextMenu("Load Salary Data")]
        public void LoadSalaryData() 
            => _salaries = DB.LoadData<Salary>();

        [ContextMenu("Load Raise Data")]
        public void LoadRaiseData() 
            => _raises = DB.LoadData<Raise>();
      
        [ContextMenu("Update Employee Table")]
        public void UpdateEmployeeTable()
            => _table.UpdateTable<Employee>(_employees);

        [ContextMenu("Update Salary Table")]
        public void UpdateSalaryTable() 
            => _table.UpdateTable<Salary>(_salaries);
        [ContextMenu("Update Raise Table")]
        public void UpdateRaiseTable() 
            => _table.UpdateTable<Raise>(_raises);

        [ContextMenu("Insert Salary Data")]
        public void InsertSalaryDataIntoDatabase() 
            => DB.InsertData<Salary>(_salaries, true, "Salary");
        public void UpdateTable<T>(List<T> list)
            => _table.UpdateTable<T>(list);

        [ContextMenu("Update Employee Salary Data")]
        public void UpdateEmployeeSalary()
        {
            if (_salaries.Count == 0) UpdateSalaryTable();
            if (_employees.Count == 0) UpdateEmployeeTable();

            foreach (Employee employee in _employees)
            {
                var amount = _salaries.FirstOrDefault(y => ((y.Department == employee.Department) && (y.Seniority == employee.Seniority))).Amount;
                if (amount > 0)
                    employee.SetSalary(amount);
            }
        }

        [ContextMenu("Apply all raises")]
        public void ApplyAllRaises()
        {
            if (_salaries.Count == 0) LoadSalaryData();
            if (_raises.Count == 0) LoadRaiseData();

            foreach (Salary sal in _salaries)
            {
                double raise = sal.Amount *
                    (_raises.FirstOrDefault(
                        x => x.Department == sal.Department && x.Seniority == sal.Seniority)
                        .Amount / 100);

                if(roundSalaries)
                    Math.Round(raise, 2, MidpointRounding.AwayFromZero);

                sal.Amount += raise;
            }
        }
 

    }

}