using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace SalaryApp
{
    public interface IDatabase
    {
        public string DatabaseName { get; }
        public string DBPath { get; }
        public void ExecuteNonQuery(string command); 
        public void DeleteDatabase();
        public bool IsDatabaseActive();
        public List<T> LoadData<T>(string query = "");
        public void InsertData<T>(List<T> newData,bool replace = false, List<PropertyInfo> properties = null, string tableName = "");
        public void TransformDTToType<T>(List<IDatabaseObject> data, DataTable dt, Type temp);
    }

}