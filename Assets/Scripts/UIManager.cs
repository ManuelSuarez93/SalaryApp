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
    [SerializeField] private Table _table;
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
    }

    [ContextMenu("Update Employee Table")]
    public void UpdateEmployeeTable()
       => _table.UpdateTable<Employee>(_employeeMgr.Employees, new string[] { "PosID"});

    [ContextMenu("Update Salary Table")]
    public void UpdateSalaryTable()
        => _table.UpdateTable<Salary>(_employeeMgr.Salaries);
    [ContextMenu("Update Raise Table")]
    public void UpdateRaiseTable()
        => _table.UpdateTable<Raise>(_employeeMgr.Raises);

    [ContextMenu("Clear table")]
    public void ClearTable()
        => _table.DeleteTableElements();
}
