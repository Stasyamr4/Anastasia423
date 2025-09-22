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
    }
}