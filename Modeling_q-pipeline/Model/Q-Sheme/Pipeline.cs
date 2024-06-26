namespace Modeling_q_pipeline.Model;

public class Pipeline
{
    private Random random = new Random();
    private IStatistics statistics;
    public int countAllDetails = 0;
    public Pipeline(IStatistics statistics)
    {
        this.statistics = statistics;
    }
    
    public async Task Start(int time, int countOfDevices, int bufferSize)
    {
        countAllDetails = 0;
        if (statistics.IsGettedStatistic)
            statistics.ResetStatistic(countOfDevices);
        //Список всех деталей
        List<Detail> unprocessedDetails = new ();
        //Список деталей отказа
        List<Detail> rejectionDetails = new();
        //Список обслуженных деталей
        List<Detail> usedDetails = new();
        
        Detail[][] detailOnPipeline = new Detail[countOfDevices][];
        
        List<Device> devices = new();
        List<Buffer> buffers = new();

        for (int i = 0; i < countOfDevices; i++)
        {
            devices.Add(new Device());
            buffers.Add(new Buffer(bufferSize));
        }

        await Task.Run(() =>
        {
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

                //Перекладывание детали из буфера на станок
                for (int j = 0; j < buffers.Count; j++)
                    if (devices[j].State == false && buffers[j].DetailInBuffer.Count != 0)
                    {
                        devices[j] = SetExpTime(devices[j], buffers[j].PullOutDetail());
                        statistics.TimeWorkingDiveces[j].Add(devices[j].TimeOfWork);
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
                
                //Загрузка деталей в буфер
                for (int j = 0; j < buffers.Count; j++)
                {
                    if (buffers[j].State == false)
                    {
                        if (detailOnPipeline[j] != null)
                        {
                            while (buffers[j].State != true)
                            {
                                if(detailOnPipeline[j] == null)
                                    break;
                                if(buffers[j].PutDetail(detailOnPipeline[j][0])) 
                                    detailOnPipeline[j] = ChangeRequest(detailOnPipeline[j]);
                            }
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

                if (buffers[j].State == true)
                    while (buffers[j].DetailInBuffer.Count!=0)
                        unprocessedDetails.Add(buffers[j].PullOutDetail());
            }
            statistics.CountRejectionDetails = rejectionDetails.Count;
            statistics.CountUnprocessedDetails = unprocessedDetails.Count;
            statistics.CountUsedDetails = usedDetails.Count;
            statistics.CountAllDetails = countAllDetails;
        });
    }
    #region Methods_For_Working_With_Pipeline
    //Добавляет детали в список каждую минуту с равномерным распределением от 0 до 4
    public Detail[] SetRequest()
    {
        int countDetailsPerMinute = random.Next(1, 5);
        Detail[] tempDetails = new Detail[countDetailsPerMinute];
        countAllDetails += countDetailsPerMinute;
        for (int j = 0; j < countDetailsPerMinute; j++)
            tempDetails[j] = new Detail();
        
        return tempDetails;
    }

    //Изменяет количество деталей на ленте
    public Detail[] ChangeRequest(Detail[] request)
    {
        Detail[] tempDetails = request.Reverse().ToArray();
        Array.Resize(ref tempDetails,tempDetails.Length - 1);
        
        if (tempDetails.Length == 0)
            return null;
        
        return tempDetails.Reverse().ToArray();
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

    public bool CheckPipeIsEmpty(Detail[][] detailOnPipeline)
    {
        for (int i = 0; i < detailOnPipeline.Length; i++)
        {
            if (detailOnPipeline[i] != null)
                return false;
        }
        return true;
    }
    #endregion
}