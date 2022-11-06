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
        private List<GameObject> elements;
         
        public void UpdateTable<T>(List<T> list, string[] show = null)
        {
            DeleteTableElements();
            elements = new List<GameObject>();

            Type temp = typeof(T); 
            GameObject row = Instantiate(_row, transform);

            elements.Add(row);
             
            foreach (PropertyInfo prop in temp.GetProperties())
            {
                if (show != null && show.Any(prop.Name.Contains)) continue;

                GameObject cell = Instantiate(_cell, row.transform);
                cell.GetComponent<Text>().text = prop.Name;
            }

            foreach(T obj in list)
            {
                GameObject newRow = Instantiate(_row, transform);
                foreach (PropertyInfo prop in temp.GetProperties())
                {
                    if (show != null && show.Any(prop.Name.Contains)) continue;

                    GameObject newCell = Instantiate(_cell, newRow.transform);
                    newCell.GetComponent<Text>().text = obj.GetType().GetProperties().FirstOrDefault(x => x.Name == prop.Name).GetValue(obj).ToString();
                }
                elements.Add(newRow);
            }

            
            row = Instantiate(_row, transform);

        }

        public void DeleteTableElements()
        {
            if(elements != null && elements.Count > 0)
            {
                foreach(GameObject e in elements)
                {
                    if (e)
                    {
                        for (int i = 0; i < e.transform.childCount; i++)
                        {
                            DestroyImmediate(e.transform.GetChild(i).gameObject);
                        }
                        DestroyImmediate(e);
                    }
                }
                elements.Clear();
            } 
        }
    }

}

