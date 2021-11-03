using System;
using System.Reflection.Metadata.Ecma335;

namespace Facade
{
    class Program
    {
        static void Main(string[] args)
        {
            // FACADE: STRUCTURAL
            // Facade Pattern is a simple code structure laid over a complex structure
            // this hides the complexity from the code
            // other code won't be able to access the complex code, they will be covered by the facade

            // Participants:
            //   Subsystems: classes or objects with functionality that will be covered by the facade to
            //   help simplify an interface
            //   Facade: abstraction layer above the subsystems and delegates work to subsystems

            // ex: when eating at a restaurant you don't care where you meal is prepped and cooked in the kitchen, that
            // layer has been abstracted away

            Server server = new Server();
            Console.WriteLine("Hell, what is your name");
            var name = Console.ReadLine();

            Patron patron = new Patron(name);

            Console.WriteLine($"Hello {patron.Name} what app would you like, 1-15?");
            var appId = int.Parse(Console.ReadLine());

            Console.WriteLine($"entree? 16-20?");
            var entreeId = int.Parse(Console.ReadLine());

            Console.WriteLine($"to drink? 21-30");
            var drinkId = int.Parse(Console.ReadLine());

            server.PlaceOrder(patron, appId, entreeId, drinkId);

            Console.ReadKey();
        }

        public class Patron
        {
            private readonly string _name;

            public Patron(string name)
            {
                _name = name;
            }

            public string Name => _name;
        }

        public class FoodItem
        {
            public int DishId;
        }

        interface IKitchenSection
        {
            FoodItem PrepDish(int dishId);
        }

        public class Order
        {
            public FoodItem Appetizer { get; set; }
            public FoodItem Entree { get; set; }
            public FoodItem Drink { get; set; }
        }

        // SUBSYSTEMS:
        public class ColdPrep : IKitchenSection
        {
            public FoodItem PrepDish(int dishId)
            {
                return new FoodItem()
                {
                    DishId = dishId
                };
            }
        }

        public class HotPrep : IKitchenSection
        {
            public FoodItem PrepDish(int dishId)
            {
                return new FoodItem()
                {
                    DishId = dishId
                };
            }
        }

        public class Bar : IKitchenSection
        {
            public FoodItem PrepDish(int dishId)
            {
                return new FoodItem()
                {
                    DishId = dishId
                };
            }
        }

        //FACADE
        public class Server
        {
            private ColdPrep _coldPrep = new ColdPrep();
            private Bar _bar = new Bar();
            private HotPrep _hotPrep = new HotPrep();

            public Order PlaceOrder(Patron patron, int coldAppId, int hotEntreeId, int drinkId)
            {
                Console.WriteLine($"{patron.Name} places order for cold app {coldAppId.ToString()}, hot entree # {hotEntreeId.ToString()}, and drink $ {drinkId.ToString()}.");
                Order order = new Order();
                order.Appetizer = _coldPrep.PrepDish(coldAppId);
                order.Entree = _hotPrep.PrepDish(hotEntreeId);
                order.Drink = _bar.PrepDish(drinkId);

                return order;
            }
        }

    }
}
