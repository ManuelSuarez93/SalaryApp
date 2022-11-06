using SalaryApp;
using System; 
using System.Collections.Generic;
using System.Linq;
using UnityEngine; 
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{ 

    [SerializeField] private TMP_InputField _inputFieldName, _inputFieldID;
    [SerializeField] private TMP_Dropdown _dropDownPosition;
    [SerializeField] private TMP_Text _tableName;
    [SerializeField] private Table _table;
    [SerializeField] private TMP_Text _totalEmployeeSalary; 
    [SerializeField] private EmployeeManager _employeeMgr;

    private int _currentPosition;

    private void Start()
    {
        _dropDownPosition.AddOptions(_employeeMgr.Positions.Select(x => x.Department + " - " + x.Seniority).ToList());
    }
     
    public void AddEmployee()
    { 
        Employee employee = new Employee();

        employee.ID = Convert.ToInt64(_inputFieldID.text); 
        employee.Name = _inputFieldName.text;
        employee.PosID = _dropDownPosition.value + 1;

        _employeeMgr.DB.InsertData<Employee>(new List<Employee> { employee }, true, Employee.GetDBProperties(),"Employee");
        _employeeMgr.LoadEmployeeData();
    }

    [ContextMenu("Update Employee Table")]
    public void UpdateEmployeeTable()
    {
        ChangeTableName("Employees");
        _table.UpdateTable<Employee>(_employeeMgr.Employees, new string[] { "PosID"});
        TotalEmployees();
    }

    [ContextMenu("Update Salary Table")]
    public void UpdateSalaryTable()
    {
        ChangeTableName("Salaries");
        _table.UpdateTable<Salary>(_employeeMgr.Salaries);

    }
    [ContextMenu("Update Raise Table")]
    public void UpdateRaiseTable()
    {
        ChangeTableName("Raises");
        _table.UpdateTable<Raise>(_employeeMgr.Raises);
    }
     
    public void UpdatePositionTable()
    {
        ChangeTableName("Positions");
        _table.UpdateTable<Position>(_employeeMgr.Positions);
    }
    private void ChangeTableName(string name) => _tableName.text = $"Table: {name}";
    [ContextMenu("Clear table")]
    public void ClearTable()
        => _table.DeleteTableElements();


    public void TotalEmployees()
    {
        double salary = 0; 
        foreach(Employee employee in _employeeMgr.Employees) 
            salary += employee.Salary; 

        _totalEmployeeSalary.text = $"Total Employee Salaries: US$ {salary}";
    }
}
