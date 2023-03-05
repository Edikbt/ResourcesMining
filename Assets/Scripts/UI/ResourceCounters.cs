using UnityEngine;
using TMPro;
using System.Collections.Generic;
using DG.Tweening;

namespace ResourcesMining
{
    public class ResourceCounters : MonoBehaviour
    {
        private const float IncreaseAnimationDuration = 0.5f;
        private const float IncreaseAnimationIncreasedScale = 1.5f;
        private const float IncreaseAnimationNormalScale = 1f;

        [SerializeField] private TextMeshProUGUI _crystalCounter;
        [SerializeField] private TextMeshProUGUI _metalCounter;
        [SerializeField] private TextMeshProUGUI _woodCounter;

        private Dictionary<ResourceTypeId, TextMeshProUGUI> _counters;
        private ProgressService _progressService;

        public void Construct(ProgressService progressService)
        {
            _progressService = progressService;
            _progressService.ResourceCountChanged += UpdateResourceCount;
        }

        private void Start()
        {
            _counters = new Dictionary<ResourceTypeId, TextMeshProUGUI>
            {
                {ResourceTypeId.Crystal, _crystalCounter },
                {ResourceTypeId.Metall, _metalCounter },
                {ResourceTypeId.Wood, _woodCounter },
            };

            foreach (var counter in _counters.Keys)
                UpdateResourceCount(counter, 1);
        }        

        private void UpdateResourceCount(ResourceTypeId resourceType, int value)
        {
            int count = _progressService.PlayerProgress.GetResource(resourceType).Count;

            if (count <= 0)
                _counters[resourceType].transform.parent.gameObject.SetActive(false);
            else
                _counters[resourceType].transform.parent.gameObject.SetActive(true);

            _counters[resourceType].text = $"{count}";

            if (value > 0)
            {
                Sequence changeCountSequence = DOTween.Sequence();
                changeCountSequence.Append(_counters[resourceType].transform.DOScale(IncreaseAnimationIncreasedScale, IncreaseAnimationDuration));
                changeCountSequence.Append(_counters[resourceType].transform.DOScale(IncreaseAnimationNormalScale, IncreaseAnimationDuration));
            }
        }
    }
}