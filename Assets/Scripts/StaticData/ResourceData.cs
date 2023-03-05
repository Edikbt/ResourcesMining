using UnityEngine;

namespace ResourcesMining
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Resource")]
    public class ResourceData : ScriptableObject
    {
        public ResourceTypeId ResourceType;
        public GameResource ResourcePrefab;

        public float Impulse;
        public float PickUpRadius;
        public float PickUpDelay;
    }
}