using System;
using Managers;
using UnityEngine;

namespace Entities
{
    public class RoomChangerCollider : MonoBehaviour
    {



        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("ON TRIGGER ROOM");
            if (!other.gameObject.CompareTag("Player")) return;
            Debug.Log("VAMO A CAMBIAR DE LEVEL");
            LevelManager.Instance.RoomChange();
            Destroy(gameObject);
        }

    }
}