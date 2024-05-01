﻿// See https://aka.ms/new-console-template for more information

using Modeling_Console;

int countOfDevices = Convert.ToInt32(Console.ReadLine());
int bufferSize = Convert.ToInt32(Console.ReadLine());
Statistics statistics = new Statistics(countOfDevices);
Pipeline pipeline = new Pipeline(statistics);
pipeline.Start(1000,countOfDevices,0);
Console.WriteLine("Заявок не обработано " + statistics.countUnprocessedDetails);
Console.WriteLine("Обработанные заявки " + statistics.countUsedDetails);
Console.WriteLine("Отказанные заявки " + statistics.countRejectionDetails);
Console.WriteLine("Заявок всего: " + statistics.countAllDetails);
statistics.SetMainStatistics();
Console.WriteLine(statistics.PercentUsedDetails);
Console.WriteLine(statistics.PercentRejectionDetails);
Console.WriteLine(statistics.PercentUnprocessedDetails);
Console.WriteLine();

pipeline.Start(1000,countOfDevices,bufferSize);
Console.WriteLine("Заявок не обработано " + statistics.countUnprocessedDetails);
Console.WriteLine("Обработанные заявки " + statistics.countUsedDetails);
Console.WriteLine("Отказанные заявки " + statistics.countRejectionDetails);
Console.WriteLine("Заявок всего: " + statistics.countAllDetails);
statistics.SetMainStatistics();
Console.WriteLine(statistics.PercentUsedDetails);
Console.WriteLine(statistics.PercentRejectionDetails);
Console.WriteLine(statistics.PercentUnprocessedDetails);
Console.WriteLine();



/*
 * public void StartWOBuffer(int time, int countOfDevices)
    {
        countAllDetails = 0;
        //Список всех деталей
        List<Detail> unprocessedDetails  = new ();
        //Список деталей отказа
        List<Detail> rejectionDetails = new();
        //Список обслуженных деталей
        List<Detail> usedDetails = new();
        
        Detail[][] detailOnPipeline = new Detail[countOfDevices][];
        
        List<Device> devices = new();
        
        for (int i = 0; i < countOfDevices; i++)
            devices.Add(new Device());
        
        
        for (int i = 0; i < time; i++)
        {
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
            {    //Добавление детали на конвейер если конвейер пустой с конца
                if (detailOnPipeline[0] == null)
                    detailOnPipeline[0] = SetRequest();
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
                            for (int k = 0; k < detailOnPipeline[j].Length; k++)
                                rejectionDetails.Add(detailOnPipeline[j][k]);
                            
                            detailOnPipeline[j] = null;
                            continue;
                        }
                        detailOnPipeline[j + 1] = detailOnPipeline[j];
                        detailOnPipeline[j] = null;
                    }
                }
                detailOnPipeline[0] = SetRequest();
            }
            
            //Загрузка деталей на станок
            for (int j = 0; j < devices.Count; j++)
            {
                if (devices[j].State == false)
                {
                    if (detailOnPipeline[j] != null)
                    {
                        devices[j] = SetExpTime(devices[j], detailOnPipeline[j][0]);
                        detailOnPipeline[j] = ChangeRequest(detailOnPipeline[j]);
                        statistics.TimeWorkingDiveces[j].Add(devices[j].TimeOfWork);
                    }
                }
            }
            statistics.EffectivityStatistics.Add(i,((double)usedDetails.Count/countAllDetails));
        }
        
        for (int j = 0; j < devices.Count; j++)
        {
            if (devices[j].State == true) 
                unprocessedDetails.Add(devices[j].DetailOnDevice);
            

            if (detailOnPipeline[j] != null)
            {
                for (int d = 0; d < detailOnPipeline[j].Length; d++)
                    unprocessedDetails.Add(detailOnPipeline[j][d]);
            }
        }
        statistics.countRejectionDetails = rejectionDetails.Count;
        statistics.countUnprocessedDetails = unprocessedDetails.Count;
        statistics.countUsedDetails = usedDetails.Count;
        statistics.countAllDetails = countAllDetails;
    }
    
 */
