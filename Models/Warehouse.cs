using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_app.Models
{
    internal class Warehouse
    {
        private uint id;
        private uint id_pharmacy;

        public uint Id
        { get { return id; } }
        public string Name { get; set; }
        public Pharmacy Pharmacy { get; set; }
        public uint Id_pharmacy
        { get { return id_pharmacy; } }

        public static uint lastWarehouseId = 0;

        public Warehouse(Pharmacy pharmacy, string? name = null)
        {
            id = ++lastWarehouseId;
            if (name != null) Name = name;
            else Name = $"New Warehose {Id}";
            Pharmacy = pharmacy;
            id_pharmacy = pharmacy.Id;
        }

        public void Print() => Console.WriteLine($"Warehouse Id: {Id}, Name: {Name}, Pharmacy: {Pharmacy.Name}");
    }
}
