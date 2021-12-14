using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] public Transform[] _wayPoints;

    private void OnDrawGizmos()
    {
        _wayPoints = new Transform[transform.childCount];

        for (int i = 0; i < _wayPoints.Length; i++)
        {
            _wayPoints[i] = transform.GetChild(i);
        }

        Gizmos.color = Color.green;

        for (int i = 0; i < _wayPoints.Length - 1; i++)
        {
            Gizmos.DrawLine(_wayPoints[i].position, _wayPoints[i + 1].position);
        }
    }
}
