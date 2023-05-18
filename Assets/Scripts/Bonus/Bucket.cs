using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : Bonus
{
    protected override void OnMouseDown()
    {
        BonusLevel = -5;
        base.OnMouseDown();
    }
}
