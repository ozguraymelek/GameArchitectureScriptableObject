using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Nacho.ObjectPools
{
    public class VFXPool : MonoBehaviour
    {
        [SerializeField] private GladiusVFX gladiusVfxPrefab;
        
        [SerializeField] private int defaultCapacity;
        [SerializeField] private int maxPoolSize;
        
        public IObjectPool<GladiusVFX> Pool { get; private set; }

        private void Awake()
        {
            Pool = new ObjectPool<GladiusVFX>(CreateItem, OnTakeFromPool, OnReturnedToPool,
                OnDestroyPoolObject, true, defaultCapacity, maxPoolSize);
        }

        private GladiusVFX CreateItem()
        {
            var instance = Instantiate(gladiusVfxPrefab,transform);
            instance.VfxPool = Pool;

            return instance;
        }

        private void OnTakeFromPool(GladiusVFX obj) => obj.gameObject.SetActive(true);
    
        private void OnReturnedToPool(GladiusVFX obj) => obj.gameObject.SetActive(false);

        private void OnDestroyPoolObject(GladiusVFX obj) => Destroy(obj.gameObject);
    }
}