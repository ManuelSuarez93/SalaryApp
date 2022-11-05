using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Collections;
using UnityEngine;
using System;


namespace SalaryApp
{
    [Serializable]
    public class Employee : IDatabaseObject
    {
        private long _id;
        [SerializeField] private string _name;
        [SerializeField] private Department _department;
        [SerializeField] private Seniority _seniority;
        [SerializeField] private double _salary;
        public double GetSalary() => _salary;
        public void SetSalary(double newSalary) => _salary = newSalary;

        public long ID { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; } 
        public String Department
        {
            get => _department.ToString().ToLower();
            set => _department = Enum.Parse<Department>(value, true);
        }
        public String Seniority
        {
            get => _seniority.ToString().ToLower();
            set
            {
                _seniority = Enum.Parse<Seniority>(value, true);
                
                if (_department == SalaryApp.Department.CEO)
                    _seniority = SalaryApp.Seniority.CEO;
                else if (_department != SalaryApp.Department.CEO && value.ToLower() == "ceo")
                    _seniority = SalaryApp.Seniority.Senior;


            }
        }
         


    }

}