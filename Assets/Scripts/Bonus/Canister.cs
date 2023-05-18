using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canister : Bonus
{
    protected override void OnMouseDown()
    {
        BonusLevel = 20;
        base.OnMouseDown();
    }
}
