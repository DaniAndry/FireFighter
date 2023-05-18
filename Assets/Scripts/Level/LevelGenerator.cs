using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Platform _platform;
    [SerializeField] private Chunk[] _chunks;
    [SerializeField] private Chunk _firstChunk;
    [SerializeField] private Spawner _spawner;

    private List<Chunk> _spawnedChunks;
    private List<SpawnPoint> _points;
    private Chunk _currentChunk;
    private Transform _platformPosition;

    public Chunk CurrentChunk => _currentChunk;

    public SpawnPoint GetSpawnPoints(int index)
    {

        return _points.ElementAt(index);
    }

    public int GetSpawnPointsCount()
    {
        return _points.Count;
    }

    private void Start()
    {
        _spawnedChunks = new List<Chunk>();
        _spawnedChunks.Add(_firstChunk);
        _platformPosition = _platform.transform;
        _points = new List<SpawnPoint>();

        SpawnPoint[] stoppingPoints = _firstChunk.GetComponentsInChildren<SpawnPoint>();
        _points.AddRange(stoppingPoints);
        _spawner.InstantiateVictims();
    }

    private void Update()
    {
        if (_platformPosition.position.y > _spawnedChunks[_spawnedChunks.Count - 1].EndPoint.position.y - 200)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        Chunk newChunk = Instantiate(_chunks[Random.Range(0, _chunks.Length)]);
        Chunk lastChunk = _spawnedChunks[_spawnedChunks.Count - 1];
        Vector3 spawnPosition = lastChunk.EndPoint.position + (newChunk.StartPoint.position - newChunk.transform.position);
        newChunk.transform.position = spawnPosition;
        _spawnedChunks.Add(newChunk);

        _currentChunk = newChunk;
        newChunk.Init(_platform);

        SpawnPoint[] stoppingPoints = newChunk.GetComponentsInChildren<SpawnPoint>();
        _points.AddRange(stoppingPoints);
        stoppingPoints = null;

        if (_spawnedChunks.Count >= 5)
        {
            Destroy(_spawnedChunks[0].gameObject);
            _spawnedChunks.RemoveAt(0);
        }

        _spawner.InstantiateVictims();
        _points.Clear();
    }
}