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
            Console.Clear();
            ProcessDeliveries(player);

            // Генерируем случайного клиента
            var clientCar = GenerateRandomClient();
            carsProcessed++;

            ShowClientInfo(clientCar);
            ProcessPlayerChoice(player, clientCar);
        }

        private static cars GenerateRandomClient()
        {
            var defects = Core.Context.defects.ToList();
            var carsList = Core.Context.cars.ToList();

            var randomDefect = defects[random.Next(defects.Count)];
            var randomCar = carsList[random.Next(carsList.Count)];

            // Создаем новую машину с дефектом
            return new cars
            {
                carName = randomCar.carName,
                defectID = randomDefect.id
            };
        }
        private static void ShowClientInfo(cars car)
        {
            var defect = Core.Context.defects.FirstOrDefault(d => d.id == car.defectID);
            var neededPart = Core.Context.Parts.FirstOrDefault(p => p.partID == defect.partNeedID);
            var repairCost = CalculateRepairCost(neededPart);

            Console.WriteLine($"Приехал клиент на {car.carName}");
            Console.WriteLine($"Неисправность: {defect.defectName}");
            Console.WriteLine($"Нужна деталь: {neededPart.partName}");
            Console.WriteLine($"Стоимость ремонта: {repairCost} руб.");
            Console.WriteLine();
        }
        
        private static void ShowStoreMenu(player player)
        {

        }

        private static decimal CalculateRepairCost(parts part)
        {
            return (decimal)(part.basePrice + (part.basePrice * (decimal)(part.workCost)));
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

        private static void ProcessPlayerChoice(player player, cars clientCar)
        {
            var defect = Core.Context.defects.FirstOrDefault(d => d.id == clientCar.defectID);
            var neededPartId = defect.partNeedID;

            Console.WriteLine("Ваш склад:");
            var inventory = Core.Context.parts_player.Where(i => i.idPlayer == player.id && i.countParts > 0).ToList();

            if (inventory.Any())
            {
                for (int i = 0; i < inventory.Count; i++)
                {
                    var part = Core.Context.Parts.FirstOrDefault(p => p.partID == inventory[i].idPart);
                    Console.WriteLine($"{i + 1}. {part.partName} - {inventory[i].countParts} шт.");
                }

                Console.WriteLine($"0. Отказать (штраф 1000 руб.)");
                Console.WriteLine("Выберите деталь для замены:");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0)
                    {
                        // Отказ от обслуживания
                        player.MyMoney -= 1000;
                        Core.Context.SaveChanges();
                        Console.WriteLine("Вы отказали клиенту. Штраф 1000 руб.");
                    }
                    else if (choice > 0 && choice <= inventory.Count)
                    {
                        var selectedPartId = inventory[choice - 1].idPart;
                        TryRepair(player, clientCar, selectedPartId, neededPartId);
                    }
                }
            }
            else
            {
                Console.WriteLine("Склад пуст! Придется отказать клиенту.");
                player.MyMoney -= 1000;
                Core.Context.SaveChanges();
                Console.WriteLine("Штраф 1000 руб.");
            }
        }

        private static void TryRepair(player player, cars clientCar, int selectedPartId, int neededPartId)
        {

        }
    }
}

