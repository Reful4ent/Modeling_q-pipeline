namespace Modeling_Console;

public class Device
{
    private bool state = false;
    private int timeOfWork = 0;
    
    
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
}