using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_app.Models
{
    internal class Product
    {
        private uint id;

        public uint Id
        { get { return id; } }

        public string Name { get; set; }

        public static uint lastProductId = 0;

        public Product()
        {
            id = ++lastProductId;
            Name = $"New product {id}";
        }

        public Product(string name)
        {
            id = ++lastProductId;
            Name = name;
        }

        public void Print() => Console.WriteLine($"Product Id: {Id}, Name: {Name}");

    }
}
