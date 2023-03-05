using System.Collections.Generic;
using UnityEngine;

namespace ResourcesMining
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Level")]
    public class LevelData : ScriptableObject
    {
        public string LevelName;
        public List<SourcePosition> Sources;
        public List<SpotPosition> Spots;        
    }
}