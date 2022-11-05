using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Linq;

namespace SalaryApp
{
    public class Table : MonoBehaviour
    {
        [SerializeField] private GameObject _row, _cell;

        public void UpdateTable<T>(List<T> list)
        { 
            for (int i = 0; i < transform.childCount; i++)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }

            Type temp = typeof(T); 
            var row = Instantiate(_row, transform);

            foreach (PropertyInfo prop in temp.GetProperties())
            {
                var cell = Instantiate(_cell, row.transform);
                cell.GetComponent<Text>().text = prop.Name;
            }

            foreach(T obj in list)
            { 
                var newRow = Instantiate(_row, transform);
                foreach (PropertyInfo prop in temp.GetProperties())
                {
                    var newCell = Instantiate(_cell, newRow.transform);
                    newCell.GetComponent<Text>().text = obj.GetType().GetProperties().FirstOrDefault(x => x.Name == prop.Name).GetValue(obj).ToString();
                }
            }

        }
    }

}

