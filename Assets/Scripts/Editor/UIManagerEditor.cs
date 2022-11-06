using UnityEditor;
using UnityEngine;

namespace SalaryApp
{
#if UNITY_EDITOR
    [CustomEditor(typeof(UIManager))]
    public class UIManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            UIManager script = (UIManager)target;

            if (GUILayout.Button("Update Employee Table"))
                script.UpdateEmployeeTable();
            if (GUILayout.Button("Update Salary Table"))
                script.UpdateSalaryTable();
            if (GUILayout.Button("Update Raise Table"))
                script.UpdateRaiseTable();
            if (GUILayout.Button("Clear table"))
                script.ClearTable();
        }
    }
#endif
}