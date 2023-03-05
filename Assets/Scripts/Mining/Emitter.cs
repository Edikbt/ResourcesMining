using System.Collections.Generic;
using UnityEngine;

namespace ResourcesMining
{
    public class Emitter : MonoBehaviour
    {
        private GameFactory _gameFactory;

        public void Construct(GameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void Emitt(List<ResourceCount> resources)
        {
            foreach (ResourceCount resource in resources)
                _gameFactory.CreateResourceFrom(resource.ResourceType, transform.position, resource.Count);
        }
    }
}