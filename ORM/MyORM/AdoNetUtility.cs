using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace MyORM
{
    public class AdoNetUtility
    {
        private SqlConnection _conn;
        public AdoNetUtility(string connectionString)
        {
            _conn = new SqlConnection(connectionString);
        }

        public void InsertQuery<T>(string tableName, T data)
        {
            if(_conn.State != ConnectionState.Open)
            {
                _conn.Open();
            }

            // Get the type of passed object 
            Type entityType = data.GetType();
            // Fetch all property info of that type
            PropertyInfo[] entityPropertyInfo = entityType.GetProperties();

            Dictionary<string, object> parameters = new();
            List<string> propertyNames = new();

            // storing the all property name and it's value for data object as a key, value pair
            foreach (PropertyInfo propertyInfo in entityPropertyInfo)
            {
                if (propertyInfo.GetValue(data) != null)
                {
                    propertyNames.Add(propertyInfo.Name);
                    parameters.Add(propertyInfo.Name, propertyInfo.GetValue(data));
                }
            }

            // entityFields contains table field name seperated by comma(id, name, age) 
            string entityFields = String.Join(",", propertyNames);

            // entityParams contains key names with '@', which name is stored in parameters dictionary as a key(@id, @name, @age)
            string entityParams = string.Join(",", propertyNames.Select(p => string.Concat("@", p)).ToList());


            string commandString = $"INSERT INTO {tableName}({entityFields}) VALUES({entityParams})";
            
            try
            {
                using (SqlCommand command = new SqlCommand(commandString, _conn))
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.Add(new SqlParameter(param.Key, param.Value));
                    }
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
