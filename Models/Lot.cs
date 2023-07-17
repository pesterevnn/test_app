using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_app.Models
{
    internal class Lot
    {
        private uint id;
        private uint id_product;
        private uint id_warehouse;

        public uint Id
        { get { return id; } }
        public uint Count { get; set; }
        public Product Product { get; set; }
        public Warehouse Warehouse { get; set; }
        public uint Id_product
        { get { return id_product; } }
        public uint Id_warehouse
        { get { return id_warehouse; } }

        public static uint lastLotId = 0;

        public Lot(Product product, Warehouse warehouse, uint count)
        {
            id = ++lastLotId;
            this.Product = product;
            this.Warehouse = warehouse;
            this.Count = count;
            id_product = product.Id;
            id_warehouse = warehouse.Id;
        }

        public void Print() => Console.WriteLine(
            $"Lot Id: {Id}, Pharmacy: {Warehouse.Pharmacy.Name}, " +
            $"Warehouse: {Warehouse.Name}, Product: {Product.Name}, Count: {Count}");
    }
}
