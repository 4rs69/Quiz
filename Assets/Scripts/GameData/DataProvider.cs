using System.Collections.Generic;
using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "DataProvider", menuName = "DataProvider")]
    public class DataProvider : ScriptableObject
    {
        public IEnumerable<GameSetData> GameSetData => _gameSetsData;
        
        [SerializeField]
        private GameSetData[] _gameSetsData;
        
        [SerializeField]
        private LevelData[] _levels;

        public int LevelsCount()
        {
            return _levels.Length;
        }

        public LevelData GetLevel(int index)
        {
            return _levels[index];
        }
    }
}