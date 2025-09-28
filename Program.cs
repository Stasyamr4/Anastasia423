using System;
using System.Collections.Generic;

class Program
{
    enum categories
    {
        food = 1,
        groceries,
        careProducts
    }

    class Product
    {
        public int id;
        public string name;
        public double price;
        public int countProd = 0;
        public bool isprod;
        public int ProdCateg;

        public Product(int newId)
        {
            id = newId;
        }

        public void InputProductData()
        {
            Console.WriteLine("Введите название товара: ");
            while (true)
            {
                name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Введите название товара еще раз!");
                }
                else break;
            }

            Console.WriteLine("Введите цену товара: ");
            while (true)
            {
                try
                {
                    price = Convert.ToDouble(Console.ReadLine());
                    if (price <= 0)
                    {
                        Console.WriteLine("Цена должна быть больше 0, введите еще раз!");
                    }
                    else break;
                }
                catch
                {
                    Console.WriteLine("Некорректный ввод, введите число!");
                }
            }

            Console.WriteLine("Введите количество товаров: ");
            while (true)
            {
                try
                {
                    countProd = Convert.ToInt32(Console.ReadLine());
                    if (countProd < 0)
                    {
                        Console.WriteLine("Количество не может быть отрицательным, введите еще раз!");
                    }
                    else break;
                }
                catch
                {
                    Console.WriteLine("Некорректный ввод, введите целое число!");
                }
            }

            isprod = countProd > 0;

            Console.WriteLine("Введите категорию товара от 1 до 3: ");
            while (true)
            {
                try
                {
                    ProdCateg = Convert.ToInt32(Console.ReadLine());
                    if (ProdCateg < 1 || ProdCateg > 3)
                    {
                        Console.WriteLine("Неправильно введена категория, попробуйте еще раз!");
                    }
                    else break;
                }
                catch
                {
                    Console.WriteLine("Некорректный ввод, введите число от 1 до 3!");
                }
            }
        }

