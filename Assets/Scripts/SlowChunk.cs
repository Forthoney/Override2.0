using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowChunk : ShipBody
{
    public SlowChunk()
    {
        setHealth(100);
        setSpeed(4);
        setRadius(15);
    }
}
