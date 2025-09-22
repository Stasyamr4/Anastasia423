using System;

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
        int id = 0;
        string name;
        double price;
        int countProd = 0;
        bool isprod;
        int ProdCateg;
        public void GetProduct()
        {
            id += 1;
            Console.WriteLine("Введите назваание товара: ");
            while (true)
            {
                name = Console.ReadLine();
                if (name == null)
                {
                    Console.WriteLine("введите название товара еще раз!");
                }
                else break;
            }
            Console.WriteLine("Введите цену товара: ");
            while (true)
            {
                price = Convert.ToDouble(Console.ReadLine());
                if ((price == 0) || (price == null))
                {
                    Console.WriteLine("введите цену товара еще раз!");
                }
                else break;
            }
            Console.WriteLine("Введите количество товаров: ");
            while (true)
            {
                countProd = Convert.ToInt32(Console.ReadLine());
                if ((countProd == 0) || (countProd == null))
                {
                    Console.WriteLine("введите количество товаров еще раз!");
                }
                else break;
            }
            if (countProd > 0)
            {
                isprod = true;

            }
            else
            {
                isprod = false;
            }
            ;
            Console.WriteLine("Введите категорию товара от 1 до 3: ");
            while (true)
            {
                ProdCateg = Convert.ToInt32(Console.ReadLine());
                if ((ProdCateg == null) || (ProdCateg < 1) || (ProdCateg > 3))
                {
                    Console.WriteLine("неправвильно введена категория, попробуйте еще раз!");
                }
                else break;
            }
            categories selectedCategory = (categories)ProdCateg;
            switch (selectedCategory)
            {
                case categories.food:
                    {
                        Console.WriteLine("Категория -  еда");
                        break;
                    }
                case categories.groceries:
                    {
                        Console.WriteLine("Категория -  бакалея");
                        break;
                    }
                case categories.careProducts:
                    {
                        Console.WriteLine("Категория -  товары для ухода");
                        break;
                    }
            }
        }
        
        static List<Product> products = new List<Product>();

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

                int choice = Convert.ToInt32(Console.ReadLine());
                

                switch (choice)
                {
                    case 1:
                        AddProduct();
                        break;
                    case 2:
                        RemoveProduct();
                        break;
                    case 3:
                        
                        break;
                    case 4:
                       
                        break;
                    case 5:
                       
                        break;
                    case 6:
                        PrintProduct();
                        break;
                    case 7:
                        return;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }
            }
        }

        // Добавить товар
        static void AddProduct()
        {
            Product newProduct = new Product();
            newProduct.GetProduct();
            products.Add(newProduct);
            Console.WriteLine("Товар успешно добавлен!");
        }
        static void RemoveProduct()
        {
            Console.Write("Введите ID товара для удаления: ");
        }
        public void PrintProduct(Product P)
        {
            Console.WriteLine($"id - {P.id}, название - {P.name}, цена - {P.price}, количество -  {P.countProd}, остатки -  {P.isprod}");
        }
    }
}