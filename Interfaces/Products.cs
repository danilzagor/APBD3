namespace APBD3.Classes;

public struct Products
{
    public static readonly KeyValuePair<string, double> Bananas = new ("Bananas", 13.3);
    public static readonly KeyValuePair<string, double> Chocolate = new("Chocolate",18);
    public static readonly KeyValuePair<string, double> Fish = new("Fish",2);
    public static readonly KeyValuePair<string, double> Meat = new("Meat",-15);
    public static readonly KeyValuePair<string, double> IceCream = new ("Ice cream",-18);
    public static readonly KeyValuePair<string, double> FrozenPizza = new("Frozen pizza", -30);
    public static readonly KeyValuePair<string, double> Cheese = new ("Cheese", 7.2);
    public static readonly KeyValuePair<string, double> Sausages = new ("Sausages",5);
    public static readonly KeyValuePair<string, double> Butter = new ("Butter",20.5);
    public static readonly KeyValuePair<string, double> Eggs = new ("Eggs",19);
    public static readonly List<KeyValuePair<string, double>> ListOfAllProducts = new List<KeyValuePair<string, double>>
    {
        Bananas, Chocolate, Fish, Meat, IceCream, FrozenPizza, Cheese, Sausages, Butter, Eggs
    };

}