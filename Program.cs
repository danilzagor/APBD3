using APBD3.Classes;
using APBD3.Exceptions;

namespace APBD3;

class Program
{
    static void Main(string[] args)
    {
        List<ContainerShip> containerShips = [];
        List<Container> containers = [];
        while (true)
        {
            if (containerShips.Count == 0)
            {
                Console.WriteLine("There are no ships");
            }
            else
            {
                Console.WriteLine("List of all ships:");
                containerShips.ForEach(Console.WriteLine);
            }

            if (containers.Count == 0)
            {
                Console.WriteLine("There are no containers");
            }
            else
            {
                Console.WriteLine("List of all Containers in docks:");
                containers.ForEach(Console.WriteLine);
            }
                
            
            Console.WriteLine("List of actions:");
            Console.WriteLine("0. Exit programm");
            Console.WriteLine("1. Create new ship");

            if (containerShips.Count>0)
            {
                Console.WriteLine("2. Remove ship");
                Console.WriteLine("3. Add new container");
                Console.WriteLine("4. Load container/containers on a ship");
                Console.WriteLine("5. Unload container from the ship");
                Console.WriteLine("6. Transport container from one ship to another");
                Console.WriteLine("7. Substitute a container on board by a new one");
                Console.WriteLine("8. Unload a container");
                Console.WriteLine("9. Load a container");
            }

            int option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 0:
                    return;
                case 1:
                    Console.WriteLine("Enter speed of a ship");
                    double speed = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter max number of containers");
                    int maxNumber = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter max weight of all containers");
                    double maxWeight = Convert.ToDouble(Console.ReadLine());
                    containerShips.Add(new ContainerShip(speed, maxNumber, maxWeight));
                    break;
                case 2:
                    int i = 0;
                    containerShips.ForEach(ship =>
                    {
                        Console.WriteLine(i++ +" - "+ ship);
                    });
                    Console.WriteLine("Choose a ship to remove");
                    int indexToRemove = Convert.ToInt32(Console.ReadLine());
                    containerShips.RemoveAt(indexToRemove);
                    break;
                case 3:
                    Console.WriteLine("Choose a type of a container: G - Gas, L - Bulk, C - Refrigerated");
                    string type = Console.ReadLine();
                    Console.WriteLine("Enter mass of a cargo");
                    double cargoMass = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter height of a container");
                    double height = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter mass of a container");
                    double containerMass = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter depth of a container");
                    double containerDepth = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter max cargo mass");
                    double maxCargoMass = Convert.ToDouble(Console.ReadLine());
                    switch (type)
                    {
                        case "G":
                            var gasContainer = new GasContainer(0, height, containerMass, containerDepth,maxCargoMass);
                            Console.WriteLine("Enter pressure of a gas");
                            double pressure = Convert.ToDouble(Console.ReadLine());
                            gasContainer.FillTheContainer(cargoMass, pressure);
                            containers.Add(gasContainer);
                            break;
                        case "L":
                            var bulkContainer = new BulkContainer(0, height, containerMass, containerDepth,maxCargoMass);
                            Console.WriteLine("Is a gas dangerous true/false");
                            bool isDangerous = Convert.ToBoolean(Console.ReadLine());
                            try
                            {
                                bulkContainer.FillTheContainer(cargoMass, isDangerous);
                            }
                            catch (OverfillException e)
                            {
                                Console.WriteLine(e);
                                
                            }
                            
                            containers.Add(bulkContainer);
                            break;
                        case "C":
                            var refrigeratedContainer = new RefrigeratedContainer(0, height, containerMass, containerDepth,maxCargoMass);
                            int n = 1;
                            foreach (var product in Products.ListOfAllProducts)
                            {
                                Console.WriteLine(n++ + " - " + product.Key);
                            }
                            Console.WriteLine("What product do you want to load");
                            n = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter temperature");
                            double temperature = Convert.ToInt32(Console.ReadLine());
                            try
                            {
                                refrigeratedContainer.FillTheContainer(cargoMass, Products.ListOfAllProducts[n-1],temperature);
                            }
                            catch (OverfillException e)
                            {
                                Console.WriteLine(e);
                                
                            }
                            
                            containers.Add(refrigeratedContainer);
                            break;
                    }
                    break;
                case 4:
                    if (containers.Count == 0)
                    {
                        break;
                    }
                    int j = 1;
                    foreach (var container in containers)
                    {
                        Console.WriteLine(j++ +" - "+ container);
                    }
                    Console.WriteLine("Enter the number of container to load (to load many containers at once, enter every number using space)");
                    string indexesToLoad = Console.ReadLine();
                    var indexArray = indexesToLoad.Split(" ").ToArray();
                    foreach (var str in indexArray)
                    {
                        bool loaded = ChooseShip("Enter the number of ship where to load", containerShips).LoadNewContainerOnBoard(containers[Convert.ToInt32(str) - 1]);
                        if (loaded)
                        {
                            containers.RemoveAt(Convert.ToInt32(str) - 1);
                        }
                    }
                    break;
                case 5:
                    containers.Add(ChooseShip("Enter the number of ship from where to unload", containerShips).UnloadContainerFromBoard());
                    break;
                case 6:
                    ChooseShip("Enter the number of ship from where to unload", containerShips).TransportContainerFromThisShipToAnother(ChooseShip("Enter the number of ship where to transport cargo", containerShips));
                    break;
                case 7:
                    Container containerToLoad = ChooseContainer("Enter the number of container to load", containers);
                    Container containerFromShip = ChooseShip("Enter the number of ship where to substitute container", containerShips).SubstituteOneContainerByAnother(containerToLoad);
                    if (!containerFromShip.Equals(containerToLoad))
                    {
                        containers.Add(containerFromShip);
                    }

                    break;
                case 8:
                    ChooseContainer("Enter the number of container to unload", containers).EmptyTheContainer();
                    break;
                case 9:
                    Container toFill = ChooseContainer("Enter the number of container to load", containers);
                    switch (toFill.SerialNumber.ToArray()[4])
                    {
                        case 'G':
                            Console.WriteLine("Enter cargo mass");
                            cargoMass = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Enter pressure of a gas");
                            double pressure = Convert.ToDouble(Console.ReadLine());
                            try
                            {
                                ((GasContainer)toFill).FillTheContainer(cargoMass, pressure);
                            }
                            catch (OverfillException e)
                            {
                                Console.WriteLine(e);
                            }
                            break;
                        case 'L':
                            Console.WriteLine("Enter cargo mass");
                            cargoMass = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Is a gas dangerous true/false");
                            bool isDangerous = Convert.ToBoolean(Console.ReadLine());
                            try
                            {
                                ((BulkContainer)toFill).FillTheContainer(cargoMass, isDangerous);
                            }
                            catch (OverfillException e)
                            {
                                Console.WriteLine(e);
                            }
                            
                            break;
                        case 'C':
                            int n = 1;
                            foreach (var product in Products.ListOfAllProducts)
                            {
                                Console.WriteLine(n++ + " - " + product.Key);
                            }
                            Console.WriteLine("What product do you want to load");
                            n = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter temperature");
                            double temperature = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Enter cargo mass");
                            cargoMass = Convert.ToDouble(Console.ReadLine());
                            try
                            {
                                ((RefrigeratedContainer)toFill).FillTheContainer(cargoMass,Products.ListOfAllProducts[n-1],temperature );
                            }
                            catch (OverfillException e)
                            {
                                Console.WriteLine(e);
                            }
                            break;
                    }
                    break;
                    
                    
            }
        }
    }

    private static ContainerShip ChooseShip(string textToShow, List<ContainerShip> containerShips)
    {
        int j = 1;
        foreach (var ship in containerShips)
        {
            Console.WriteLine(j++ +" - "+ ship);
        }
        Console.WriteLine(textToShow);
        int shipIndex = Convert.ToInt32(Console.ReadLine());
        if (shipIndex > containerShips.Count)
        {
            throw new ArgumentException("No ship with this id");
        }
        return containerShips[shipIndex - 1];
    }

    private static Container ChooseContainer(string textToShow, List<Container> containers)
    {
        int j = 1;
        foreach (var container in containers)
        {
            Console.WriteLine(j++ +" - "+ container);
        }
        Console.WriteLine("Enter the number of container to unload");
        string indexesToLoad = Console.ReadLine();
        return containers[Convert.ToInt32(indexesToLoad)-1];
    }
    
}