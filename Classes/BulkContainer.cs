using APBD3.Exceptions;

namespace APBD3.Classes;

public class BulkContainer( double cargoMass,
    double height,
    double containerMass,
    double depth,
    double maxCargoMass): Container(cargoMass, height, containerMass, depth, "L", maxCargoMass), IHazardNotifier
{
    public bool IsDangerousCargo { get; protected set; }
    
    public void SendHazardNotification()
    {
        Console.WriteLine("Attention, Hazard situation, serial number of the container = " + SerialNumber);
    }

    public void FillTheContainer(double mass, bool isDangerousCargo)
    {
        IsDangerousCargo = isDangerousCargo;
        if(isDangerousCargo)
            if(CargoMass<=MaxCargoMass/2)
                base.FillTheContainer(mass);
            else SendHazardNotification();
        else if(CargoMass<=MaxCargoMass/100*90)
            base.FillTheContainer(mass);
        else SendHazardNotification();
    }

    public override string ToString()
    {
        return $"{base.ToString()}, {nameof(IsDangerousCargo)}: {IsDangerousCargo}";
    }
}