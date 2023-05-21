using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;

    private List<SpawnPoint> _points = new List<SpawnPoint>();
    private Platform _platform;

    public Transform StartPoint => _startPoint;
    public Transform EndPoint => _endPoint;

    private void Start()
    {
        SpawnPoint[] points = gameObject.GetComponentsInChildren<SpawnPoint>();
        _points = points.ToList();
    }

    public SpawnPoint GetPointByIndex(int index)
    {
        return _points.ElementAt(index);
    }

    public int GetPointsCount()
    {
        return _points.Count;
    }

    public void Init(Platform platform)
    {
        _platform = platform;
    }

}
