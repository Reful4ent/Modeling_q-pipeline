namespace Modeling_Console;

public class Buffer
{
    private bool state = false;
    private Detail detailInBuffer;
    
    public bool State
    {
        get => state;
        set => state = value;
    }

    public Detail DetailInBuffer
    {
        get => detailInBuffer;
        set => detailInBuffer = value;
    }


    public void PutDetail(Detail detail)
    {
        DetailInBuffer = detail;
        State = true;
    }

    public Detail PullOutDetail()
    {
        Detail temp = (Detail)DetailInBuffer.Clone();
        DetailInBuffer = null;
        State = false;
        return temp;
    }
}