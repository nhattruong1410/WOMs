using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public MonterMove Base { get; set; }
    public int PP { get; set; }
    public Move(MonterMove pBase)
    {
        Base = pBase;
        PP = pBase.PP;
    }
}
