using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _gameOver;
    private float _currentYPosition;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += Vector3.up * _speed * Time.deltaTime;
    }

    public void UseBonus(int bonus)
    {
        _currentYPosition += bonus;

        UpdatePosition();
    }

    private void UpdatePosition()
    {
        Vector3 newPosition = transform.position;
        newPosition.y = _currentYPosition;
        transform.position = newPosition;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Platform platform))
        {
            _gameOver.SetActive(true);
            Invoke(nameof(Stop), 1f);
        }
    }

    private void Stop()
    {
        Time.timeScale = 0;
    }
}
