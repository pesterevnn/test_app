using test_app.Models;

Dictionary<uint, Product> products = new Dictionary<uint, Product>();
Dictionary<uint, Pharmacy> pharmacies = new Dictionary<uint, Pharmacy>();
Dictionary<uint, Warehouse> warehouses = new Dictionary<uint, Warehouse>();
Dictionary<uint, Lot> lots = new Dictionary<uint, Lot>();

List<string> gMenu = new List<string>
{
    "1. Аптеки",
    "2. Склады",
    "3. Товары",
    "4. Партии",
    "5. Список товаров по аптеке",
    "0. Выход"
};

List<string> subMenu1 = new List<string>
{
    "1. Создать",
    "2. Удалить",
    "3. Показать весь список",
    "0. Вернуться выше"
};

List<string> subMenu2 = new List<string>
{
    "1. Сформировать",
    "0. Вернуться выше"
};

string? menuNumber = null;
string? subMenuNumber = null;

//создаем начальные объекты для работы
var product = new Product("Аспирин");
products.Add(product.Id, product);
product = new Product("Парацетамол");
products.Add(product.Id, product);

var pharmacy = new Pharmacy("Аптека на Советской", "Город, ул.Советская", "+7-999-999-99-99");
pharmacies.Add(pharmacy.Id, pharmacy);
var warehouse = new Warehouse(pharmacy, "Склад 1");
warehouses.Add(warehouse.Id, warehouse);

var lot = new Lot(products[1], warehouse, 10);
lots.Add(lot.Id, lot);
lot = new Lot(products[2], warehouse, 20);
lots.Add(lot.Id, lot);

warehouse = new Warehouse(pharmacy, "Склад 2");
warehouses.Add(warehouse.Id, warehouse);
lot = new Lot(products[1], warehouse, 15);
lots.Add(lot.Id, lot);
lot = new Lot(products[2], warehouse, 25);
lots.Add(lot.Id, lot);

pharmacy = new Pharmacy("Аптека на Ленина", "Город, ул.Ленина", "+7-911-111-11-11");
pharmacies.Add(pharmacy.Id, pharmacy);
warehouse = new Warehouse(pharmacy, "Склад малый");
warehouses.Add(warehouse.Id, warehouse);
lot = new Lot(products[1], warehouse, 50);
lots.Add(lot.Id, lot);
lot = new Lot(products[2], warehouse, 70);
lots.Add(lot.Id, lot);

warehouse = new Warehouse(pharmacy, "Склад большой");
warehouses.Add(warehouse.Id, warehouse);
lot = new Lot(products[1], warehouse, 55);
lots.Add(lot.Id, lot);
lot = new Lot(products[2], warehouse, 75);
lots.Add(lot.Id, lot);

