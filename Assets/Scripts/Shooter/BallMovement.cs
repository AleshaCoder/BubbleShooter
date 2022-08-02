using Logic;
using UnityEngine;
using DG.Tweening;

namespace Shooter
{
    public class BallMovement : MonoBehaviour
    {
        [SerializeField] private BallAiming _ballAiming;
        [SerializeField] private Collision2DObserver _collision2DObserver;

        private Tweener _tween;

        private void OnEnable()
        {
            _ballAiming.OnAimingEnd += StartMovement;
            _collision2DObserver.CollisionEnter += StopMovement;
        }

        private void OnDisable()
        {
            _ballAiming.OnAimingEnd -= StartMovement;
            _collision2DObserver.CollisionEnter -= StopMovement;
        }

        private void StartMovement(AttackData attackData)
        {
            _tween = transform.DOPath(attackData.Trajection, 10f, PathType.Linear).SetSpeedBased().Play();
        }

        private void StopMovement(Collision2D obj)
        {
        }
    }
}
