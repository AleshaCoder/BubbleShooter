using UnityEngine;
using Logic;

namespace Shooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class AttackingBall : MonoBehaviour, IAttacking
    {
        [SerializeField] private int _id;
        [SerializeField] private Collision2DObserver _collision2DObserver;

        private Rigidbody2D _rigidbody2D;

        public int Id => _id;

        private void StopAttack(Collision2D obj) => _rigidbody2D.Sleep();

        private void OnEnable()
        {
            _collision2DObserver.CollisionEnter += StopAttack;
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnDisable() => _collision2DObserver.CollisionEnter -= StopAttack;
    }
}
