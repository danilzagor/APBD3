namespace APBD3.Classes;

public class RefrigeratedContainer(double cargoMass,
    double height,
    double containerMass,
    double depth,
    double maxCargoMass): Container(cargoMass, height, containerMass, depth, "C", maxCargoMass)
{
    public KeyValuePair<string, double> TypeOfProduct { get; protected set; }
    public double Temperature { get; protected set; }
    public void FillTheContainer(double mass, KeyValuePair<string, double> product, double temperature)
    {
        if (TypeOfProduct.Equals(default(KeyValuePair<string, double>)))
            TypeOfProduct = product;
            
        if(TypeOfProduct.Equals(product))
            if (product.Value > temperature)
            {
                base.FillTheContainer(mass);
                Temperature = temperature;
            }
            else Console.Write("Provided temperature is lower than required");
        else Console.Write("Provided product differs from what this container can contain");
    }

    public override void EmptyTheContainer()
    {
        TypeOfProduct = new KeyValuePair<string, double>();
        base.EmptyTheContainer();
    }

    public override string ToString()
    {
        return $"{base.ToString()}, {nameof(TypeOfProduct)}: {TypeOfProduct.Key}, {nameof(Temperature)}: {Temperature}";
    }
}