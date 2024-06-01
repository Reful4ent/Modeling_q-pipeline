using System.Security.AccessControl;

namespace Modeling_DeliveryService.ConsoleV.Model;

public class Courier
{
    private int timeOfWorking = 0;
    private Queue<Order> orders = new();
    private Queue<Order> deliveredOrders = new ();
    private List<int> deliveryTimes = new ();
    private int indexOrder = 0;
    private const int MaxCourierOrders = 2;
    private bool isWorking = false;
    private bool isReturning = false;
    
    private bool isCycleBroke = false;
    private bool isDisturbance = false;
    private int timeOfDisturbance = 0;
    
    public int TimeOfWorking
    {
        get => timeOfWorking;
        private set => timeOfWorking = value;
    }

    public Queue<Order> Orders
    {
        get => orders;
        private set => orders = value;
    }
    
    public Queue<Order> DeliveredOrders
    {
        get => deliveredOrders;
        private set => deliveredOrders = value;
    }
    
    public List<int> DeliveryTimes
    {
        get => deliveryTimes;
        private set => deliveryTimes = value;
    }
    
    public bool IsWorking
    {
        get => isWorking;
        private set => isWorking = value;
    }
    
    public bool IsReturning
    {
        get => isReturning;
        private set => isReturning = value;
    }
    
    public bool IsDisturbance
    {
        get => isDisturbance;
        private set => isDisturbance = value;
    }
    
    public int TimeOfDisturbance
    {
        get => timeOfDisturbance;
        private set => timeOfDisturbance = value;
    }
    
    public int IndexOrder
    {
        get => indexOrder;
        private set => indexOrder = value;
    }

    public bool SetCourier(Queue<Order> orders)
    {
        if (IsWorking || IsReturning)
            return false;
        if (timeOfWorking > 1)
            return false;
        if (orders.Count > MaxCourierOrders)
            return false;
        DeliveredOrders.Clear();
        Orders.Clear();
        DeliveryTimes.Clear();
        foreach (var order in orders)
        {
            timeOfWorking += order.TimeOfDelivery * 2;
            DeliveryTimes.Add(timeOfWorking);
        }
        SetDisturbance();
        Orders = orders;
        IsWorking = true;
        IsReturning = false;
        return true;
    }

    public bool MoveTime()
    {
        if (IsWorking)
        {
            TimeOfWorking -= 1;
            if(IsDisturbance || isCycleBroke)
                TimeOfDisturbance -= 1;
            if (TimeOfDisturbance == 0)
            {
                if (IsDisturbance)
                {
                    TimeOfWorking /= 2;
                    DeliveryTimes[IndexOrder] /= 2;
                    orders.Peek().TimeOfDelivery /= 2;
                    IsDisturbance = false;
                    TimeOfDisturbance = -1;
                    Console.WriteLine("Отмена заказа");
                }
                else if (isCycleBroke)
                {
                    TimeOfWorking += DeliveryTimes[IndexOrder] * 2;
                    DeliveryTimes[IndexOrder] *= 2;
                    orders.Peek().TimeOfDelivery *= 2;
                    isCycleBroke = false;
                    TimeOfDisturbance = -1;
                    Console.WriteLine("Велик сломался!");
                }
            }
            if (!IsReturning)
            {
                bool flag = false;
                foreach (var order in Orders)
                {
                    if (order.MoveTime() && order.IsDelivered)
                    {
                        flag = true; 
                        deliveredOrders.Enqueue(order);
                        if (orders.Count > 1)
                            IndexOrder += 1;
                    }
                    else break;
                }
                if(IndexOrder > 0)
                    orders.Dequeue();
                if (flag)
                    IsReturning = true;
            }
            if (TimeOfWorking < 1)
            {
                TimeOfWorking = 0;
                IsWorking = false;
                IsReturning = false;
                IndexOrder = 0;
                TimeOfDisturbance = 0;
                return true;
            }
        }
        return false;
    }

    private void SetDisturbance()
    {
        Random random = new Random();
        double number = 1;
        int counter = 0;

        for (int i = 0; i < 50; i++)
        {
            while (number > Math.Exp(-5))
            {
                number *= random.NextDouble();
                counter++;
            }
            if (counter > 8)
            {
                double checkDistrubance = random.NextDouble();
                if (checkDistrubance < 0.5d)
                {
                    IsDisturbance = true;
                    TimeOfDisturbance = random.Next(0, timeOfWorking);
                }
                else
                {
                    isCycleBroke = true;
                    TimeOfDisturbance = random.Next(0, timeOfWorking);
                }
            }
        }
    }
}