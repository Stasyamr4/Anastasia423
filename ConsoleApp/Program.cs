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
                            //Catalogue();
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

        }
                //каталог товаров 
                //{
                //вывести данные из таблице товров бд
                //выбрать конкретный товар(айди)
                //очищение консоли
                //выводим полную информацию о товаре
                //выбор между добавлением в корзину и вернуться в меню и купить товар
                //}

        //функция для добавления товара в коризу(Товары товар)

        //функция для показа корзины
        //Проверка
        //{
        //если пользователь вошел, то показываем корзину
        //}

        //фунция для оформления заказа
            }
    }
        

