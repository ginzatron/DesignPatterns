using System;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Channels;

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

            //Server server = new Server();
            //Console.WriteLine("Hell, what is your name");
            //var name = Console.ReadLine();

            //Patron patron = new Patron(name);

            //Console.WriteLine($"Hello {patron.Name} what app would you like, 1-15?");
            //var appId = int.Parse(Console.ReadLine());

            //Console.WriteLine($"entree? 16-20?");
            //var entreeId = int.Parse(Console.ReadLine());

            //Console.WriteLine($"to drink? 21-30");
            //var drinkId = int.Parse(Console.ReadLine());

            //server.PlaceOrder(patron, appId, entreeId, drinkId);

            //Console.ReadKey();

            var homeTheaterFacade = new HomeTheaterFacade(new Amplifier(), new DvdPlayer());
            homeTheaterFacade.WatchMovie("Toy Story");
            homeTheaterFacade.EndMovie();
        }
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

    //Server FACADE
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

    //Project from gang of four
    //Home Theater Facade
    public class HomeTheaterFacade
    {
        // composition of subsystem components
        public Amplifier _amp;
        public DvdPlayer _dvd;
    
        // the constructor takes a dependancy on each component in the subsystem
        public HomeTheaterFacade(Amplifier amp, DvdPlayer dvd)
        {
            _amp = amp;
            _dvd = dvd;
        }
    
        public void WatchMovie(String movie)
        {
            Console.WriteLine($"getting ready to watch {movie}");
            _amp.On();
            _amp.SetVolume(5);
            _dvd.On();
            _dvd.PlayMovie();
        }
    
        public void EndMovie()
        {
            Console.WriteLine($"turning off and shutting down");
            _amp.Off();
            _dvd.Stop();
            _dvd.Eject();
            _dvd.Off();
        }
    }

    public class DvdPlayer
    {
        public void On()
        {
            Console.WriteLine("dvd player is on");
        }

        public void PlayMovie()
        {
            Console.WriteLine("movie playing");
        }

        public void Stop()
        {
            Console.WriteLine("stropping movie");
        }

        public void Eject()
        {
            Console.WriteLine("get this dvd out of here");
        }

        public void Off()
        {
            Console.WriteLine("dvd player turned off");
        }

    }

    public class Amplifier
    {
        public void On()
        {
            Console.WriteLine("turning amp on");
        }

        public void SetVolume(int vol)
        {
            Console.WriteLine($"volume set to {vol}");
        }

        public void Off()
        {
            Console.WriteLine("amp turned off");
        }

    }
}
