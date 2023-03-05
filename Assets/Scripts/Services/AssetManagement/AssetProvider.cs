using UnityEngine;

namespace ResourcesMining
{
    public class AssetProvider
    {
        public GameObject Instantiate(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return GameObject.Instantiate(prefab);
        }

        public GameObject Instantiate(string path, Vector3 at)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return GameObject.Instantiate(prefab, at, Quaternion.identity);
        }
    }
}