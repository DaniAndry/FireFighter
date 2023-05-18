using DG.Tweening;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float yOffSet;

    private Vector3 _targetLastPossition;
    private Vector3 _targetPossition;
    private Tweener _tween;

    private void Start()
    {
        _targetPossition = new Vector3(transform.position.x, _target.position.y - yOffSet, transform.position.z);
    }

    private void Update()
    {
        _targetPossition = new Vector3(transform.position.x, _target.position.y - yOffSet, transform.position.z);

        if (_targetLastPossition != _targetPossition)
        {
            _tween = transform.DOMove(_targetPossition, 2).SetAutoKill(false);
        }
    }
}
