using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

namespace ResourcesMining
{
    public class Source : MonoBehaviour
    {
        private const float HitAnimateDuration = 0.5f;
        private const float HitAnimateHitScale = 0.75f;
        private const float HitAnimateNormalScale = 1f;

        [SerializeField] private Emitter _emitter;
        [SerializeField] private Renderer _renderer;

        public float MiningSpeed { get; private set; }
        public bool IsAvailable { get; private set; }

        private float _restoreDelay;
        private int _hitsBeforeExhaustion;
        private List<ResourceCount> _resources;

        private Color _rendererColor;
        private int _currentHits = 0;

        public void Init(SourceData sourceData)
        {
            MiningSpeed = sourceData.MiningSpeed;
            _restoreDelay = sourceData.RestoreDelay;
            _hitsBeforeExhaustion = sourceData.HitsBeforeExhaustion;
            _resources = sourceData.Resources;

            _rendererColor = _renderer.material.color;
            IsAvailable = true;
        }

        public bool Hit()
        {
            _currentHits++;
            HitAnimation();
            _emitter.Emitt(_resources);

            if (_currentHits >= _hitsBeforeExhaustion)
            {
                IsAvailable = false;
                _renderer.material.color = Color.gray;
                StartCoroutine(Restore());
            }

            return _currentHits >= _hitsBeforeExhaustion;
        }

        private void HitAnimation()
        {
            Sequence hitTweenSequence = DOTween.Sequence();
            hitTweenSequence.Append(transform.DOScaleY(HitAnimateHitScale, HitAnimateDuration));
            hitTweenSequence.Append(transform.DOScaleY(HitAnimateNormalScale, HitAnimateDuration));
        }

        private IEnumerator Restore()
        {
            yield return new WaitForSeconds(_restoreDelay);
            _renderer.material.color = _rendererColor;
            _currentHits = 0;
            IsAvailable = true;
        }

    }
}