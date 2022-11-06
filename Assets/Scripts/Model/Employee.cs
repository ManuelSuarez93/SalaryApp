using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace SalaryApp
{
    [Serializable]
    public class Employee : IDatabaseObject
    {
        [SerializeField] private long _id;
        [SerializeField] private string _name;
        [SerializeField] private long _posID; 
        [SerializeField] private double _salary;
        [SerializeField] private string _department; 
        [SerializeField] private string _seniority;   
        public long PosID { get => _posID; set => _posID = value; }
        public long ID { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Department { get => _department; set => _department = value; }
        public string Seniority { get => _seniority; set => _seniority = value; }
        public double Salary { get => _salary; set => _salary = value; }

        public static List<PropertyInfo> GetDBProperties() => Util.GetPropertyListWithout(typeof(Employee), new string[] { "Department", "Seniority", "Salary" });
    }

}