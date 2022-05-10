using System;
using System.Collections;
using __Common.Extensions;
using UnityEngine;

namespace Prototype2.Platform_System
{
    [RequireComponent(typeof (BoxCollider))]
    public class FallingPlatform : MonoBehaviour
    {
        [Header("Used Objects")]
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject destination;
        
        [Header("Config")]
        [SerializeField] private float fallDuration;
        
        [SerializeField] private float timeBeforeFall;
        [SerializeField] private float timeBeforeReset;
        
        private float _currentTimer;
        private float _currentResetTimer;

        private Vector3 _origin;

        private Coroutine _currentCor;

        private bool _measuringFallTime;
        private bool _measuringResetTime;

        private void Awake()
        {
            _origin = transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == player.layer)
            {
                if(_measuringFallTime || _measuringResetTime)
                    return;
                
                Debug.Log("Measuring Fall time!");
                _measuringFallTime = true;
            }
        }

        private void Update()
        {
            if (_measuringFallTime)
            {
                _currentTimer += Time.deltaTime;

                if (_currentTimer >= timeBeforeFall)
                {
                    // Debug.Log("Falling!");

                    if (_currentCor == null)
                    {
                      _currentCor = StartCoroutine(MoveToDestinationCor());
                      gameObject.DisableObjectTemporarily(timeBeforeReset);
                    }
                }
            }
        }

        private IEnumerator MoveToDestinationCor()
        {
            Debug.Log("Coroutine started!");
            
            float t = 0;

            while (t < 1)
            {
                t += Time.deltaTime / fallDuration;

                transform.parent.transform.position = Vector3.Lerp(_origin, destination.transform.position, t);
                yield return new WaitForEndOfFrame();
            }

            _measuringFallTime = false;
            _measuringResetTime = true;

            _currentCor = null;
            
            yield return null;
        }
    }
}
