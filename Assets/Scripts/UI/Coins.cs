using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public TMPro.TextMeshProUGUI CoinsCount;
    public int Count;

    private void Update()
    {
        CoinsCount.text = Count.ToString();
    }
}
