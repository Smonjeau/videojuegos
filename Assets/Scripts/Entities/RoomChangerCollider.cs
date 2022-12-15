using System;
using Managers;
using UnityEngine;

namespace Entities
{
    public class RoomChangerCollider : MonoBehaviour
    {



        private void OnTriggerEnter(Collider other)
        {
            LevelManager.Instance.RoomChange();
            Destroy(gameObject);
        }

    }
}