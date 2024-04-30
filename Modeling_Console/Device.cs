namespace Modeling_Console;

public class Device
{
    private bool state = false;
    private int timeOfWork = 0;
    private Detail detailOnDevice;

    #region Properties
    public bool State
    {
        get => state;
        private set => state = value;
    }
    public int TimeOfWork
    {
        get => timeOfWork;
        private set => timeOfWork = value;
    }
    public Detail DetailOnDevice
    {
        get => detailOnDevice;
        private set => detailOnDevice = value;
    }
    #endregion
    

    public void SetWork(Detail detail, int timeOfWork)
    {
        DetailOnDevice = detail;
        TimeOfWork = timeOfWork;
        State = true;
    }
    
    public void MoveTime()
    {
        TimeOfWork -= 1;
        if (timeOfWork < 1)
        {
            TimeOfWork = 0;
            State = false;
        }
    }
    
    public void RemoveDetail() => DetailOnDevice = null;
}