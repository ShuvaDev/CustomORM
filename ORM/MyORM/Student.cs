using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    public class Student<T> : IId<T>
    {
        public T id { get; set; }
        public string? name { get; set; }
        public int? age { get; set; }
    }
}
