using System.Collections.Generic;
using UnityEngine;
using Logic;
using System;

public class NeighboursFinder : MonoBehaviour
{
    [SerializeField] private Collision2DObserver _collision2DObserver;

    public event Action<IReadOnlyCollection<MapCellBehaviour>> OnFound;

    private void OnEnable() => _collision2DObserver.CollisionEnter += Find;

    private void OnDisable() => _collision2DObserver.CollisionEnter -= Find;

    private void Find(Collision2D obj)
    {
        if (obj.gameObject.TryGetComponent(out MapCellBehaviour mapCellBehaviour))
        {
            float distance = Vector3.Distance(obj.transform.position, transform.position) * 1.1f;
            Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, distance);
            List<MapCellBehaviour> list = new List<MapCellBehaviour>();

            foreach (var collider in collider2Ds)
                if (collider.gameObject.TryGetComponent(out MapCellBehaviour behaviour))
                    list.Add(behaviour);

            OnFound?.Invoke(list);
        }
    }
}
