using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_app.Models
{
    internal class Pharmacy
    {
        private uint id;

        public uint Id
        { get { return id; } }
        public string Name { get; set; }
        public string? Adress { get; set; }
        public string? Telephone { get; set; }

        public static uint lastPharmacyId = 0;

        public Pharmacy()
        {
            id = ++lastPharmacyId;
            Name = $"New pharmacy {id}";
        }

        public Pharmacy(string name, string? adress = null, string? telephone = null)
        {
            id = ++lastPharmacyId;
            Name = name;
            Adress = adress;
            Telephone = telephone;
        }

        public void Print() => Console.WriteLine($"Pharmacy Id: {Id}, Name: {Name}, Adress: {Adress}, Tel.: {Telephone}");
    }
}
