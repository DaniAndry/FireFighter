using UnityEngine;

public class EvacuationPlatform : MonoBehaviour
{
    public bool IsDocked;

    private float _timer;
    private float _delay = 3;

    private void Start()
    {
        IsDocked = false;
    }

    private void FixedUpdate()
    {
        if (IsDocked)
        {
            _timer += Time.deltaTime;

            if (_timer >= _delay)
            {
                IsDocked = false;
                Destroy(gameObject);
            }
        }
    }
}


