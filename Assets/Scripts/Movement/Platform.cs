using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Platform : MonoBehaviour
{
    public event UnityAction Movement;

    [SerializeField] private int _victimCount;
    [SerializeField] private float _impulse;
    [SerializeField] private float _gravity;

    private float _speed;

    private Vector3 _oldPoint;
    private Vector3 _newPoint;

    private bool _isStopped;
    private bool _isReadyEvacuation;

    private SpawnPoint _currentPoint;
    private EvacuationPlatform _currentPlatform;

    public float Speed => _speed;
    public bool IsStopped => _isStopped;
    public bool IsReadyEvacuation => _isReadyEvacuation;

    private void Stop()
    {
        float duration = 0.7f;

        _gravity = 0;

        if (_currentPoint != null)
        {
            transform.DOLocalMoveY(_currentPoint.transform.position.y - 0.9f, duration).OnComplete(() => _isStopped = true);
        }
        else if (_currentPlatform != null)
        {
            transform.DOLocalMoveY(_currentPlatform.transform.position.y - 0.9f, duration).OnComplete(() => _isStopped = true);
        }
    }

    private void Run()
    {
        _isStopped = false;
        _isReadyEvacuation = false;
        _gravity = 1f;

        if (_victimCount > 0)
        {
            _gravity *= _victimCount / 2;
        }
    }

    private void Update()
    {
        _oldPoint = transform.position;
        transform.position -= Vector3.up * _gravity * Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && !_isStopped)
        {
            Move();
        }
    }

    private void FixedUpdate()
    {
        float time = 1f;
        _newPoint = transform.position;
        float dist = Vector3.Distance(_oldPoint, _newPoint);
        _speed = (dist / (Time.deltaTime * time)) * 3.8f;

        if (_currentPlatform == null && _currentPoint == null)
        {
            Run();
        }

    }

    private void Move()
    {
        float duration = 0.5f;
        Vector3 needVector = transform.position + Vector3.up * _impulse * Time.deltaTime;
        Movement?.Invoke();
        transform.DOMove(needVector, duration);

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Victim victim))
        {
            _victimCount++;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent(out Victim victim))
        {
            _victimCount--;
        }
    }


    private void OnTriggerStay(Collider collision)
    {
        float stopSpeed = 7f;

        if (collision.TryGetComponent(out SpawnPoint stoppingPoint) && _speed <= stopSpeed && !_isStopped)
        {
            _currentPoint = stoppingPoint;
            Stop();
            stoppingPoint.IsDocked = true;
            _isStopped = true;
        }
        else if (collision.TryGetComponent(out EvacuationPlatform evacuationPlatform) && _speed <= stopSpeed && !_isStopped)
        {
            _isReadyEvacuation = true;
            _currentPlatform = evacuationPlatform;
            Stop();
            evacuationPlatform.IsDocked = true;
            _isStopped = true;
        }
    }
}
