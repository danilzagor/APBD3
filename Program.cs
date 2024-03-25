using APBD3.Classes;

namespace APBD3;

class Program
{
    static void Main(string[] args)
    {
        List<ContainerShip> containerShips = [];
        List<Container> containers = [];
        while (true)
        {
            if (containerShips.Count==0)
                Console.Write("There are no ships");
            else
            {
                Console.Write("List of all ships:");
                containerShips.ForEach(Console.Write);
            }
            if (containers.Count==0)
                Console.Write("There are no containers");

            Console.Write("List of actions:");
            Console.Write("1. Create new ship");

            if (containerShips.Count>0)
            {
                Console.Write("2. Remove ship");
                Console.Write("3. Add new container");
            }

            if (containers.Count>0)
            {
                Console.Write("4. Load container on a ship");
                Console.Write("5. Remove container");
                Console.Write("6. Transport container from one ship to another");
                Console.Write("7. Substitute a container on board by a new one");
            }

            int option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    Console.Write("Enter speed of a ship");
                    double speed = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Enter max number of containers");
                    int maxNumber = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter max weight of all containers");
                    double maxWeight = Convert.ToDouble(Console.ReadLine());
                    containerShips.Add(new ContainerShip(speed, maxNumber, maxWeight));
                    break;
                case 2:
                    int i = 0;
                    containerShips.ForEach(ship =>
                    {
                        Console.Write(i++ +" - "+ ship);
                    });
                    Console.Write("Choose a ship to remove");
                    int indexToRemove = Convert.ToInt32(Console.ReadLine());
                    containerShips.RemoveAt(indexToRemove);
                    break;
                case 3:
                    Console.Write("Choose a type of a container: G - Gas, L - Bulk, C - Refrigerated");
                    string type = Console.ReadLine();
                    Console.Write("Enter mass of a cargo");
                    double cargoMass = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Enter height of a container");
                    double height = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Enter mass of a container");
                    double containerMass = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Enter depth of a container");
                    double containerDepth = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Enter max cargo mass");
                    double maxCargoMass = Convert.ToDouble(Console.ReadLine());
                    switch (type)
                    {
                        case "G":
                            containers.Add(new GasContainer(cargoMass, height, containerMass, containerDepth,maxCargoMass));
                            break;
                        case "L":
                            containers.Add(new BulkContainer(cargoMass, height, containerMass, containerDepth,maxCargoMass));
                            break;
                        case "C":
                            containers.Add(new RefrigeratedContainer(cargoMass, height, containerMass, containerDepth,maxCargoMass));
                            break;
                    }

                    break;
                    
            }
        }
    }
}