        public void PrintProduct()
        {
            string categoryName = ((categories)ProdCateg).ToString();
            string availability = isprod ? "Да" : "Нет";

            Console.WriteLine($"ID: {id}, Название: {name}, " +
                             $"Цена: {price:F2}, Количество: {countProd}, " +
                             $"В наличии: {availability}, Категория: {categoryName}");
        }
    }


    static List<Product> products = new List<Product>();
    static int nextId = 1;

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n=== МЕНЮ ===");
            Console.WriteLine("1. Добавить товар");
            Console.WriteLine("2. Удалить товар");
            Console.WriteLine("3. Заказать поставку товара");
            Console.WriteLine("4. Продать товар");
            Console.WriteLine("5. Поиск товаров");
            Console.WriteLine("6. Показать все товары");
            Console.WriteLine("7. Выход");
            Console.Write("Выберите действие: ");

            try
            {
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddProduct();
                        break;
                    case 2:
                        DeleteProduct();
                        break;
                    case 3:
                        OrderSupply();
                        break;
                    case 4:
                        SellProduct();
                        break;
                    case 5:
                        SearchProducts();
                        break;
                    case 6:
                        ShowAllProducts();
                        break;
                    case 7:
                        return;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Ошибка ввода!");
            }
        }
    }


    static void AddProduct()
    {
        Product newProduct = new Product(nextId++);
        newProduct.InputProductData();
        products.Add(newProduct);
        Console.WriteLine("\nТовар успешно добавлен!");
        newProduct.PrintProduct();
    }
    static void ShowAllProducts()
    {
        if (products.Count == 0)
        {
            Console.WriteLine("Товаров нет!");
            return;
        }

        Console.WriteLine("\n=== ВСЕ ТОВАРЫ ===");
        foreach (var product in products)
        {
            product.PrintProduct();
        }
    }
    static void DeleteProduct()
    {

        if (products.Count == 0)
        {
            Console.WriteLine("Список товаров пуст! Нечего удалять.");
            return;
        }


        Console.WriteLine("\n=== СПИСОК ТОВАРОВ ===");
        ShowAllProducts();


        Console.Write("\nВведите ID товара для удаления: ");

        try
        {
            int idToDelete = Convert.ToInt32(Console.ReadLine());


            Product productToDelete = products.Find(p => p.id == idToDelete);

            if (productToDelete != null)
            {

                Console.WriteLine($"Вы действительно хотите удалить товар: {productToDelete.name}?");
                Console.Write("Введите 'да' для подтверждения: ");
                string confirmation = Console.ReadLine();

                if (confirmation.ToLower() == "да")
                {
                    products.Remove(productToDelete);
                    Console.WriteLine($"Товар '{productToDelete.name}' успешно удален!");
                }
                else
                {
                    Console.WriteLine("Удаление отменено.");
                }
            }
            else
            {
                Console.WriteLine($"Товар с ID {idToDelete} не найден!");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Ошибка! Нужно ввести число (ID товара).");
        }
    }
    static void OrderSupply()
    {
        if (products.Count == 0)
        {
            Console.WriteLine("Товаров нет! Сначала добавьте товары.");
            return;
        }


        Console.WriteLine("\n=== ВСЕ ТОВАРЫ ===");
        ShowAllProducts();


        Console.Write("Введите ID товара для заказа поставки: ");

        try
        {
            int idToRestock = Convert.ToInt32(Console.ReadLine());


            bool found = false;
            foreach (Product product in products)
            {
                if (product.id == idToRestock)
                {
                    found = true;


                    Console.Write($"Сколько единиц товара '{product.name}' заказать?: ");
                    int quantity = Convert.ToInt32(Console.ReadLine());

                    if (quantity > 0)
                    {

                        product.countProd += quantity;
                        product.isprod = true;

                        Console.WriteLine($"Поставка успешно заказана! Теперь товара '{product.name}' в наличии: {product.countProd} шт.");
                    }
                    else
                    {
                        Console.WriteLine("Количество должно быть больше 0!");
                    }
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine($"Товар с ID {idToRestock} не найден!");
            }
        }
        catch
        {
            Console.WriteLine("Ошибка! Нужно ввести число.");
        }
    }
    static void SellProduct()
    {
        if (products.Count == 0)
        {
            Console.WriteLine("Товаров нет! Нечего продавать.");
            return;
        }

        Console.WriteLine("\n=== ВСЕ ТОВАРЫ ===");
        ShowAllProducts();


        Console.Write("Введите ID товара для продажи: ");

        try
        {
            int idToSell = Convert.ToInt32(Console.ReadLine());


            bool found = false;
            foreach (Product product in products)
            {
                if (product.id == idToSell)
                {
                    found = true;


                    if (!product.isprod || product.countProd == 0)
                    {
                        Console.WriteLine($"Товар '{product.name}' отсутствует на складе!");
                        return;
                    }


                    Console.WriteLine($"Товар: {product.name}");
                    Console.WriteLine($"В наличии: {product.countProd} шт.");
                    Console.WriteLine($"Цена за шт.: {product.price:F2} руб.");


                    Console.Write("Сколько единиц продать?: ");
                    int quantity = Convert.ToInt32(Console.ReadLine());

                    if (quantity <= 0)
                    {
                        Console.WriteLine("Количество должно быть больше 0!");
                    }
                    else if (quantity > product.countProd)
                    {
                        Console.WriteLine($"Недостаточно товара! В наличии только {product.countProd} шт.");
                    }
                    else
                    {
                        product.countProd -= quantity;


                        product.isprod = product.countProd > 0;


                        double totalPrice = quantity * product.price;

                        Console.WriteLine($"Продажа успешно выполнена!");
                        Console.WriteLine($"Продано: {quantity} шт. товара '{product.name}'");
                        Console.WriteLine($"Общая стоимость: {totalPrice:F2} руб.");
                        Console.WriteLine($"Осталось на складе: {product.countProd} шт.");
                    }
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine($"Товар с ID {idToSell} не найден!");
            }
        }
        catch
        {
            Console.WriteLine("Ошибка! Нужно ввести число.");
        }
    }
    static void SearchProducts()
    {
        if (products.Count == 0)
        {
            Console.WriteLine("Товаров нет! Нечего искать.");
            return;
        }

        Console.WriteLine("\n=== ПОИСК ТОВАРОВ ===");
        Console.WriteLine("1. Поиск по ID (коду)");
        Console.WriteLine("2. Поиск по названию");
        Console.WriteLine("3. Поиск по категории");
        Console.Write("Выберите тип поиска: ");

        try
        {
            int searchType = Convert.ToInt32(Console.ReadLine());

            switch (searchType)
            {
                case 1: // Поиск по ID
                    SearchById();
                    break;
                case 2: // Поиск по названию
                    SearchByName();
                    break;
                case 3: // Поиск по категории
                    SearchByCategory();
                    break;
                default:
                    Console.WriteLine("Неверный выбор! Введите 1, 2 или 3.");
                    break;
            }
        }
        catch
        {
            Console.WriteLine("Ошибка! Нужно ввести число.");
        }
    }

    // Поиск по ID
    static void SearchById()
    {
        Console.Write("Введите ID товара: ");
        try
        {
            int searchId = Convert.ToInt32(Console.ReadLine());
            bool found = false;

            foreach (Product product in products)
            {
                if (product.id == searchId)
                {
                    Console.WriteLine("\n=== НАЙДЕН ТОВАР ===");
                    product.PrintProduct();
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine($"Товар с ID {searchId} не найден!");
            }
        }
        catch
        {
            Console.WriteLine("Ошибка! Нужно ввести число.");
        }
    }

    // Поиск по названию
    static void SearchByName()
    {
        Console.Write("Введите название товара (или часть названия): ");
        string searchName = Console.ReadLine().ToLower(); // Приводим к нижнему регистру

        List<Product> foundProducts = new List<Product>();

        foreach (Product product in products)
        {
            if (product.name.ToLower().Contains(searchName))
            {
                foundProducts.Add(product);
            }
        }

        if (foundProducts.Count > 0)
        {
            Console.WriteLine($"\n=== НАЙДЕНО ТОВАРОВ: {foundProducts.Count} ===");
            foreach (Product product in foundProducts)
            {
                product.PrintProduct();
            }
        }
        else
        {
            Console.WriteLine($"Товары с названием '{searchName}' не найдены!");
        }
    }

    // Поиск по категории
    static void SearchByCategory()
    {
        Console.WriteLine("Выберите категорию:");
        Console.WriteLine("1. Еда (food)");
        Console.WriteLine("2. Бакалея (groceries)");
        Console.WriteLine("3. Товары для ухода (careProducts)");
        Console.Write("Введите номер категории: ");

        try
        {
            int categoryNumber = Convert.ToInt32(Console.ReadLine());

            if (categoryNumber < 1 || categoryNumber > 3)
            {
                Console.WriteLine("Неверный номер категории! Введите 1, 2 или 3.");
                return;
            }

            List<Product> foundProducts = new List<Product>();

            foreach (Product product in products)
            {
                if (product.ProdCateg == categoryNumber)
                {
                    foundProducts.Add(product);
                }
            }

            if (foundProducts.Count > 0)
            {
                string categoryName = ((categories)categoryNumber).ToString();
                Console.WriteLine($"\n=== ТОВАРЫ В КАТЕГОРИИ '{categoryName}' ===");
                foreach (Product product in foundProducts)
                {
                    product.PrintProduct();
                }
            }
            else
            {
                string categoryName = ((categories)categoryNumber).ToString();
                Console.WriteLine($"В категории '{categoryName}' товаров не найдено!");
            }
        }
        catch
        {
            Console.WriteLine("Ошибка! Нужно ввести число.");
        }



    }
}