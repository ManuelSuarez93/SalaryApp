using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SalaryApp
{
    public class EmployeeManager : MonoBehaviour
    {
        [Header("TABLES")]
        [SerializeField] private List<Employee> _employees = new List<Employee>();
        [SerializeField] private List<Salary> _salaries = new List<Salary>();
        [SerializeField] private List<Raise> _raises = new List<Raise>();
        [SerializeField] private List<Position> _positions = new List<Position>();
        [Header("SETTINGS")]
        [SerializeField] private bool _initializeOnStart = true;
        [SerializeField] private bool _replaceDatabaseOnInit = false;
        [SerializeField] private bool _roundSalaries = true;
        [SerializeField] private int _decimalsToRound = 2;

        public IDatabase DB { get; set; } = new Database(SQL.DATABASE_NAME);
        public List<Employee> Employees => _employees;
        public List<Salary> Salaries => _salaries;
        public List<Raise> Raises => _raises;
        public List<Position> Positions => _positions;

        void Start()
        {
            if (_initializeOnStart)
                InitializeDatabase();
        }

        [ContextMenu("Create Database")]
        public void InitializeDatabase()
        {
            if (!DB.IsDatabaseActive() || _replaceDatabaseOnInit)
            {
                DB.ExecuteNonQuery(SQL.DROP_TABLE_QUERY);
                DB.ExecuteNonQuery(SQL.CREATE_TABLE_QUERY);
                DB.ExecuteNonQuery(SQL.POSITION_INSERT_DATA);
                DB.ExecuteNonQuery(SQL.EMPLOYEE_INSERT_DATA);
                DB.ExecuteNonQuery(SQL.SALARY_INSERT_DATA);
                DB.ExecuteNonQuery(SQL.RAISE_INSERT_DATA);
            }

            LoadPositionData();
            LoadSalaryData();
            LoadRaiseData();
            LoadEmployeeData();
        }


        [ContextMenu("Load Employee Data")]
        public void LoadEmployeeData()
        {
            _employees = DB.LoadData<Employee>();

            UpdateEmployeeSalary();
            UpdateEmployeePosition();
        }

        [ContextMenu("Load Salary Data")]
        public void LoadSalaryData()
        {
            _salaries = DB.LoadData<Salary>();
            foreach (Salary salary in _salaries)
            {
                Position pos = _positions.FirstOrDefault(y => y.ID == salary.PosID);
                salary.Department = pos.Department;
                salary.Seniority = pos.Seniority;
            }
        }

        [ContextMenu("Load Raise Data")]
        public void LoadRaiseData()
        {
             _raises = DB.LoadData<Raise>();
            foreach (Raise raise in _raises)
            {
                Position pos = _positions.FirstOrDefault(y => y.ID == raise.PosID);
                raise.Department = pos.Department;
                raise.Seniority = pos.Seniority;
            }
        }

        [ContextMenu(" Load Position Data")]
        public void LoadPositionData()
            => _positions = DB.LoadData<Position>();

        [ContextMenu("Insert Salary Data")]
        public void InsertSalaryDataIntoDatabase()
            => DB.InsertData<Salary>(_salaries, true, Salary.GetDBProperties(), "Salary");

        [ContextMenu("Update Employee Position Data")]
        public void UpdateEmployeePosition()
        {
            if (_positions.Count == 0) LoadPositionData(); 

            foreach (Employee employee in _employees)
            {
                Position pos = _positions.FirstOrDefault(y => y.ID == employee.PosID);
                employee.Department = pos.Department;
                employee.Seniority = pos.Seniority;
            }
        }

        [ContextMenu("Update Employee Salary Data")]
        public void UpdateEmployeeSalary()
        {
            if (_salaries.Count == 0) LoadSalaryData();
            if (_employees.Count == 0) LoadEmployeeData();

            foreach (Employee employee in _employees)
            {
                employee.Salary = _salaries.FirstOrDefault(y => y.PosID == employee.PosID).Amount;
            }
        }


        [ContextMenu("Apply all raises")]
        public void ApplyAllRaises()
        {
            if (_salaries.Count == 0) LoadSalaryData();
            if (_raises.Count == 0) LoadRaiseData();

            foreach (Salary sal in _salaries)
            {
                sal.Amount +=  (sal.Amount *
                    (_raises.FirstOrDefault(
                        x => x.PosID == sal.PosID)
                        .Amount / 100));

                if (_roundSalaries)
                    sal.Amount = Math.Round(sal.Amount, _decimalsToRound);
            }
        }
        [ContextMenu("Clear data")]
        public void ClearData()
        {
            _positions.Clear();
            _salaries.Clear();
            _employees.Clear();
            _raises.Clear();
        }

    }

}