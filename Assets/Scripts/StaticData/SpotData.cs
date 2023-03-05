using System.Collections.Generic;
using UnityEngine;

namespace ResourcesMining
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Spot")]
    public class SpotData : ScriptableObject
    {
        public SpotTypeId SpotType;
        public List<ResourceCount> RequiredResources;
        public List<ResourceCount> CreatedResources;

        public Spot SpotPrefab;
        public float Spread;
        public float Delay;
    }
}