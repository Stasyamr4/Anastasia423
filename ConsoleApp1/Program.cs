using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1

{
    internal class Program
    {
        static Random random = new Random();
        static int carsProcessed = 0; //сколько приехало
        static int successfulRepairs = 0;//успешные
        static int failedRepairs = 0;//неудачные
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
            Console.WriteLine($"=== АВТОСЕРВИС ===");
            Console.WriteLine($"Баланс: {player.MyMoney} руб.");
            Console.WriteLine($"Обслужено машин: {carsProcessed}");
            Console.WriteLine($"Успешных ремонтов: {successfulRepairs}");
            Console.WriteLine($"Неудачных ремонтов: {failedRepairs}");

            // Показать ожидающие поставки
            var pendingOrders = Core.Context.OrderParts.Where(o => o.PlayerID == 1).ToList();
            if (pendingOrders.Any())
            {
                Console.WriteLine("\nОжидаются поставки:");
                foreach (var order in pendingOrders)
                {
                    var part = Core.Context.parts.FirstOrDefault(p => p.partID == order.PartID);
                    Console.WriteLine($"{part.partName}: {order.count} шт. (через {order.carsUntilDelivery} машин)");
                }
            }
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
            var inventory = Core.Context.parts_player.Where(i => i.idPlayer == 1).ToList();
            Console.WriteLine("Ваш склад:");

            foreach (var item in inventory)
            {
                var part = Core.Context.Parts.FirstOrDefault(p => p.partID == item.idPart);
                Console.WriteLine($"{part.partName}: {item.countParts} шт.");
            }
        }
    }
}

