using Assignment3;
using MyORM;

var connectionString = "Server=.\\SQLEXPRESS;Database=TestDB;User Id=test;Password=12345; Trust Server Certificate = True ";
MyORM<int, Student<int>> myORM = new(connectionString);

Student<int> s1 = new Student<int>() { id = 3, name = "Sazin", age = 22};
myORM.Insert(s1);
