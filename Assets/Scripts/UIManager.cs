using SalaryApp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private TMP_InputField _inputFieldName, _inputFieldID; 
    [SerializeField] private TMP_Dropdown _dropDownDepartment, _dropDownSeniority; 

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        } 
        Instance = this; 
    }

    private void Start()
    {
        _dropDownDepartment.AddOptions(Enum.GetNames(typeof(Department)).ToList());
        _dropDownSeniority.AddOptions(Enum.GetNames(typeof(Seniority)).ToList());
    }
    public void AddEmployee()
    { 
        Employee employee = new Employee();

        employee.ID = Convert.ToInt64(_inputFieldID.text);
        employee.Name = _inputFieldName.text;
        employee.Department = _dropDownDepartment.options[_dropDownDepartment.value].text;
        employee.Seniority = _dropDownSeniority.options[_dropDownSeniority.value].text;

        EmployeeManager.Instance.DB.InsertData<Employee>(new List<Employee> { employee }, true, "Employee");
    }
}
