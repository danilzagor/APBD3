using APBD3.Exceptions;

namespace APBD3.Classes;

public abstract class Container(
    double cargoMass,
    double height,
    double containerMass,
    double depth,
    string containerType,
    double maxCargoMass) : IContainer
{
    private static int _nextId = 1;
    
    public double CargoMass { get; protected set;} = cargoMass;
    public double Height { get; protected set;} = height;
    public double ContainerMass { get; protected set;} = containerMass;
    public double Depth { get; protected set;} = depth;
    public string SerialNumber { get; protected set; } = "KON-" + containerType + "-" + _nextId++;
    public double MaxCargoMass { get; protected set;} = maxCargoMass;

    public virtual void EmptyTheContainer()
    {
        CargoMass = 0;
    }
    
    public virtual void FillTheContainer(double mass)
    {
        if (mass <= MaxCargoMass)
            CargoMass = mass;
        else throw new OverfillException("Cargo weight exceeds maximum cargo weight");
    }

    public override string ToString()
    {
        return $"{nameof(CargoMass)}: {CargoMass}, {nameof(Height)}: {Height}, {nameof(ContainerMass)}: {ContainerMass}, {nameof(Depth)}: {Depth}, {nameof(SerialNumber)}: {SerialNumber}, {nameof(MaxCargoMass)}: {MaxCargoMass}";
    }
}