using System;
using System.Collections;
using UnityEngine;

namespace Prototype2.BridgeSystem
{
    public class BridgeController : MonoBehaviour
    {
        // Y to go to = 0.2694
        [Header("Selections")] 
        [SerializeField] private GameObject bridgeController;
        [SerializeField] private GameObject bridgeWalkable;
        [SerializeField] private Material selectedMaterial;
        [SerializeField] private Material activatedMaterial;
        [SerializeField] private Material deactivatedMaterial;

        [Header("Configuration")]
        [SerializeField] private float bridgeLoweringTime;
        
        
        private bool _bridgeDown;

        private Coroutine _currentBridgeLoweringRoutine;

        private void Awake()
        {
            _bridgeDown = false;
            _currentBridgeLoweringRoutine = null;
            
            bridgeController.GetComponent<Renderer>().material = deactivatedMaterial;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player"))
                return;

            bridgeController.GetComponent<Renderer>().material = selectedMaterial;

            if (_bridgeDown)
                return;

            _currentBridgeLoweringRoutine ??= StartCoroutine(LowerBridgeCor());
                
            _bridgeDown = true;
        }

        private IEnumerator LowerBridgeCor()
        {
            bridgeWalkable.SetActive(true);

            Vector3 bridgeStartPos = bridgeWalkable.transform.position;
            Vector3 newBridgePos = new Vector3(bridgeStartPos.x, 0.2694f, bridgeStartPos.z);

            float t = 0;
            
            while (t < 1)
            {
                t += Time.deltaTime / bridgeLoweringTime;
                
                // Execute lerp
                bridgeWalkable.transform.position = Vector3.Lerp(bridgeStartPos, newBridgePos, t);

                yield return new WaitForEndOfFrame();
            }

            _currentBridgeLoweringRoutine = null;
            
            bridgeController.GetComponent<Renderer>().material = activatedMaterial;

            yield return null;
        }
    }
}
