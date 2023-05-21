using System.Collections;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private bool _isDocked;
    private float _delay = 3;

    public bool IsDocked => _isDocked;

    private void Start()
    {
        _isDocked = false;
    }

    public void Docked()
    {
        _isDocked = true;
    }

    private IEnumerator DockedTimer()
    {
        yield return new WaitForSeconds(_delay);
        _isDocked = false;
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (_isDocked)
        {
            StartCoroutine(DockedTimer());
        }
    }
}
