using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Nacho.ObjectPools
{
    public class PointCreator : MonoBehaviour
    {
        [SerializeField] private Point sphere;

        private Point _createdPoint;

        private void Awake()
        {
            CreatePoint();
        }

        private void CreatePoint()
        {
            _createdPoint = Instantiate(sphere, transform);
            _createdPoint.gameObject.SetActive(false);
        }

        public Point GetPoint()
        {
            return _createdPoint;
        }

        public void ActivatePoint()
        {
            _createdPoint.gameObject.SetActive(true);
        }

        public void DeactivatePoint()
        {
            _createdPoint.gameObject.SetActive(false);
        }
    }
}