bool work = true;
bool chek_input_value;
while (work)
{
    Console.Clear();

    Console.WriteLine("Выберите раздел:");
    PrintMenu(gMenu);

    var pressedKey = Console.ReadLine();
    uint menuNum;

    chek_input_value = CheckInputMenuNum(pressedKey, out menuNum);

    if (!chek_input_value)
    {
        Console.WriteLine("\nТакого меню не существует, хотите повторить? y/n: ");
        string? lets_again = Console.ReadLine();
        if (lets_again != "y") work = false;
    }
    else
    {
        bool not_exit = true;
        while (not_exit && chek_input_value)
        {
            if (menuNum == 0)
            {
                not_exit = false;
            }
            else if (chek_input_value)
            {
                Console.WriteLine("Выберите действие:");
                if (menuNum == 5)
                    PrintMenu(subMenu2);
                else
                    PrintMenu(subMenu1);
                subMenuNumber = Console.ReadLine();
                if (subMenuNumber == "0")
                    Console.Clear();
                else break;
            }
        }

        if (menuNum != 0 && chek_input_value)
        {
            if (menuNum != 5)
                Console.WriteLine($"\nМеню {gMenu.ElementAt((int)menuNum - 1)}, " +
                $"подменю {subMenu1.ElementAt(int.Parse(subMenuNumber) - 1)}");
            else
                Console.WriteLine($"\nМеню {gMenu.ElementAt((int)menuNum - 1)}, " +
                $"подменю {subMenu2.ElementAt(int.Parse(subMenuNumber) - 1)}");

            Action? do_func;

            switch (menuNum + subMenuNumber)
            {
                case "11":
                    do_func = CreatePharmacy;
                    break;
                case "12":
                    do_func = DeletePharmacy;
                    break;
                case "13":
                    do_func = ShowPharmacies;
                    break;
                case "21":
                    do_func = CreateWarehouse;
                    break;
                case "22":
                    do_func = DeleteWarehouse;
                    break;
                case "23":
                    do_func = ShowWarehouses;
                    break;
                case "31":
                    do_func = CreateProduct;
                    break;
                case "32":
                    do_func = DeleteProduct;
                    break;
                case "33":
                    do_func = ShowProducts;
                    break;
                case "41":
                    do_func = CreateLot;
                    break;
                case "42":
                    do_func = DeleteLot;
                    break;
                case "43":
                    do_func = ShowLots;
                    break;
                case "51":
                    do_func = OpenStat;
                    break;
                default:
                    do_func = NotFound;
                    break;
            }

            do_func();

            Console.Write("\nПродолжим работу? y/n: ");
            string? lets_again = Console.ReadLine();
            if (lets_again != "y") work = false;
        }
        else work = false;
    }
}

Console.WriteLine("\nРабота завершена! \nНажмите любую клавишу ...");
Console.ReadKey();

bool CheckInputMenuNum(string input_value, out uint menuNum)
{
    bool res = false;

    var parse_result = uint.TryParse(input_value, out menuNum);
    if (parse_result && menuNum <= 5)
        res = true;

    return res;
}

void PrintMenu(List<string> menu)
{
    foreach (var item in menu)
    {
        Console.WriteLine(item);
    }
}

void NotFound() => Console.WriteLine("Такой вариант не найден :(");

void ShowPharmacies()
{
    foreach (var item in pharmacies)
        item.Value.Print(); 
}

void ShowWarehouses()
{
    foreach (var item in warehouses)
        item.Value.Print();
}

void ShowProducts()
{
    foreach (var item in products)
        item.Value.Print();
}

void ShowLots()
{
    foreach (var item in lots)
        item.Value.Print();
}

void CreateProduct()
{
    Console.Write("Наименование товара: ");
    string productName = Console.ReadLine();
    var product = new Product(productName);
    Console.Write("Создан: ");
    product.Print();
    products.Add(product.Id, product);
}

void CreatePharmacy()
{
    Console.Write("Наименование аптеки: ");
    string pharmacyName = Console.ReadLine();
    Console.Write("Адрес: ");
    string? pharmacyAdress = Console.ReadLine();
    Console.Write("Телефон: ");
    string? pharmacyTel = Console.ReadLine();
    var pharmacy = new Pharmacy(pharmacyName, pharmacyAdress, pharmacyTel);
    Console.Write("Создан: ");
    pharmacy.Print();
    pharmacies.Add(pharmacy.Id, pharmacy);
}

void CreateWarehouse()
{
    Console.WriteLine("\nПеречень аптек:");
    ShowPharmacies();
    Console.Write("\nВыберите номер (id) аптеки из перечня выше: ");
    string pharmacyNum = Console.ReadLine();
    var pharmacy = pharmacies.GetValueOrDefault(uint.Parse(pharmacyNum));
    
    Console.Write("Наименование склада: ");
    string whouseName = Console.ReadLine();

    Warehouse warehouse = new Warehouse(pharmacy, whouseName);
    Console.Write("Создан: ");
    warehouse.Print();
    warehouses.Add(warehouse.Id, warehouse);
}

