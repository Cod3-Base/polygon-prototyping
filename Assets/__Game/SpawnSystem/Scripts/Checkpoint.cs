using System.Collections.Generic;
using UnityEngine;

namespace Prototype2.SpawnSystem
{
    public class Checkpoint : ASpawnPoint
    {
        [Header("Checkpoint properties")]
        [SerializeField] private Material unactivated;
        [SerializeField] private Material activated;

        [SerializeField] private Material glowActivated;
        [SerializeField] private Material glowUnactivated;

        [SerializeField] private GameObject glow;

        [SerializeField] private List<GameObject> parts;

        public void Awake()
        {
            foreach (GameObject part in parts)
            {
                part.gameObject.GetComponent<MeshRenderer>().material = unactivated;    
            }
            
            glow.GetComponent<MeshRenderer>().material = glowUnactivated;
        }

        public void OnTriggerEnter(Collider other)
        {
            if(Registered)
                return;

            if (other.gameObject.layer != player.layer)
                return;
            
            RegisterPoint();

            foreach (GameObject part in parts)
            {
                part.gameObject.GetComponent<MeshRenderer>().material = activated;    
            }
            
            glow.GetComponent<MeshRenderer>().material = glowActivated;
        }

        public override void DeactivatePoint()
        {
            transform.parent.GetComponent<MeshRenderer>().material = unactivated;
            glow.GetComponent<MeshRenderer>().material = unactivated;
            
            base.DeactivatePoint();
        }
    }
}
