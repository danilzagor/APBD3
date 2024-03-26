namespace APBD3.Classes;

public class ContainerShip(
    double speed,
    int maxNumberOfContainers,
    double maxWeightOfAllContainers)
{
    private double CurrentWeightOnBoard { get; set; }

    public List<Container> ContainersOnBoard { get; protected set; } = new();
    public double Speed { get; protected set; } = speed;
    public int MaxNumberOfContainers { get; protected set; } = maxNumberOfContainers;
    public double MaxWeightOfAllContainers { get; protected set; } = maxWeightOfAllContainers;

    public bool LoadNewContainerOnBoard(Container container)
    {
        if (container.ContainerMass + container.CargoMass + CurrentWeightOnBoard / 1000 < maxWeightOfAllContainers)
            if (ContainersOnBoard.Count + 1 <= MaxNumberOfContainers)
            {
                ContainersOnBoard.Add(container);
                CurrentWeightOnBoard += container.ContainerMass+container.CargoMass;
                return true;
            }
            else
            {
                Console.WriteLine("Maximum number of container on board exceeded");
                return false;
            }

        Console.WriteLine("Maximum weight of containers on board exceeded");
        return false;
    }

    public void LoadNewContainerOnBoard(List<Container> containers)
    {
        double weightOfAllContainers = containers.Sum(container => container.ContainerMass);
        if (weightOfAllContainers + CurrentWeightOnBoard / 1000 < maxWeightOfAllContainers)
        {
            if (ContainersOnBoard.Count + containers.Count <= MaxNumberOfContainers)
            {
                ContainersOnBoard.AddRange(containers);
                CurrentWeightOnBoard += weightOfAllContainers;
            }
            else Console.WriteLine("Maximum number of container on board exceeded");
        }
        else Console.WriteLine("Maximum weight of containers on board exceeded");
    }

    public Container UnloadContainerFromBoard()
    {
        Console.WriteLine("Enter a container serial number to unload container");
        var containerToRemove = ChooseContainer();
        ContainersOnBoard.Remove(containerToRemove);
        CurrentWeightOnBoard -= containerToRemove.ContainerMass+containerToRemove.CargoMass;
        return containerToRemove;
    }

    public void TransportContainerFromThisShipToAnother(ContainerShip toTransport)
    {
        Console.WriteLine("Enter a container serial number to transport container");
        var containerToTransport = ChooseContainer();
        if (containerToTransport != null && toTransport.LoadNewContainerOnBoard(containerToTransport))
        {
            ContainersOnBoard.Remove(containerToTransport);
            CurrentWeightOnBoard -= containerToTransport.ContainerMass + containerToTransport.CargoMass;
            Console.WriteLine("Success!");
        }
        else Console.WriteLine("Couldn't transport container");
    }

    public Container SubstituteOneContainerByAnother(Container newContainer)
    {
        Console.WriteLine("Enter a container serial number to change container");
        var containerToChange = ChooseContainer();
        if (!ContainersOnBoard.Equals(null))
        {
            ContainersOnBoard.Remove(containerToChange);
            if (LoadNewContainerOnBoard(newContainer))
            {
                CurrentWeightOnBoard -= containerToChange.ContainerMass + containerToChange.CargoMass;
                Console.WriteLine("Success");
            }
                
            else
            {
                ContainersOnBoard.Add(containerToChange);
                Console.WriteLine("Couldn't substitute old container by new");
                return newContainer;
            }
        }

        return containerToChange;
    }

    private Container ChooseContainer()
    {
        foreach (var container in ContainersOnBoard)
        {
            Console.WriteLine(container.SerialNumber);
        }

        string input = Console.ReadLine();
        Container containerToChoose = null;
        for (int i = 0; i < ContainersOnBoard.Count; i++)
        {
            if (ContainersOnBoard[i].SerialNumber==input)
                containerToChoose = ContainersOnBoard[i];
        }

        return containerToChoose;
    }

    public void GetInformationAboutEveryContainerOnBoard()
    {
        ContainersOnBoard.ForEach(container =>
        {
            Console.WriteLine(container.SerialNumber);
        });
    }

    public override string ToString()
    {
        return $"{nameof(CurrentWeightOnBoard)}: {CurrentWeightOnBoard},  {nameof(Speed)}: {Speed}, {nameof(MaxNumberOfContainers)}: {MaxNumberOfContainers}, {nameof(MaxWeightOfAllContainers)}: {MaxWeightOfAllContainers}";
    }
}