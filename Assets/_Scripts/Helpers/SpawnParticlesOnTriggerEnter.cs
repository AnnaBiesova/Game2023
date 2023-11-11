using _Scripts.MonoBehaviours.Patterns.EasyObjectPool;
using _Scripts.MonoBehaviours.Patterns.EasyObjectPool.Core;
using _Scripts.Patterns;
using UnityEngine;

public class SpawnParticlesOnTriggerEnter : MonoBehaviour
{
    [SerializeField] private string fxPoolName;
    
    private void OnTriggerEnter(Collider other)
    {
        EasyObjectPool.Instance.GetObjectFromPool<PooledObjectWithLifetime>(fxPoolName, other.ClosestPointOnBounds(transform.position), Quaternion.identity);
    }
    
}