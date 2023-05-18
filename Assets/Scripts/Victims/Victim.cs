using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Victim : MonoBehaviour
{
    private float _duration;
    private Platform _platform;
    private Vector3 _endPosition;
    private Animator _animator;
    private bool _isOnPlatform;
    private float _distanceThreshold = 2f;
    private int _coinPrice = 100;
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
    }

    private void Update()
    {
        float distance = Mathf.Abs(transform.position.y - _platform.transform.position.y);
        bool isOnSameLevel = distance <= _distanceThreshold;

        if (_platform.IsStoped && isOnSameLevel || _isOnPlatform)
        {
            Move();
        }

        if (_platform.IsReadyEvacuation && _isOnPlatform)
        {
            Evacuation();
        }
        else
        {
            _endPosition.y = _platform.transform.position.y + 1.1f;
        }
    }

    private void Move()
    {

        if ((Mathf.Approximately(transform.position.y, _platform.transform.position.y + 1.1f)) || _isOnPlatform)
        {
            _animator.SetBool("isStop", false);

            transform.DOMove(_endPosition, _duration);

            if (Vector3.Distance(transform.position, _endPosition) < 0.8f)
            {
                Debug.Log("Stop");
                _animator.SetBool("isStop", true);
            }
        }

        transform.DOLocalMoveY(_platform.transform.position.y + 1.1f, 0);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Platform platform))
        {
            _isOnPlatform = true;
            platform.VictimCount += 1;
        }
        if (collision.TryGetComponent(out Fire fire))
        {
            _coins.Count -= _coinPrice;
            Destroy(gameObject);
        }
        if (collision.TryGetComponent(out EvacuationPlatform evacuationPlatform))
        {
            _coins.Count += _coinPrice;
            Invoke("Destroy", 1f);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent(out Platform platform))
        {
            _isOnPlatform = true;
            platform.VictimCount -= 1;
        }
    }

    private void Evacuation()
    {
        _animator.SetBool("isStop", false);
        _endPosition = new Vector3(transform.position.x - 10f, _endPosition.y, _endPosition.z);
        transform.DOMove(_endPosition, _duration);
    }


    private void Destroy()
    {
        Destroy(gameObject);
    }
}