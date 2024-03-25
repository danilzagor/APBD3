namespace APBD3.Classes;

public class ContainerShip(
    double speed,
    int maxNumberOfContainers,
    double maxWeightOfAllContainers)
{
    private double CurrentWeightOnBoard { get; set; }

    public List<Container> ContainersOnBoard { get; protected set; }
    public double Speed { get; protected set; } = speed;
    public int MaxNumberOfContainers { get; protected set; } = maxNumberOfContainers;
    public double MaxWeightOfAllContainers { get; protected set; } = maxNumberOfContainers;

    public bool LoadNewContainerOnBoard(Container container)
    {
        if (container.ContainerMass + CurrentWeightOnBoard / 1000 < maxWeightOfAllContainers)
            if (ContainersOnBoard.Count + 1 <= MaxNumberOfContainers)
            {
                ContainersOnBoard.Add(container);
                CurrentWeightOnBoard += container.ContainerMass;
                return true;
            }
            else
            {
                Console.Write("Maximum number of container on board exceeded");
                return false;
            }

        Console.Write("Maximum weight of containers on board exceeded");
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
            else Console.Write("Maximum number of container on board exceeded");
        }
        else Console.Write("Maximum weight of containers on board exceeded");
    }

    public void UnloadContainerFromBoard()
    {
        Console.Write("Write a container serial number to unload container");
        var containerToRemove = ChooseContainer();
        ContainersOnBoard.Remove(containerToRemove);
    }

    public void TransportContainerFromThisShipToAnother(ContainerShip toTransport)
    {
        Console.Write("Write a container serial number to transport container");
        var containerToTransport = ChooseContainer();
        if (containerToTransport != null && toTransport.LoadNewContainerOnBoard(containerToTransport))
        {
            ContainersOnBoard.Remove(containerToTransport);
            Console.Write("Success!");
        }
        else Console.Write("Couldn't transport container");
    }

    public void SubstituteOneContainerByAnother(Container newContainer)
    {
        Console.Write("Write a container serial number to change container");
        var containerToChange = ChooseContainer();
        if (!ContainersOnBoard.Equals(null))
        {
            ContainersOnBoard.Remove(containerToChange);
            if(LoadNewContainerOnBoard(newContainer))
                Console.Write("Success");
            else
            {
                ContainersOnBoard.Add(containerToChange);
                Console.Write("Couldn't substitute old container by new");
            }
        }
    }

    private Container ChooseContainer()
    {
        foreach (var container in ContainersOnBoard)
        {
            Console.Write(container.SerialNumber);
        }

        string input = Console.ReadLine();
        Container containerToChoose = null;
        for (int i = 0; i < ContainersOnBoard.Count; i++)
        {
            if (ContainersOnBoard[i].SerialNumber.Equals(input))
                containerToChoose = ContainersOnBoard[i];
        }

        return containerToChoose;
    }

    public void GetInformationAboutEveryContainerOnBoard()
    {
        ContainersOnBoard.ForEach(container =>
        {
            Console.Write(container.SerialNumber);
        });
    }

    public override string ToString()
    {
        return $"{nameof(CurrentWeightOnBoard)}: {CurrentWeightOnBoard}, {nameof(ContainersOnBoard)}: {ContainersOnBoard}, {nameof(Speed)}: {Speed}, {nameof(MaxNumberOfContainers)}: {MaxNumberOfContainers}, {nameof(MaxWeightOfAllContainers)}: {MaxWeightOfAllContainers}";
    }
}