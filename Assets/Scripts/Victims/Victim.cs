using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Victim : MonoBehaviour
{
    private float _duration;
    private float _distanceThreshold = 2f;
    private float _alignmentPossition = 1.1f;
    private Platform _platform;
    private Vector3 _endPosition;
    private Animator _animator;
    private bool _isOnPlatform;
    private int _coinPrice = 100;
    private int _isStopAnimation;
    private Coins _coins;

    public void Init(Platform platform, Vector3 evacuationPoint, Coins coins)
    {
        _platform = platform;
        _endPosition = evacuationPoint;
        _coins = coins;
    }

    private void Start()
    {
        _duration = Random.Range(4f, 5);
        _animator = GetComponent<Animator>();
        _isStopAnimation = Animator.StringToHash("isStop");
    }

    private void Update()
    {
        float distance = Mathf.Abs(transform.position.y - _platform.transform.position.y);
        bool isOnSameLevel = distance <= _distanceThreshold;

        if (_platform.IsStopped && isOnSameLevel || _isOnPlatform)
        {
            Move();
        }

        if (_platform.IsReadyEvacuation && _isOnPlatform)
        {
            Evacuation();
        }
        else
        {
            _endPosition.y = _platform.transform.position.y + _alignmentPossition;
        }
    }

    private void Move()
    {

        if ((Mathf.Approximately(transform.position.y, _platform.transform.position.y + _alignmentPossition)) || _isOnPlatform)
        {
            _animator.SetBool(_isStopAnimation, false);

            transform.DOMove(_endPosition, _duration);

            if (Vector3.Distance(transform.position, _endPosition) < 0.8f)
            {
                Debug.Log("Stop");
                _animator.SetBool(_isStopAnimation, true);
            }
        }

        transform.DOLocalMoveY(_platform.transform.position.y + _alignmentPossition, 0);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Platform platform))
        {
            _isOnPlatform = true;
        }
        if (collision.TryGetComponent(out Fire fire))
        {
            _coins.Count -= _coinPrice;
            Destroy(gameObject);
        }
        if (collision.TryGetComponent(out EvacuationPlatform evacuationPlatform))
        {
            _coins.Count += _coinPrice;
            Invoke(nameof(Destroy), 1f);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent(out Platform platform))
        {
            _isOnPlatform = false;
        }
    }

    private void Evacuation()
    {
        float evacuatedPosition = transform.position.x - 10f;

        _animator.SetBool(_isStopAnimation, false);
        _endPosition = new Vector3(evacuatedPosition, _endPosition.y, _endPosition.z);
        transform.DOMove(_endPosition, _duration);
    }


    private void Destroy()
    {
        Destroy(gameObject);
    }
}