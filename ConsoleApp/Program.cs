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
                Console.WriteLine("4. Корзина товаров");
                Console.WriteLine("5. Заказ из корзины");
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
                        case 4:
                            Console.Clear();
                            ShowBasket();
                            break;
                        case 5:
                            Console.Clear();
                            OrderFromBasket();
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
                                            "2. Посмотреть каталог\n" +
                                            "3. Посмотреть корзину");

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
        public static void ShowBasket()
        {
            if (user != null)
            {
                if (Core.Context.Product_Basket != null)
                {
                    var idbasketUser = Core.Context.Basket.Where(usID => usID.user_id == user.id).FirstOrDefault();
                    var basketUser = Core.Context.Product_Basket.Where(idBasket => idBasket.basket_id == idbasketUser.id).ToList();
                    decimal summa = 0;
                    foreach (var product in basketUser)
                    {
                        var prods = Core.Context.Product.Where(prodID => product.product_id == prodID.id).ToList(); ;
                        foreach (var prod in prods)
                        {
                            decimal price = 0;
                            Console.WriteLine($"ID: {prod.id}, название: {prod.name}, цена: {prod.price}, количество: {product.count}, стоимость: {prod.price * product.count} руб.");
                            summa += price;
                        }
                    }
                    Console.WriteLine($"Итоговая стоимость корзины: {summa}");
                }
                else
                {
                    Console.WriteLine("Корзина пустая! Добавьте товары!");
                    Catalogue();
                }
            }

            else
            {
                Console.WriteLine("Войдите в аккаунт!");
                SignIn();
            }
        }
        //просмотр корзины()
        //функция для показа корзины
        //Проверка
        //{
        //если пользователь вошел, то показываем корзину
        //}
        static public void OrderFromBasket()
        {
            if (user != null)
            {
                // Получаем товары в корзине текущего пользователя
                var userBasket = Core.Context.Basket
                .Where(b => b.user_id == user.id)
                .FirstOrDefault();

                if (userBasket == null)
                {
                    Console.WriteLine("Корзина пуста!");
                    return;
                }

                // Получаем товары в корзине пользователя
                var productsInBasket = Core.Context.Product_Basket
                .Where(pb => pb.basket_id == userBasket.id)
                .ToList();

                if (!productsInBasket.Any())
                {
                    Console.WriteLine("В корзине нет товаров!");
                    return;
                }

                var PVZ = Core.Context.PVZ.ToList();
                Console.WriteLine("Заказ товаров");

                bool zakaz = true;
                while (zakaz)
                {
                    Console.WriteLine("Желаете заказать все товары из корзины? (да/нет)");
                    string choice = Console.ReadLine().ToLower();

                    switch (choice)
                    {
                        case "да":
                            Console.WriteLine("Список товаров в корзине:");
                            foreach (var item in productsInBasket)
                            {
                                var product = Core.Context.Product
                                .FirstOrDefault(p => p.id == item.product_id);
                                if (product != null)
                                {
                                    Console.WriteLine($"- {product.name}: {product.price} руб.");
                                }
                            }

                            Console.WriteLine("\nВыберите id ПВЗ:");
                            foreach (var pvz in PVZ)
                            {
                                Console.WriteLine($"id: {pvz.id}, Адрес: {pvz.address}");
                            }

                            if (!int.TryParse(Console.ReadLine(), out int IDpvz) ||
                            !PVZ.Any(p => p.id == IDpvz))
                            {
                                Console.WriteLine("Неверный ID ПВЗ!");
                                continue;
                            }

                            try
                            {
                             
                                var delivery = new Delivery
                                {
                                    pvz_id = IDpvz,
                                    date_order = DateTime.Now,
                                    user_id = user.id
                                };
                                Core.Context.Delivery.Add(delivery);
                                Core.Context.SaveChanges();

                                
                                foreach (var basketItem in productsInBasket)
                                {
                                    var deliveryProductItem = new Delivery_Product
                                    {
                                        product_id = basketItem.product_id,
                                        delivery_id = delivery.id,
                                        
                                    };
                                    Core.Context.Delivery_Product.Add(deliveryProductItem);
                                }
                                Core.Context.SaveChanges();

                          
                                Core.Context.Product_Basket.RemoveRange(productsInBasket);
                                Core.Context.SaveChanges();

                                Console.WriteLine("Заказ успешно оформлен!");
                                zakaz = false;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Ошибка при оформлении заказа: {ex.Message}");
                            }
                            break;

                        case "нет":
                            
                            Console.WriteLine("Функция выбора отдельных товаров пока не реализована.");
                            Console.WriteLine("Хотите продолжить оформление заказа? (да/нет)");
                            string continueChoice = Console.ReadLine().ToLower();
                            if (continueChoice == "нет")
                            {
                                zakaz = false;
                            }
                            break;

                        default:
                            Console.WriteLine("Пожалуйста, введите 'да' или 'нет'.");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Пользователь не авторизован!");
            }
        }

        public static void ShowHistory()
        {
            if (user != null)
            {
                var Hist = Core.Context.Delivery.Where(us => us.id == user.id).ToList();
                if (Hist != null)
                {
                    foreach (var ord in Hist)
                    {
                        var deliveriesWithPVZName = Core.Context.Delivery.Select(p => p.pvz_id);
                        Console.WriteLine($"id: {ord.id}, pvz: {ord.pvz_id}, date: {ord.date_order},  ");
                    }
                }
                else
                {
                    Console.WriteLine("Истории покупок еще нет.");
                }
            }
            // Просмотр истории покупок()
        }
    }
}
        

