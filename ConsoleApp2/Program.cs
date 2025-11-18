using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var player = Core.Context.player.FirstOrDefault();
            if (player == null)
            {
                player = new player { MyMoney = 5000 };
                Core.Context.player.Add(player);
                Core.Context.SaveChanges();
                Console.WriteLine("Создан новый игрок!");
            }
            bool gameRunning = true;

            while (gameRunning)
            {
                Console.Clear();
                ShowPlayerStatus(player);
                Console.WriteLine("\n1 - Обслужить следующего клиента");
                Console.WriteLine("2 - Купить запчасти");
                Console.WriteLine("3 - Посмотреть склад");
                Console.WriteLine("4 - Выход");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ProcessNextCar(player); 
                        break;
                    case "2":
                        ShowStoreMenu(player);
                        break;
                    case "3":
                        ShowInventory(player);
                        break;
                    case "4":
                        gameRunning = false; 
                        break;
                }

                if (choice != "4")
                {
                    Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                }
            }

            Console.WriteLine("Игра завершена!");
        }
        private static void ShowPlayerStatus(player player)
        {

        }
        private static void ProcessNextCar(player player)
        {

        }
        private static void ShowClientInfo(cars car)
        {

        }
        //private static cars GenerateRandomClient()
        //{

        //}
        private static void ShowStoreMenu(player player)
        {

        }

        private static void ShowInventory(player player)
        {

        }
    }
}
