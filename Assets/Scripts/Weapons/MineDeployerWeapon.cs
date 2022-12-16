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
        private static readonly int IsDeploy = Animator.StringToHash("IsDeploying");
        private bool first;
        
        public void Start()
        {
            first = true;
            _soundEffectController = GetComponent<SoundEffectController>();
            _armAnimator = transform.parent.parent.GetComponent<Animator>();
            _character = transform.parent.parent.parent.gameObject;
            _deployLocationTransform = _character.transform.GetChild(_character.transform.childCount - 1);


        }
        
        public void Deploy()
        {
            if (!first)return;
            first = false;
            
            StartCoroutine(DeployMine());
           
        }
        
        private IEnumerator DeployMine()
        {
            
            _armAnimator.SetBool(IsDeploy,true);
            
            
            yield return new WaitForSeconds((float)1);
           
            _armAnimator.SetBool(IsDeploy,false);
            
            _soundEffectController.PlayOnShot();
            Instantiate(_minePrefab, _deployLocationTransform.position, _deployLocationTransform.rotation);

            _character.GetComponent<Character>().DeployableReset();
            first = true;

        }
        
        
        

    }
}