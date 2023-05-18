using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    [SerializeField] private Transform platformTransform;
    [SerializeField] private Transform fireTransform;
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient fillGradient;

    private float maxDistance =200f;
    private float minDistance = 10f;

    private void Start()
    {
        slider.maxValue = 1f;
    }

    private void Update()
    {
        float distance = Vector3.Distance(platformTransform.position, fireTransform.position);
        float normalizedDistance = Mathf.Clamp01((maxDistance - distance) / (maxDistance - minDistance));
        slider.value = normalizedDistance;
        Color fillColor = fillGradient.Evaluate(normalizedDistance);
        slider.fillRect.GetComponentInChildren<Image>().color = fillColor;
    }
}
