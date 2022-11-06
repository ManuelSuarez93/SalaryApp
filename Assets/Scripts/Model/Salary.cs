using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SalaryApp
{
    [Serializable]
    public class Salary: IDatabaseObject
    {
        [SerializeField] private long _id;
        [SerializeField] private long _posid;
        [SerializeField] private string _department;
        [SerializeField] private string _seniority;
        [SerializeField] private double _amount;

        public long ID { get => _id; set => _id = value; }
          
        public long PosID { get => _posid; set => _posid = value; }
        public double Amount { get => _amount; set => _amount = value; }
        public string Department { get => _department; set => _department = value; }
        public string Seniority { get => _seniority; set => _seniority = value; }

        public static List<PropertyInfo> GetDBProperties()
            => Util.GetPropertyListWithout(typeof(Salary), new string[] { "Department", "Seniority" });
    }

}