using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Shooter
{
    public class BallAiming : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IAiming
    {
        [SerializeField] private Rigidbody2D _target;
        [SerializeField] private float _distanceToStrength = 1f;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Projection _projection;

        private Vector3[] Trajectory => _projection.GetTrajectory();
        public event Action<AttackData> OnAimingEnd;

        public void Construct(Rigidbody2D target, LineRenderer lineRenderer = default)
        {
            _target = target;
            if (lineRenderer != default)
                _lineRenderer = lineRenderer;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            DrawLineFromMouseToBall(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            DrawLineFromMouseToBall(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            var distance = Vector2.Distance(_target.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            AttackData attackData = new AttackData(distance * _distanceToStrength, Trajectory);
            OnAimingEnd?.Invoke(attackData);
            _lineRenderer.positionCount = 0;
            _projection.HideTrajectory();
        }

        private void DrawLineFromMouseToBall(PointerEventData eventData)
        {
            Vector3 mousePosition = eventData.pointerCurrentRaycast.worldPosition;
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPositions(new Vector3[] { _target.position, mousePosition });
            _projection.ShowTrajectory(_target.position, Trajectory);
        }
    }
}
