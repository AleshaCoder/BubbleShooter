using System.Collections.Generic;
using Services.MapGenerator;
using UnityEngine;

namespace Configs.Map
{
    [CreateAssetMenu(fileName = "Config", menuName = "ScriptableObjects/MapConfig", order = 1)]
    public class MapConfig : ScriptableObject
    {
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private Vector3 _secondStartPosition;
        [SerializeField] private float _stepX;
        [SerializeField] private float _stepY;
        [SerializeField] private float _frequency;
        [SerializeField] private List<MapCellBehaviour> _cells;

        public Vector3 StartPosition => _startPosition;
        public Vector3 SecondStartPosition => _secondStartPosition;

        public float StepX => _stepX;
        public float StepY => _stepY;

        public float Frequency => _frequency;

        public MapCellBehaviour Get(int id) => _cells[id];
    }
}
