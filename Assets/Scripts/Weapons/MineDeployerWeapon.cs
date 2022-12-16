using System.Collections;
using Controllers;
using Entities;
using Strategy;
using UnityEngine;

namespace Weapons
{
    [RequireComponent(typeof(SoundEffectController))]
    public class MineDeployerWeapon: MonoBehaviour,IDeployable
    {
        [SerializeField] private GameObject _minePrefab;  
        private GameObject _character;
        private Transform _deployLocationTransform;
        private Animator _armAnimator;
        private SoundEffectController _soundEffectController;
        private static readonly int IsDeploy = Animator.StringToHash("isDeploy");
        
        public void Start()
        {
            
            Debug.Log("MDW start start");
            _soundEffectController = GetComponent<SoundEffectController>();
            _armAnimator = transform.parent.parent.GetComponent<Animator>();
            _character = transform.parent.parent.parent.gameObject;
            _deployLocationTransform = _character.transform.GetChild(_character.transform.childCount - 1);
            Debug.Log("MDW start end");
            
            
        }
        
        public void Deploy()
        {
           
            // Debug.Log("Starting coroutine");
            // StartCoroutine(DeployMine());
            Instantiate(_minePrefab, _deployLocationTransform.position, _deployLocationTransform.rotation);
            // Debug.Log("ended instantiation");
            // Debug.Log("Starting callback");
            
            _character.GetComponent<Character>().DeployableReset();
        }
        
        private IEnumerator DeployMine()
        {
            Debug.Log("Starting coroutine inside");
            _armAnimator.SetBool(IsDeploy,true);
            _soundEffectController.PlayOnReloadStart();
            Debug.Log("Starting yield coroutine");
            yield return new WaitForSeconds((float)0.5);
            Debug.Log("recovered yield coroutine");
            _armAnimator.SetBool(IsDeploy,false);
            Debug.Log("Starting instantiation");
            Instantiate(_minePrefab, _deployLocationTransform.position, _deployLocationTransform.rotation);
            Debug.Log("ended instantiation");
            Debug.Log("Starting callback");
            
            _character.GetComponent<Character>().DeployableReset();
           
            
        }
        
        
        

    }
}