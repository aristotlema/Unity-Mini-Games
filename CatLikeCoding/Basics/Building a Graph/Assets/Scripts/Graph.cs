using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] private Transform _pointPrefab;

    private void Awake()
    {
        Vector3 position = Vector3.zero;
        var scale = Vector3.one / 5f;

        for (int i = 0; i < 10; i++)
        {
            Transform point = Instantiate(_pointPrefab);
            position.x = (i + 0.5f) / 5f - 1f;
            position.y = position.x * position.x;
            point.localPosition = position;
            point.localScale = scale;
        }
    }
}