void CreateLot()
{
    Console.WriteLine("\nПеречень аптек:");
    ShowPharmacies();
    Console.Write("\nВыберите номер (id) аптеки из перечня выше: ");
    string pharmacyNum = Console.ReadLine();
    var pharmacy = pharmacies.GetValueOrDefault(uint.Parse(pharmacyNum));

    var whouses = warehouses.Values.Where(w => w.Pharmacy == pharmacy).ToList();
    Console.WriteLine("\nПеречень складов:");
    ShowWarehouses();
    Console.Write("\nВыберите номер (id) склада из перечня выше: ");
    string whouseNum = Console.ReadLine();
    var whouse = warehouses.GetValueOrDefault(uint.Parse(whouseNum));

    Console.WriteLine("\nПеречень товаров:");
    ShowProducts();

    Console.Write("\nВыберите номер (id) товара: ");
    string productNum = Console.ReadLine();
    var product = products.GetValueOrDefault(uint.Parse(productNum));

    Console.Write("\nУкажите количество товара: ");
    string productCount = Console.ReadLine();
    uint count = uint.Parse(productCount);

    var lot = new Lot(product, whouse, count);
    Console.Write("Создан: ");
    lot.Print();
    lots.Add(lot.Id, lot);
}

void OpenStat()
{
    Console.WriteLine("\nПеречень аптек:");
    ShowPharmacies();
    Console.Write("\nВыберите номер (id) аптеки из перечня выше: ");
    string pharmacyNum = Console.ReadLine();
    var pharmacy = pharmacies.GetValueOrDefault(uint.Parse(pharmacyNum));

    //весь список товаров и его количество в выбранной аптеке
    var products_by_pharmacy = lots.Values.Where(l => l.Warehouse.Pharmacy == pharmacy).
        GroupBy(l => l.Product, l => l.Count).ToList();

    foreach (var item in products_by_pharmacy)
        Console.WriteLine($"{item.Key.Name}: {item.Sum(el => el)}");
}

void DeleteProduct()
{
    Console.WriteLine("\nПеречень товаров:");
    ShowProducts();
    Console.Write("\nВыберите номер (id) товара из перечня выше:");
    string productNum = Console.ReadLine();
    uint product_id = uint.Parse(productNum);
    var lots_by_product = lots.Where(l => l.Value.Id_product == product_id).ToList();
    foreach (var item in lots_by_product)
        lots.Remove(item.Key);
    products.Remove(product_id);
    Console.WriteLine($"Товар с Id {product_id} и все связанные с ним партии во всех аптеках успешно удалены");
}

void DeletePharmacy()
{
    Console.WriteLine("\nПеречень аптек:");
    ShowPharmacies();
    Console.Write("\nВыберите номер (id) аптеки из перечня выше:");
    string pharmaNum = Console.ReadLine();
    uint pharma_id = uint.Parse(pharmaNum);
    var whouses_by_pharma = warehouses.Where(wh => wh.Value.Id_pharmacy == pharma_id).ToList();
    foreach (var item in whouses_by_pharma)
        RemoveWarehouse(item.Key);
    pharmacies.Remove(pharma_id);
    Console.WriteLine($"Аптека с Id {pharma_id} и все её склады и партии в них успешно удалены");
}

void DeleteWarehouse()
{
    Console.WriteLine("\nПеречень складов:");
    ShowWarehouses();
    Console.Write("\nВыберите номер (id) склада из перечня выше: ");
    string whouseNum = Console.ReadLine();
    uint whouse_id = uint.Parse(whouseNum);
    RemoveWarehouse(whouse_id);
    Console.WriteLine($"Склад c Id {whouseNum} и все его партии успешно удалены");
}

void RemoveWarehouse(uint id)
{
    var lots_by_whouse = lots.Where(l => l.Value.Id_warehouse == id);
    foreach (var item in lots_by_whouse)
        lots.Remove(item.Key);
    warehouses.Remove(id);
}

void DeleteLot()
{
    Console.WriteLine("\nПеречень партий:");
    ShowLots();
    Console.Write("\nВыберите номер (id) партии из перечня выше: ");
    string lotNum = Console.ReadLine();
    lots.Remove(uint.Parse(lotNum));
    Console.WriteLine($"Партия с Id {lotNum} успешно удалена");
}