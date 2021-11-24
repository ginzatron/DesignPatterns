var carrots = new Carrots(0.82);
carrots.Attach(new Restaurant("Gibsons", 0.77));
carrots.Attach(new Restaurant("Bob Chins", 0.74));
carrots.Attach(new Restaurant("Smoke Daddys", 0.75));

carrots.PricePerPound = 0.79;
carrots.PricePerPound = 0.76;
carrots.PricePerPound = 0.74;
carrots.PricePerPound = 0.81;

Console.ReadKey();
// SUBJECT - will provide interface for attaching and detaching observers.
// will keep track of current veggie price
abstract class Veggie
{
    private double _pricePerPound;
    public List<IRestaurant> _restaurants = new List<IRestaurant>();

    public Veggie(double pricePerPound)
    {
        _pricePerPound = pricePerPound;
    }

    public void Attach(IRestaurant restaurant)
    {
        _restaurants.Add(restaurant);
    }

    public void Detach(IRestaurant restaurant)
    {
        _restaurants.Remove(restaurant);
    }

    public void Notify()
    {
        foreach (IRestaurant restaurant in _restaurants)
        {
            // each restaurant will hold the implementation of updating itself with the current veggie?
            restaurant.Update(this);
        }

        Console.WriteLine("");
    }

    public double PricePerPound
    {
        get { return _pricePerPound; }
        set
        {
            if (_pricePerPound != value)
            {
                _pricePerPound = value;
                // if the price is updated, notify all restaurants
                Notify();
            }
        }
    }
}

// CONCRETE SUBJECT - represents price of a specific vegetable
class Carrots: Veggie
{
    public Carrots(double price) : base(price) { }
}

// OBSERVER PARTICIPANT - this will be the restaurant that wants to observe the vegetable prices.
// will define an interface by which the implementors of the interface can be updated
interface IRestaurant
{
    void Update(Veggie veggie);
}

// CONCRETE OBSERVER - these are the specific retsaurants
class Restaurant: IRestaurant
{
    private string _name;
    private Veggie? _veggie;
    private double _purchaseThreshold;

    public Restaurant(string name, double purchaseThreshold)
    {
        _name = name;
        _purchaseThreshold = purchaseThreshold;
    }

    public void Update(Veggie veggie)
    {
        Console.WriteLine($"{_name} is notifed of a price change in {veggie.GetType().Name} to {veggie.PricePerPound}");
        if (veggie.PricePerPound < _purchaseThreshold)
            Console.WriteLine($"{_name} wants to buy some {veggie.GetType().Name}");
    }
}