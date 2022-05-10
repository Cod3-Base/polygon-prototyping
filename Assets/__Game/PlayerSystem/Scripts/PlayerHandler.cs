using System;
using System.Collections.Generic;
using Prototype2.SpawnSystem;
using UnityEngine;

namespace Prototype2.PlayerSystem
{
    public class PlayerHandler : MonoBehaviour
    {
        [SerializeField] private GameObject defaultSpawn;

        [SerializeField] private LayerMask deathLayers;

        [SerializeField] private float rayLength;
        
        private Stack<ASpawnPoint> _checkpointHistory;

        private void Awake()
        {
            _checkpointHistory = new Stack<ASpawnPoint>();
        }

        private void Start()
        {
            defaultSpawn.GetComponent<ASpawnPoint>().TeleportToPoint();
        }

        private void FixedUpdate()
        {
            Transform ownTransform;
            Vector3 fwd = (ownTransform = transform).TransformDirection(Vector3.down);

            bool raycast = Physics.Raycast(ownTransform.position, fwd, rayLength, deathLayers);
            
            if (raycast)
            {
                OnDeath();
            }
        }

        public void RegisterSpawnPoint(ASpawnPoint spawnPoint)
        {
            if (_checkpointHistory.Count <= 0)
                _checkpointHistory.Push(spawnPoint);
            
            if (spawnPoint.SpawnNumber > _checkpointHistory.Peek().SpawnNumber)
                _checkpointHistory.Push(spawnPoint);
        }

        private void OnDeath()
        {
            _checkpointHistory.Peek().TeleportToPoint();
        }
    }
}
