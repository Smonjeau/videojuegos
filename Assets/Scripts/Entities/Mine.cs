using System;
using System.Collections;
using Controllers;
using Flyweight;
using Strategy;
using UnityEngine;

namespace Entities
{
    public class Mine : MonoBehaviour
    {
        
        [SerializeField] private GameObject _eplosionPrefab;
        [SerializeField] private DeployableStats _stat;
        // [SerializeField] private GameObject _minePrefab;  
        private GameObject _character;
        // private Transform _deployLocationTransform;
        // private Animator _armAnimator;
        // private SoundEffectController _soundEffectController;
        // private static readonly int Reloading = Animator.StringToHash("reloading");
        //
        private float _radius => _stat.Range;
        private float _force = 800f;
        private int _damage => _stat.Damage;

        
        public void Start()
        {
            
            // _soundEffectController = GetComponent<SoundEffectController>();
            // _armAnimator = transform.parent.parent.GetComponent<Animator>();
            // _character = transform.parent.parent.parent.gameObject;
            // _deployLocationTransform = _character.transform.GetChild(_character.transform.childCount - 1);
            
        }
        private void OnTriggerEnter(Collider other)
        {
            
            if (other.gameObject.layer.ToString() != "8")
            {
                Debug.LogError("Exploded");
                Debug.LogError(other.gameObject.layer.ToString());
                Explode();
            }
           
        }

        private void Explode()
        {
            Instantiate(_eplosionPrefab, transform.position, transform.rotation);
            Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

            foreach (Collider collider in colliders)
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(_force, transform.position, _radius);
                }

                IDamageable damageable = collider.GetComponent<IDamageable>();
                if (damageable != null && collider.gameObject.layer != 8)
                {
                    damageable.TakeDamage(_damage);
                }
            }
            Destroy(gameObject);
        }

       
        // public void Deploy()
        // {
        //     
        //     StartCoroutine(DeployMine());
        //     
        // }
        //
        // private IEnumerator DeployMine()
        // {
        //     _armAnimator.SetBool(Reloading,true);
        //     _soundEffectController.PlayOnReloadStart();
        //     yield return new WaitForSeconds((float)0.5);
        //     _armAnimator.SetBool(Reloading,false);
        //     Instantiate(_minePrefab, _deployLocationTransform.position, _deployLocationTransform.rotation);
        //     _character.GetComponent<Character>().DeployableReset();
        //    
        //     
        // }
    }

}