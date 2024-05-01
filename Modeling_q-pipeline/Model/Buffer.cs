namespace Modeling_q_pipeline.Model;

public class Buffer
{
    private bool state = false;
    private int bufferSize = 0;
    private Queue<Detail> detailInBuffer = new();

    public Buffer(int bufferSize)
    {
        this.bufferSize = bufferSize;
        if (this.bufferSize == 0)
            State = true;
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