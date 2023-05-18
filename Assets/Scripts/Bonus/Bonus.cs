using UnityEngine;

public class Bonus : MonoBehaviour
{
    protected Fire FireInScene;
    protected float FallSpeed = 12f;
    protected float Lifetime = 15f;
    protected int BonusLevel = 0;

    public void Init(Fire fire)
    {
        FireInScene = fire;
    }

    protected virtual void Start()
    {
        Destroy(gameObject, Lifetime);
    }

    protected virtual void OnMouseDown()
    {
        FireInScene.UseBonus(BonusLevel);
        Destroy(gameObject);
    }

    protected virtual void Update()
    {
        transform.Translate(Vector3.down * FallSpeed * Time.deltaTime);
    }
}
