using MyORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    public class MyORM<G, T>
        where T : IId<G>
    {
        private AdoNetUtility adoNetUtility;
        public MyORM(string connectionString) 
        {
            adoNetUtility = new AdoNetUtility(connectionString);
        }
        public void Insert(T item)
        {
            string tableName = item.GetType().Name.Substring(0, item.GetType().Name.Length - 2);
            adoNetUtility.InsertQuery(tableName, item);
        }

        public void Update(T item)
        {

        }
        public void Delete(T item)
        {

        }
        public void Delete(G id)
        {

        }
        public void GetById(G id)
        {

        }
        public void GetAll()
        {

        }
    }
}
