using System.Collections.Generic;
using UnityEngine;

public class Projection : MonoBehaviour
{
    private const string Geometry = "Geometry";
    private const string Animals = "Animals";

    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private int _maxReflectionsCount = 5;

    private Vector3 _origin;
    private Vector3 _direction;

    private bool _showed = true;

    private void Start() => _lineRenderer.positionCount = _maxReflectionsCount + 1;

    public void ShowTrajectory(Vector3 origin, Vector3[] trajectory = null)
    {
        if (_showed == false)
            return;
        _showed = false;
        _direction = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _origin = origin;
        var points = trajectory ?? GetTrajectory();
        int index = 0;
        _lineRenderer.positionCount = points.Length;
        foreach (var item in points)
        {
            _lineRenderer.SetPosition(index, item);
            index++;
        }
        _showed = true;
    }

    public Vector3[] GetTrajectory()
    {
        List<Vector3> trajectory = new List<Vector3>(_maxReflectionsCount);
        trajectory.Add(_origin);

        int currentReflectionsCount = 0;

        RaycastHit2D[] geometryHits2D;
        RaycastHit2D ballsHit;
        RaycastHit2D current;

        while (currentReflectionsCount != _maxReflectionsCount)
        {
            ballsHit = Physics2D.Raycast(_origin, _direction, 100, LayerMask.GetMask(Animals));

            if (ballsHit.collider != null)
            {
                trajectory.Add(ballsHit.point);
                return trajectory.ToArray();
            }

            geometryHits2D = Physics2D.RaycastAll(_origin, _direction, 100, LayerMask.GetMask(Geometry));

            if (geometryHits2D.Length == 0)
                return trajectory.ToArray();

            bool firstHitInOrigin = geometryHits2D[0].point == new Vector2(_origin.x, _origin.y);

            if (firstHitInOrigin && geometryHits2D.Length > 1)
                current = geometryHits2D[1];
            else
                current = geometryHits2D[0];

            currentReflectionsCount++;
            trajectory.Add(current.point);
            _origin = current.point;
            _direction = Vector2.Reflect(_direction, current.normal);
        }

        return trajectory.ToArray();
    }

    public void HideTrajectory()
    {
        for (int i = _lineRenderer.positionCount - 1; i > 0; i--)
            for (int j = i; j < _lineRenderer.positionCount; j++)
                _lineRenderer.SetPosition(j, _lineRenderer.GetPosition(i - 1));
    }
}
