using UnityEngine;

public class MoveInstance
{
    public MoveData MoveData { get; private set; }

    public MoveInstance(MoveData moveData)
    {
        MoveData = moveData;
    }
}
