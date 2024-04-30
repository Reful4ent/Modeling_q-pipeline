namespace Modeling_Console;

public class Buffer
{
    private bool state = false;
    private int bufferSize = 0;
    private Queue<Detail> detailInBuffer = new();

    public Buffer(int bufferSize)
    {
        this.bufferSize = bufferSize;
    }
    
    public bool State
    {
        get => state;
        private set => state = value;
    }

    public Queue<Detail> DetailInBuffer
    {
        get => detailInBuffer;
        private set => detailInBuffer = value;
    }


    public bool PutDetail(Detail detail)
    {
        if (detailInBuffer.Count == bufferSize)
            return false;
        DetailInBuffer.Enqueue(detail);
        if(detailInBuffer.Count == bufferSize)
            State = true;
        return true;
    }

    public Detail PullOutDetail()
    {
        Detail temp = (Detail)DetailInBuffer.Dequeue().Clone();
        State = false;
        return temp;
    }
}