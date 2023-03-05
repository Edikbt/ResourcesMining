using System.Collections;
using UnityEngine;

namespace ResourcesMining
{
    public class PickupResource : MonoBehaviour
    {
        private const int SaveProgressDelay = 5;
        [SerializeReference] private TriggerObserver _triggerObserver;
        private ProgressService _progressService;
        private SaveLoadService _saveLoadService;

        private bool _isSaveProgressInitialized = false;

        public void Construct(ProgressService progressService, SaveLoadService saveLoadService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
        }        

        private void OnDestroy()
        {
            _triggerObserver.TriggerEnter -= TriggerEnter;
        }

        private void TriggerEnter(Collider collider)
        {
            GameResource gameResource = collider.GetComponentInParent<GameResource>();

            if (gameResource != null)
            {
                _progressService.ChangeResource(gameResource.ResourceType, 1);
                Destroy(gameResource.gameObject);

                if (_isSaveProgressInitialized == false)
                    StartCoroutine(InitSaveProgress());
            }            
        }

        private IEnumerator InitSaveProgress()
        {
            _isSaveProgressInitialized = true;

            yield return new WaitForSeconds(SaveProgressDelay);

            _saveLoadService.SaveProgress();
            _isSaveProgressInitialized = false;
        }

    }
}