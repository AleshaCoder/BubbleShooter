using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallNeighboursInstaller : MonoBehaviour
{
    [SerializeField] private NeighboursFinder _neighboursFinder;
    [SerializeField] private MapCellBehaviour _mapCellBehaviour;

    private void OnEnable() => _neighboursFinder.OnFound += Install;

    private void OnDisable() => _neighboursFinder.OnFound -= Install;

    private void Install(IReadOnlyCollection<MapCellBehaviour> obj)
    {
        foreach (var cell in obj)
            _mapCellBehaviour.AddNeighbour(cell);
    }
}
