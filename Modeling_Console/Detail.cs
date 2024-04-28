namespace Modeling_Console;

public class Detail
{
    private bool isServiced = false;
    private int position = -1;
    public bool IsServiced
    {
        get => isServiced;
        private set => isServiced = value;
    }

    public int Position
    {
        get => position;
        set => position = value;
    }


    public void MovePosition() => Position += 1;
}