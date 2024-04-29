// See https://aka.ms/new-console-template for more information

using Modeling_Console;

Pipeline pipeline = new Pipeline();
pipeline.StartWOBuffer(1000,5);
pipeline.StartWithBuffer(1000,5);


















 /*public void StartWOBuffer(int time, int numOfDevices)
    {
        //Список всех деталей
        List<Detail> allDetails = new ();
        //Список деталей отказа
        List<Detail> rejectionDetails = new();
        //Список обслуженных деталей
        List<Detail> usedDetails = new();
        
        Detail[] detailOnPipeline = new Detail[numOfDevices];
        
        List<Device> devices = new();
        
        for (int i = 0; i < numOfDevices; i++)
            devices.Add(new Device());
        
        
        for (int i = 0; i < time; i++)
        {
            allDetails = SetRequest(allDetails);
            
            //Работа станков в течении минуты
            foreach (var device in devices)
            {
                if (device.State == true)
                {
                    device.MoveTime();
                    if (device.State == false && device.DetailOnDevice != null)
                    {
                        usedDetails.Add(device.DetailOnDevice);
                        device.RemoveDetail();
                    }
                }
            }
            
            //Cмещение деталей и добавление их на конвейер
            if (CheckPipeIsEmpty(detailOnPipeline))
            {
                //Добавление детали на конвейер если конвейер пустой с конца
                if (detailOnPipeline[0] == null)
                {
                    allDetails[0].MovePosition();
                    detailOnPipeline[0] = allDetails[0];
                    allDetails.RemoveAt(0);
                }
            }
            else
            {
                int position = 0;
                for (int j = detailOnPipeline.Length - 1; j > -1; j--)
                {
                    if (detailOnPipeline[j] != null)
                    {
                        position = j;
                        break;
                    }
                }
                
                for (int j = position; j >-1; j--)
                {
                    if (detailOnPipeline[j] != null)
                    {

                        if (j == detailOnPipeline.Length - 1)
                        {
                            rejectionDetails.Add(detailOnPipeline[j]);
                            detailOnPipeline[j] = null;
                            continue;
                        }
                        
                        detailOnPipeline[j].MovePosition();
                        detailOnPipeline[j + 1] = detailOnPipeline[j];
                        detailOnPipeline[j] = null;
                    }
                }
                allDetails[0].MovePosition();
                detailOnPipeline[0] = allDetails[0];
                allDetails.RemoveAt(0);
            }
            
            //Загрузка деталей на станок
            for (int j = 0; j < devices.Count; j++)
            {
                if (devices[j].State == false)
                {
                    if (detailOnPipeline[j] != null)
                    {
                        devices[j] = SetExpTime(devices[j], detailOnPipeline[j]);
                        detailOnPipeline[j] = null;
                    }
                }
            }
        }
        
        for (int j = 0; j < devices.Count; j++)
        {
            if (devices[j].State == true)
            {
                allDetails.Add(devices[j].DetailOnDevice);
            }
        }
        Console.WriteLine("Заявок необработано " + allDetails.Count);
        Console.WriteLine("Обработанные заявки " + usedDetails.Count);
        Console.WriteLine("Отказанные заявки " + rejectionDetails.Count);
        Console.WriteLine("Заявок всего: " + (allDetails.Count+usedDetails.Count()+rejectionDetails.Count()));
    }
    
    
    //Добавляет детали в список каждую минуту с равномерным распределением от 0 до 4
    public List<Detail> SetRequest(List<Detail> allDetails)
    {
        int countDetailsPerMinute = random.Next(1, 5);
        for (int j = 0; j < countDetailsPerMinute; j++)
            allDetails.Add(new Detail());
        return allDetails;
    }
    
    //Устанавливает время работы устройства по экспонециальному закону
    public Device SetExpTime(Device device, Detail detail)
    {
        int MathExpectation = 1;
        int time = Convert.ToInt32(
            Math.Ceiling(
                 (-MathExpectation) * Math.Log(random.NextDouble())));
        device.SetWork(detail, time);
        return device;
    }

    public bool CheckPipeIsEmpty(Detail[] detailOnPipeline)
    {
        for (int i = 0; i < detailOnPipeline.Length; i++)
        {
            if (detailOnPipeline[i] != null)
                return false;
        }
        return true;
    }
    
    */
    
    
    /*
     //Смотреть промежуточное
            Console.WriteLine($"-------------------------shag: {i}------------------------------");
            string conv = "";
            string dit = "";
            string tvow = "";
            foreach (var VARIABLE in devices)
            {
                int sta = 0;
                if (VARIABLE.State == true)
                    sta = 1;
                tvow += " " + VARIABLE.TimeOfWork;
                dit += " " + sta;
            }
            Console.WriteLine(tvow);
            Console.WriteLine(dit);
            foreach (var VARIABLE in detailOnPipeline)
            {
                int sta = 0;
                if (VARIABLE != null)
                    sta = 1;
                conv += " " + sta;
            }
            Console.WriteLine(conv + "\n");
     */
    
    
    /*
     * //Самая первая заявка
           if (i == 0)
           {
               devices[0] = SetExpTime(devices[0], allDetails[0]);
               allDetails.RemoveAt(0);
               continue;
           }*/