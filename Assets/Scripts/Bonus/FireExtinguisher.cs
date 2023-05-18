using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : Bonus
{
    protected override void OnMouseDown()
    {
        BonusLevel = -10;
        base.OnMouseDown();
    }
}
