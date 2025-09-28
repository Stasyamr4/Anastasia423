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
                        Console.WriteLine("Функция в разработке");
                        break;
                    case 4:
                        Console.WriteLine("Функция в разработке");
                        break;
                    case 5:
                        Console.WriteLine("Функция в разработке");
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
}