using APBD3.Exceptions;

namespace APBD3.Classes;

public class GasContainer(double cargoMass,
    double height,
    double containerMass,
    double depth,
    double maxCargoMass): Container(cargoMass, height, containerMass, depth, "G", maxCargoMass), IHazardNotifier
{
    public double Pressure { get; protected set; }

    public void SendHazardNotification()
    {
        Console.Write("Attention, Hazard situation, serial number of the container = " + SerialNumber);
    }

    public override void EmptyTheContainer()
    {
        CargoMass = CargoMass / 100 * 5;
    }

    public void FillTheContainer(double mass,  double pressure)
    {
        if (mass + CargoMass <= MaxCargoMass)
        {
            CargoMass = mass;
            Pressure = pressure;
        }
        else
        {
            SendHazardNotification();
            throw new OverfillException("Cargo weight exceeds maximum cargo weight");
        }
    }

    public override string ToString()
    {
        return $"{base.ToString()}, {nameof(Pressure)}: {Pressure}";
    }
}