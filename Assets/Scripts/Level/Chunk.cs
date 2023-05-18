using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;

    private List<SpawnPoint> _points = new List<SpawnPoint>();
    private Platform _platform;

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

    private void Start()
    {
        // StopingPoint point=   GetComponentInChildren<StopingPoint>();
        SpawnPoint[] points = gameObject.GetComponentsInChildren<SpawnPoint>();
        _points = points.ToList();
       // InitPoints();
    }

    //private void InitPoints()
    //{
    //    for (int i = 0; i < _points.Count; i++)
    //    {
    //        _points[i].Init(_platform);
    //    }
    //}
}
