using System.Collections.Generic;
using UnityEngine;
using Logic;

[RequireComponent(typeof(Collider2D))]
public class MapCellBehaviour : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Collision2DObserver _collisionObserver;

    private float _frequency = 15f;
    private Collider2D _collider2D;

    private MapCell _mapCell;

    private List<MapCellBehaviour> _neighbours = new List<MapCellBehaviour>();
    private List<SpringJoint2D> _joints = new List<SpringJoint2D>();

    public int Id => _mapCell.Id;
    public Position Position => _mapCell.Position;
    public Rigidbody2D Rigidbody2D => _rigidbody2D;

    public void Construct(MapCell mapCell, bool kinematic = false, float frequency = 15f)
    {
        _mapCell = mapCell;
        _rigidbody2D.isKinematic = kinematic;
        _rigidbody2D.freezeRotation = true;
        _collider2D.isTrigger = false;
        _frequency = frequency;
    }

    public bool HasCell(MapCell cell) => cell == _mapCell;

    public void AddNeighbour(MapCellBehaviour mapCellView)
    {
        var joint = gameObject.AddComponent<SpringJoint2D>();
        joint.connectedBody = mapCellView.Rigidbody2D;
        joint.frequency = _frequency;
        joint.autoConfigureDistance = false;
        joint.distance = 0.666f;
        _neighbours.Add(mapCellView);
        _joints.Add(joint);
    }

    private void TryFree(Collision2D obj)
    {
        if (obj.gameObject.TryGetComponent(out IAttacking attacking))
            if (attacking.Id == Id)
                Free();
    }

    private void Free()
    {
        _collider2D.isTrigger = true;
        SwithOffJoints();
        SwitchOffJointsInNeighbours();
        _rigidbody2D.gravityScale = 1;
    }

    private void SwitchOffJointsInNeighbours()
    {
        foreach (var item in _neighbours)
            foreach (var joint in item._joints)
                if (joint.connectedBody == Rigidbody2D)
                    joint.enabled = false;
    }

    private void SwithOffJoints()
    {
        foreach (var item in _joints)
            item.enabled = false;
    }

    private void OnEnable()
    {
        _collider2D = GetComponent<Collider2D>();
        _collisionObserver.CollisionEnter += TryFree;
    }

    private void OnDisable() => _collisionObserver.CollisionEnter -= TryFree;
}
