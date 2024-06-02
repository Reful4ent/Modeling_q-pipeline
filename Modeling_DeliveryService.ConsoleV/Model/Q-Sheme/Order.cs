namespace Modeling_DeliveryService.ConsoleV.Model;

public class Order
{
    private bool isDelivered = false;
    private bool isDelivering = false;
    private bool isRefused = false;
    private int timeOfDelivery = 0;

    public bool IsDelivered
    {
        get => isDelivered;
        private set => isDelivered = value;
    }
    
    public bool IsDelivering
    {
        get => isDelivering;
        private set => isDelivering = value;
    }
    
    public bool IsRefused
    {
        get => isRefused;
        set => isRefused = value;
    }

    public int TimeOfDelivery
    {
        get => timeOfDelivery;
        set => timeOfDelivery = value;
    }

    public Order(int timeOfDelivery)
    {
        SetOrder(timeOfDelivery);
    }

    private bool SetOrder(int timeOfDelivery)
    {
        if (timeOfDelivery < 1)
            return false;
        IsDelivered = false;
        IsDelivering = true;
        IsRefused = false;
        TimeOfDelivery = timeOfDelivery;
        return true;
    }

    public bool MoveTime()
    {
        TimeOfDelivery -= 1;
        if (TimeOfDelivery > 1)
            return false;
        IsDelivered = true;
        IsDelivering = false;
        TimeOfDelivery = 0;
        return true;
    }
    
}