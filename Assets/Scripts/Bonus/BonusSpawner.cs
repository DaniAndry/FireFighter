using System.Collections;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] private Bonus[] _objectCollection;
    [SerializeField] private Fire _fire;

    private Vector3 spawnAreaSize = new Vector3(1f, 0f, 1f);
    private float minSpawnDelay = 10f;
    private float maxSpawnDelay = 30f;
    private Bonus bonus;

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        float minValue = - 0.7f;
        float maxValue = 0.7f;

        while (true)
        {
            float spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(spawnDelay);
            float randomPossition = Random.Range(minValue, maxValue);
            Vector3 spawnPosition = new Vector3(transform.position.x * randomPossition, transform.position.y, transform.position.z);

            Bonus randomObject = _objectCollection[Random.Range(0, _objectCollection.Length)];

            bonus = Instantiate(randomObject, spawnPosition, Quaternion.identity);
            bonus.Init(_fire);
        }
    }
}
