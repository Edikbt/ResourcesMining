using System.Collections.Generic;
using UnityEngine;

namespace ResourcesMining
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Source")]
    public class SourceData : ScriptableObject
    {
        public SourceTypeId SourceType;
        public List<ResourceCount> Resources;

        public Source SourcePrefab;
        public float MiningSpeed;
        public float RestoreDelay;
        public int HitsBeforeExhaustion;
    }
}