using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    [SerializeField] private Animator _gearAnimator;
    [SerializeField] private Animator _fireFighterAnimator;
    [SerializeField] private Animator _leverAnimator;
    [SerializeField] private Platform _platform;

   public float _normalized = 0.05f;

    private void FixedUpdate()
    {
        _gearAnimator.speed = _platform.Speed / _normalized / Time.deltaTime;
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
