using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace ResourcesMining
{
    public class Tutorial : MonoBehaviour
    {
        private const float FingerAnimationDuration = 0.5f;
        private const int FingerAnimationStep = 50;

        [SerializeField] private Image _image;

        private Source _source;
        private Absorber _spotAbsorber;
        private ProgressService _progressService;

        public void Construct(Source source, Absorber spotAbsorber, ProgressService progressService)
        {
            _source = source;
            _spotAbsorber = spotAbsorber;
            _progressService = progressService;

            _progressService.ResourceCountChanged += SourceFinish;            
        }        

        private void Start()
        {
            transform.position = _source.transform.position;
            StartFingerAnimation();
        }

        private void SourceFinish(ResourceTypeId resourceType, int count)
        {
            _progressService.ResourceCountChanged -= SourceFinish;
            _spotAbsorber.Absorbed += SpotFinish;

            transform.position = _spotAbsorber.transform.position;
        }

        private void SpotFinish(ResourceTypeId obj)
        {
            _spotAbsorber.Absorbed -= SpotFinish;
            gameObject.SetActive(false);
        }

        private void StartFingerAnimation()
        {
            Sequence fingerMoveAnimation = DOTween.Sequence();
            fingerMoveAnimation.Append(_image.rectTransform.DOAnchorPosY(_image.rectTransform.anchoredPosition.y + FingerAnimationStep, FingerAnimationDuration));
            fingerMoveAnimation.Append(_image.rectTransform.DOAnchorPosY(_image.rectTransform.anchoredPosition.y, FingerAnimationDuration));
            fingerMoveAnimation.SetLoops(-1);
        }
    }
}