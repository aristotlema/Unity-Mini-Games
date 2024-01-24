using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] private Transform _pointPrefab;

    [SerializeField,Range(10, 100)] private int resolution = 10;

    Transform[] points;

    private void Awake()
    {
        float step = 2f / resolution;
        var scale = Vector3.one * step;
        Vector3 position = Vector3.zero;

        points = new Transform[resolution];

        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i] = Instantiate(_pointPrefab);
            position.x = (i + 0.5f) * step - 1f;
            point.localPosition = position;
            point.localScale = scale;
            point.SetParent(transform, false);
        }
    }

    private void Update()
    {
        float _time = Time.time;

        for (int i = 0;i < points.Length;i++)
        {
            Transform point = points[i];
            Vector3 position = point.localPosition;
            position.y = GraphY(position, _time);
            point.localPosition = position;
        }
    }

    private static float GraphY(Vector3 position, float time)
    {
        position.y = Mathf.Sin(Mathf.PI * (position.x + time));
        return position.y;
    }
}
