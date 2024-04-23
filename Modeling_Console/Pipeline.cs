namespace Modeling_Console;

public class Pipeline
{
    public void StartWOBuffer(int time, int numOfDevices)
    {
        List<Detail> rejectionDetails = new();
        List<Detail> usedDetails = new();
        List<Device> devices = new();
        for (int i = 0; i < numOfDevices; i++)
            devices.Add(new Device());
    }
}