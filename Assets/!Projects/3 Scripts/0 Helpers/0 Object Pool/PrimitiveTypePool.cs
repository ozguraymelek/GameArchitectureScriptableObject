using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Nacho.ObjectPools
{
    public class PrimitiveTypePool : MonoBehaviour
    {
        [SerializeField] private Waypoint sphere;

        private Waypoint _createdWaypoint;
        
        [SerializeField] private int defaultCapacity;
        [SerializeField] private int maxPoolSize;

        private void Awake()
        {
            CreatePoint();
        }

        private void CreatePoint()
        {
            _createdWaypoint = Instantiate(sphere, transform);
            _createdWaypoint.gameObject.SetActive(false);
        }

        public Waypoint GetPoint()
        {
            return _createdWaypoint;
        }

        public void ActivatePoint()
        {
            _createdWaypoint.gameObject.SetActive(true);
        }

        public void DeactivatePoint()
        {
            _createdWaypoint.gameObject.SetActive(false);
        }
    }
}

