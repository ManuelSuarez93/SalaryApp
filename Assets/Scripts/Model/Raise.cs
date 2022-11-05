using UnityEngine;
using System;


namespace SalaryApp
{
    [Serializable]
    public class Raise : IDatabaseObject
    {
        private long _id;
        [SerializeField] private Department _department;
        [SerializeField] private Seniority _seniority;
        [SerializeField] private double _amount;

        public long ID { get => _id; set => _id = value;}
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

        public double Amount
        {
            get => _amount;
            set => _amount = value;
        }

    }

}