using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ShowMenu();
        }
        public static void ShowMenu()
        {
            bool ShowMenu = true;
            while (ShowMenu)
            {
                Console.WriteLine("ОНЛАЙН МАГАЗИН");
                Console.WriteLine("1. Регистрация");
                Console.WriteLine("2. Войти в аккаунт");
                Console.WriteLine("3. Каталог товаров");
                Console.WriteLine("0. Выход");
                if (int.TryParse(Console.ReadLine(), out int choice))
                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            registration();
                            break;
                        case 2:
                            Console.Clear();
                            SignIn();
                            break;
                        case 3:
                            Console.Clear();
                            Catalogue();
                            break;
                        case 0:
                            ShowMenu = false;
                            break;
                    }
            }
        }
        //Меню
        //{
        //регистрация
        //войти в аккаунт
        ///каталог товаров
        //}
        public static void registration()
        {
            User user = new User();
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("Введите логин");
                string login = Console.ReadLine();
                Console.WriteLine("Введите пароль");
                string password = Console.ReadLine();
                if ((login != null) || (password != null))
                {
                    var AreUser = Core.Context.User.Where(u => u.login == login).FirstOrDefault();
                    if (AreUser == null)
                    {
                        user.login = login;
                        while (true)
                        {
                            //попросить ввести пароль еще раз, если не совпадут - введет еще раз благодаря бесконечному циклу
                            Console.WriteLine("Введите пароль еще раз");
                            string password2 = Console.ReadLine();
                            if ((password2 != null) && (password2 == password))
                            {
                                user.password = password;
                                Core.Context.User.Add(user);
                                Core.Context.SaveChanges();
                                Console.WriteLine("Пользователь успешно добавлен!");
                                flag = false;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Пароли не совпадают");
                            }
                        }

                    }
                    else
                    {
                        Console.WriteLine("Пользователь с таким именем уже существует");
                    }
                }
            }
        }

        //Регистрация 
        //{
        ////Создать экземпляр класса пользователя из БД
        ///Попросить пользователя заполнить экземпляр данными
        ///Проверить не занят ли логин(почта/телефон) введённый пользователем пользователя
        /////пользователь вводит повторно пароль
        ///Если все прошло успешно добавить пользователя и войти в аккаунт\вернутся в меню
        //}
        static User user = null;
        public static void SignIn()
        {
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("Введите логин");
                string login = Console.ReadLine();
                Console.WriteLine("Введите пароль");
                string password = Console.ReadLine();
                if ((login != null) || (password != null))
                {
                    var AreUser = Core.Context.User.Where(u => u.login == login).FirstOrDefault();
                    if (AreUser != null)
                    {
                        if (password == AreUser.password)
                        {
                            user = AreUser;
                            flag = false;
                            Console.Clear();
                            Console.WriteLine("Успешный вход");
                        }
                        else
                        {
                            Console.WriteLine("Ошибка, неправильный пароль");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Пользователь с таким именем не найден");
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка, логин или пароль неправильный");
                }
            }
        }

        //Войти в аккаунт
        //{
        //пользователь вводит логин
        //пользователь вводит пароль 
        //проверяем есть ли такой лог в базе данных
        //если есть то проверяем правильный ли пароль, а если нет то выводим ошибку
        //если все успешно то входим в акк
        //}

        public static void Catalogue()
        {
            List<Product> products = Core.Context.Product.ToList();
            bool ShowCatalogue = true;
            while (ShowCatalogue)
            {
                if (Core.Context.Product.Any())
                {
                    Console.WriteLine("====== КАТАЛОГ ТОВАРОВ ======");
                    foreach (var prod in products)
                    {
                        Console.WriteLine($"ID: {prod.id}, название: {prod.name}, цена: {prod.price}");
                    }
                    Console.WriteLine();

                    Console.WriteLine("Хотите посмотреть конкретный товар? (Да/нет)");
                    string choice = Console.ReadLine().ToLower();
                    bool ChoiceProd = true;
                    while (ChoiceProd)
                    {
                        switch (choice.ToLower())
                        {
                            case "да":
                                Console.WriteLine("Введите ID товара: ");
                                if (int.TryParse(Console.ReadLine(), out int id))
                                {
                                    var IdProd = Core.Context.Product.FirstOrDefault(prod => prod.id == id);
                                    if (IdProd != null)
                                    {
                                        Console.Clear();
                                        Console.WriteLine($"ID: {IdProd.id}, название: {IdProd.name}, цена: {IdProd.price}");
                                        Console.WriteLine("Желаете продолжить? (Да/нет)");
                                        string answer = Console.ReadLine();
                                        switch (answer.ToLower())
                                        {
                                            case "да":
                                                Console.Clear();
                                                Console.WriteLine("Выберите пункт меню:\n" +
                                            "1. Добавить товар в корзину\n" +
                                            "2. Посмотреть каталог");
                                                string vibor = Console.ReadLine();
                                                if (vibor == "2")
                                                {
                                                    Catalogue();
                                                    ChoiceProd = false;
                                                }
                                                if (vibor == "1")
                                                {
                                                    if (user != null)
                                                    {
                                                        AddProductInBasket(user.id, IdProd.id);
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Необходимо войти в аккаунт!");
                                                        SignIn();
                                                    }
                                                }
                                                break;
                                            case "нет":
                                                Console.Clear();
                                                ShowCatalogue = false;
                                                ChoiceProd = false;
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Товар с таким ID не найден");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Введите корректный ID!");
                                }
                                break;
                            case "нет":
                                Console.Clear();
                                ChoiceProd = false;
                                ShowCatalogue = false;
                                ShowMenu();
                                    break;
                        }
                        break;
                    }
                }
                

            }
        }

        //каталог товаров 
        //{
        //вывести данные из таблице товров бд
        //выбрать конкретный товар(айди)
        //очищение консоли
        //выводим полную информацию о товаре
        //выбор между добавлением в корзину и вернуться в меню и купить товар
        //}

        static public void AddProductInBasket(int UsID, int IdProd)
        {
            Console.WriteLine("Введите количество товара, который хотите добавить в корзину");
            if (int.TryParse(Console.ReadLine(), out int countProd) && countProd > 0)
            {
                try
                {
                    // Проверяем существование товара
                    var product = Core.Context.Product.FirstOrDefault(p => p.id == IdProd);
                    if (product == null)
                    {
                        Console.WriteLine("Товар не найден!");
                        return;
                    }

                    // Ищем корзину пользователя
                    var userBasket = Core.Context.Basket.FirstOrDefault(b => b.user_id == UsID);

                    if (userBasket == null)
                    {
                        // Создаем новую корзину
                        userBasket = new Basket { user_id = UsID };
                        Core.Context.Basket.Add(userBasket);
                        Core.Context.SaveChanges();
                        Console.WriteLine("Создана новая корзина");
                    }

                    // Проверяем, есть ли уже этот товар в корзине
                    var existingProduct = Core.Context.Product_Basket
                        .FirstOrDefault(pb => pb.basket_id == userBasket.id && pb.product_id == IdProd);

                    if (existingProduct != null)
                    {
                        // Если товар уже есть, обновляем количество
                        existingProduct.count += countProd;
                        Console.WriteLine($"Количество товара обновлено: {existingProduct.count}");
                    }
                    else
                    {
                        // Добавляем новый товар в корзину
                        var productBasket = new Product_Basket
                        {
                            basket_id = userBasket.id,
                            product_id = IdProd,
                            count = countProd
                        };
                        Core.Context.Product_Basket.Add(productBasket);
                        Console.WriteLine("Товар добавлен в корзину");
                    }

                    // Сохраняем изменения
                    Core.Context.SaveChanges();
                    Console.WriteLine("Изменения сохранены!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при добавлении товара: {ex.Message}");
                    // Для отладки можно вывести внутреннее исключение
                    if (ex.InnerException != null)
                        Console.WriteLine($"Внутренняя ошибка: {ex.InnerException.Message}");
                }
            }
            else
            {
                Console.WriteLine("Введите корректное количество!");
            }
        }

        //функция для добавления товара в коризу(Товары товар)

        //функция для показа корзины
        //Проверка
        //{
        //если пользователь вошел, то показываем корзину
        //}

        //фунция для оформления заказа
    }
}
        

