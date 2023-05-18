using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    [SerializeField] Animator _gearAnimator;
    [SerializeField] Animator _fireFighterAnimator;
    [SerializeField] Animator _leverAnimator;
    [SerializeField] private Platform _platform;

    public int _normalized;

    private void FixedUpdate()
    {
        _gearAnimator.speed = _platform.Speed / _normalized /  Time.deltaTime;

    }

    private void MovePlatform()
    {
        _fireFighterAnimator.Play(0);
        _leverAnimator.Play(0);
    }

    private void OnEnable()
    {
        _platform.Movement += MovePlatform;
    }

    private void OnDisable()
    {
        _platform.Movement -= MovePlatform;
    }
}
