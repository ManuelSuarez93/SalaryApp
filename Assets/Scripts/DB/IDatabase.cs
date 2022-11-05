using System.Collections.Generic;
using System.Data;

namespace SalaryApp
{
    public interface IDatabase
    {
        public string DatabaseName { get; }
        public string DBPath { get; }
        public void ExecuteNonQuery(string command); 
        public void DeleteDatabase();
        public List<T> LoadData<T>(string query = "");
        public void InsertData<T>(List<T> newData,bool replace = false, string tableName = "");
    }

}