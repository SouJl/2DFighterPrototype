using System.Collections;
using UnityEngine;

namespace FighterGame
{
    [RequireComponent(typeof(SpriteRenderer))]
    internal class AnimationComponentView : MonoBehaviour
    {
        [SerializeField] private Sprite[] _idleSprites;
        [SerializeField] private float _anomationSpeed = 0.2f;

        private SpriteRenderer _spriteRenderer;
        
        private int _currentPointIndex = 0;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            StartCoroutine(Animation());
        }

        IEnumerator Animation()
        {
            var waitForSeconds = new WaitForSeconds(_anomationSpeed);
            
            while (true)
            {
                _spriteRenderer.sprite = GetCurrentSprite();
                yield return waitForSeconds;
            }     
        }

        private Sprite GetCurrentSprite()
        {
            if (_idleSprites == null) return null;
            _currentPointIndex = (_currentPointIndex + 1) % _idleSprites.Length;
            return _idleSprites[_currentPointIndex];
        }

        private void OnDestroy()
        {
            StopCoroutine(Animation());
        }
    }
}
