using Prototype2.PlayerSystem;
using UnityEngine;

namespace Prototype2.SpawnSystem
{
    /// <summary>
    /// The contract for each spawn, or check, point.
    /// </summary>
    public abstract class ASpawnPoint : MonoBehaviour
    {
        [Header("Abstract Properties")]
        [SerializeField] protected GameObject player;
        
        [SerializeField] protected int SpawnDelay { get; set; }

        [SerializeField] private int spawnNumber;
        public int SpawnNumber => spawnNumber;

        protected bool Activated;
        
        protected bool Registered;
        
        public virtual void TeleportToPoint()
        {
            player.GetComponent<CharacterController>().enabled = false;
            
            Vector3 pos = transform.position;

            float y = pos.y + player.transform.lossyScale.y;

            player.transform.position = new Vector3(pos.x, y, pos.z);
            
            player.GetComponent<CharacterController>().enabled = true;
        }

        protected virtual void RegisterPoint()
        {
            player.GetComponent<PlayerHandler>().RegisterSpawnPoint(this);

            Registered = true;
        }

        public virtual void DeactivatePoint()
        {
            Activated = false;
        }
    }
}