using Configs.Map;
using UnityEngine;

namespace Configs.Game
{
    [CreateAssetMenu(fileName = "Config", menuName = "ScriptableObjects/GameConfig", order = 1)]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private MapConfig _mapConfig;

        public MapConfig MapConfig => _mapConfig;
    }
}
