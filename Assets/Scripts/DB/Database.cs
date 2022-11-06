using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.IO; 
using System;
using System.Collections.Generic; 
using System.Reflection; 
using System.Linq; 

namespace SalaryApp
{ 
    public class Database : IDatabase
    {
        private string _databaseName;
        public string DatabaseName => _databaseName;

        public Database (string databaseName)
        {
            _databaseName = databaseName;
        }

        public string DBPath => $"{Application.persistentDataPath}/{_databaseName}.db";
        private List<List<IDatabaseObject>> objects = new List<List<IDatabaseObject>>();
        

        public void ExecuteNonQuery(string commandText)
        {
            using (SqliteConnection connection = new SqliteConnection($"URI=file:{DBPath};Pooling=False"))
            {
                SqliteCommand command;
                connection.Open();

                command = connection.CreateCommand();
                command.CommandText = commandText;
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
        }

        public void ExecuteNonQuery(SqliteCommand command)
        {
            using (SqliteConnection connection = new SqliteConnection($"URI=file:{DBPath};Pooling=False"))
            { 
                connection.Open();
                command.Connection = connection;
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
        }

        public void DeleteDatabase()
        { 
            if (IsDatabaseActive())
                File.Delete(DBPath);
        }

        public bool IsDatabaseActive() => File.Exists(DBPath);
        public List<T> LoadData<T>(string tableName = "" )
        {
            Type temp = typeof(T); 
            List<IDatabaseObject> data = new List<IDatabaseObject>();
            DataTable dt = new DataTable();

            using (IDbConnection connection = new SqliteConnection($"URI=file:{DBPath};"))
            {
                IDbCommand command;
                IDataReader reader;
                connection.Open();

                //Create table
                command = connection.CreateCommand();
                string name = string.IsNullOrWhiteSpace(tableName) ? temp.Name : tableName;
                command.CommandText = $"SELECT * FROM {name}";
                reader = command.ExecuteReader();


                dt.Load(reader);
                TransformDTToType<T>(data, dt, temp);

                command.Dispose();
                reader.Dispose();
                connection.Close(); 
            } 
            return data.Cast<T>().ToList();
        } 
        public void TransformDTToType<T>(List<IDatabaseObject> data, DataTable dt, Type temp)
        {
            foreach (DataRow row in dt.Rows)
            {
                T newItem = Activator.CreateInstance<T>();

                foreach (DataColumn column in dt.Columns)
                {
                    foreach (PropertyInfo pro in temp.GetProperties())
                    {
                        if (pro.Name.ToLower() == column.ColumnName.ToLower())
                        {
                            pro.SetValue(newItem, row[column.ColumnName], null);
                        }
                    }
                }
                data.Add((IDatabaseObject)newItem);

            }
        }

        public void InsertData<T>(List<T> newData, bool replace = false,List<PropertyInfo> properties = null, string tableName = "")
        {
            SqliteCommand cmd = new SqliteCommand();
            Type temp = typeof(T);

            if (properties == null)
                properties = temp.GetProperties().ToList();
             
            if (string.IsNullOrWhiteSpace(tableName))
                tableName = temp.Name;

            string insert = "INSERT " + (replace ? "OR REPLACE "  : "" )+ $"INTO {tableName} ";

            string tables = "";
            string values = "";
            for (int i = 0; i < properties.Count(); i++)
            {
                tables += $"{properties[i].Name}" + (i == properties.Count() - 1 ? "" : ",");
                values += $"@value{i}" + (i == properties.Count() - 1 ? "" : ",");
            }

            insert += $"({tables}) VALUES({values})";
            foreach(T item in newData)
            { 
                cmd.CommandText = insert;
                for (int i = 0; i < properties.Count(); i++) 
                    cmd.Parameters.AddWithValue($"@value{i}", properties[i].GetValue(item));  

                ExecuteNonQuery(cmd);
            }
             

             
             
        }
    }

}