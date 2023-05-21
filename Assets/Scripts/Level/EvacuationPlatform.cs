using System.Collections;
using UnityEngine;

public class EvacuationPlatform : MonoBehaviour
{
    public bool IsDocked;

    private float _delay = 3;

    private void Start()
    {
        IsDocked = false;
    }

    private IEnumerator DockedTimer()
    {
        yield return new WaitForSeconds(_delay);
        IsDocked = false;
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (IsDocked)
        {
            StartCoroutine(DockedTimer());
        }
    }
}
