using UnityEngine;
using System;


namespace SalaryApp
{
    [Serializable]
    public class Position : IDatabaseObject
    {
        [SerializeField] private Department _department;
        [SerializeField] private Seniority _seniority;
        [SerializeField] private long _id;
        public String Department
        {
            get => _department.ToString().ToLower();
            set => _department = Enum.Parse<Department>(value, true);
        }

        public String Seniority
        {
            get => _seniority.ToString().ToLower();
            set => _seniority = Enum.Parse<Seniority>(value, true); 
        }

        public long ID { get => _id; set => _id = value; }
    }

}