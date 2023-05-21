using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Victim _victimPrefab;
    [SerializeField] private BoxCollider _evacuationPlatform;
    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private Platform _platform;
    [SerializeField] private Coins _coins;

    private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();

    private Vector3 SelectPoint(BoxCollider platform)
    {
        Vector3 randomPosition;
        float xPoint;
        float zPoint;

        float side = Random.Range(0, 4);
        float offset = 1.0f;
        float minRange = platform.transform.position.z - platform.size.z / 2f;
        float maxRange = platform.transform.position.z + platform.size.z / 2f;
        float rangeMultiplier = Random.Range(1, 3);

        switch (side)
        {
            case 0: 
                xPoint = platform.transform.position.x - platform.size.x / 2f - (offset * rangeMultiplier);
                zPoint = Random.Range(minRange, maxRange);
                break;
            case 1: 
                xPoint = platform.transform.position.x + platform.size.x / 2f + (offset * rangeMultiplier);
                zPoint = Random.Range(minRange, maxRange);
                break;
            case 2: 
                xPoint = Random.Range(platform.transform.position.x - platform.size.x / 2f, platform.transform.position.x + platform.size.x / 2f);
                zPoint = platform.transform.position.z - platform.size.z / 2f - (offset * rangeMultiplier);
                break;
            case 3: 
                xPoint = Random.Range(platform.transform.position.x - platform.size.x / 2f, platform.transform.position.x + platform.size.x / 2f);
                zPoint = platform.transform.position.z + platform.size.z / 2f + (offset * rangeMultiplier);
                break;
            default:
                xPoint = platform.transform.position.x;
                zPoint = platform.transform.position.z;
                break;
        }

        randomPosition = new Vector3(xPoint, platform.transform.position.y + 0.7f, zPoint);

        return randomPosition;
    }

    public void InstantiateVictims()
    {

        for (int i = 0; i < _levelGenerator.GetSpawnPointsCount(); i++)
        {
            _spawnPoints.Add(_levelGenerator.GetSpawnPoints(i));
        }

        for (int i = 0; i < _spawnPoints.Count; i++)
        {
            SpawnPoint point = _spawnPoints[i];

            if (point != null)
            {
                for (int j = 0; j < 5; j++)
                {
                    Vector3 movePosition = SelectPoint(_evacuationPlatform);
                    Victim victim = Instantiate(_victimPrefab, point.transform.position, Quaternion.Euler(0, -90, 0)).GetComponent<Victim>();
                    victim.Init(_platform, movePosition, _coins);
                }

            _spawnPoints.Remove(point);
            }
        }
    }
}
