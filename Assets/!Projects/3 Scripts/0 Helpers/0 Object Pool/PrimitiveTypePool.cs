using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Nacho.ObjectPools
{
    public class PrimitiveTypePool : MonoBehaviour
    {
        [SerializeField] private Waypoint sphere;
        
        [SerializeField] private int defaultCapacity;
        [SerializeField] private int maxPoolSize;
        
        public IObjectPool<Waypoint> Pool { get; private set; }

        private void Awake()
        {
            Pool = new ObjectPool<Waypoint>(CreateItem, OnTakeFromPool, OnReturnedToPool,
                OnDestroyPoolObject, true, defaultCapacity, maxPoolSize);
        }

        private Waypoint CreateItem()
        {
            var instance = Instantiate(sphere, transform);
            instance.WaypointPool = Pool;

            return instance;
        }

        private void OnTakeFromPool(Waypoint obj) => obj.gameObject.SetActive(true);
    
        private void OnReturnedToPool(Waypoint obj) => obj.gameObject.SetActive(false);

        private void OnDestroyPoolObject(Waypoint obj) => Destroy(obj.gameObject);
    }
}

