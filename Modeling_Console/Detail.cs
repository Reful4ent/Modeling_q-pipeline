namespace Modeling_Console;

public class Detail : ICloneable
{
    private int position = -1;
    public int Position
    {
        get => position;
        set => position = value;
    }
    
    public Detail(){}
    Detail(int position)
    {
        Position = position;
    }

    public void MovePosition() => Position += 1;
    public object Clone() => new Detail(Position);
    
}