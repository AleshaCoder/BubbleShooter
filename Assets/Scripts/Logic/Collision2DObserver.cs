using System;
using UnityEngine;

namespace Logic
{
    [RequireComponent(typeof(Collider2D))]
    public class Collision2DObserver : MonoBehaviour
    {
        public event Action<Collision2D> CollisionEnter;
        public event Action<Collision2D> CollisionExit;

        private void OnCollisionEnter2D(Collision2D collision)
            => CollisionEnter?.Invoke(collision);

        private void OnCollisionExit2D(Collision2D collision)
            => CollisionExit?.Invoke(collision);
    }
}
