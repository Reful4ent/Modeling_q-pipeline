using Modeling_q_pipeline.Model;

namespace Modeling_DeliveryService.ConsoleV.Model;

public class Service
{
    private Random random = new();
    public IStatistics statistics;
    private int countAllOrders = 0;

    public Service(IStatistics statistics)
    {
        this.statistics = statistics;
    }
    
    public void Start(int time, int countOfCouriers)
    {
        Queue<Order> orderList = new();
        List<Courier> couriersList = new();
        int countUsedOrders = 0;
        int countRejectionOrders = 0;
        int countOfUnprocessedOrders = 0;
        countAllOrders = 0;
        for(int i = 0; i < countOfCouriers; i++)
            couriersList.Add(new Courier());

        for (int i = 0; i < time; i++)
        {
            if (i % 5 == 0)
                SetRequest(orderList);
            for (int j = 0; j < countOfCouriers; j++)
            {
                if (couriersList[j].MoveTime())
                {
                    for (int k = 0; k < couriersList[j].DeliveredOrders.Count; k++)
                    {
                        if (couriersList[j].DeliveredOrders.Peek().IsRefused)
                            countRejectionOrders += 1;
                        else
                            countUsedOrders += 1;
                        couriersList[j].DeliveredOrders.Dequeue();
                        k--;
                    }
                }
            }
            
            //Передача заказа курьеру
            foreach (var courier in couriersList)
            {
                if (!courier.IsWorking && !courier.IsReturning && (orderList.Count() > 0))
                {
                    int countOfOrders =  random.Next(1, 2);
                    Queue<Order> tempList = new Queue<Order>();
                    for (int j = 0; j < countOfOrders; j++)
                    {
                        if(orderList.Count == 0)
                            break;
                        tempList.Enqueue(orderList.Dequeue());
                    }
                    courier.SetCourier(tempList);
                }
            }
            statistics.EffectivityStatistics.Add(i,(double)countUsedOrders/countAllOrders);
        }

        for (int d = 0; d < couriersList.Count; d++)
        {
            if (couriersList[d].DeliveredOrders.Count == 0)
                countOfUnprocessedOrders += couriersList[d].Orders.Count();
        }
        countOfUnprocessedOrders += orderList.Count;
        statistics.CountRejectionDetails = countRejectionOrders;
        statistics.CountUnprocessedDetails = countOfUnprocessedOrders;
        statistics.CountUsedDetails = countUsedOrders;
        statistics.CountAllDetails = countAllOrders;
        //Console.WriteLine($"Кол-во обработанных заявок:{countUsedOrders}\nКол-во откказанных заявок:{countRejectionOrders}\nКол-во необработанных заявок: {countOfUnprocessedOrders}\nКол-во всех заявок:{countAllOrders}");
    }
    
    private void SetRequest(Queue<Order> orderListToAdd)
    {
        int countOrders = random.Next(1, 4);
        countAllOrders += countOrders;
        for (int j = 0; j < countOrders; j++)
            orderListToAdd.Enqueue(SetExpTimeNewOrder());
    }

    private Order SetExpTimeNewOrder()
    {
        int MathExpectation = 10;
        int time = Convert.ToInt32(
            Math.Ceiling(
                (-MathExpectation) * Math.Log(1-random.NextDouble())));
        return new Order(time);
    }

}