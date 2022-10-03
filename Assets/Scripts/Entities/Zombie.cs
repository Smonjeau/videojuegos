using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Zombie : MonoBehaviour
{
    // ATTACK CONFIGURATION
    [SerializeField] private int _hitDamage = 5;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    private float _attackRange = 3.2f;
    private float _attackSpeed = 0.6f;
    
    // CONTROLLERS
    private LifeController _lifeController;
    
    // ANIMATIONS
    private Animation _animations;
    private string _attackAnimation = "Z_Walk_InPlace";
    private string _walkAnimation = "Z_Attack";
    private string _deathAnimation = "Z_FallingBack";
    
    private GameObject _target;
    private bool _attacking;

    private void Start()
    {
        _animations = GetComponent<Animation>();
        _lifeController = GetComponent<LifeController>();
        _navMeshAgent.stoppingDistance = _attackRange;
        _target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (_lifeController.IsDead()) Destroy(this);

        var playerPosition = _target.transform.position;
        float distance = DistanceBetween(playerPosition, transform.position);

        if (distance <= _attackRange && _attacking == false)
        {
            _animations.Stop(_walkAnimation);
            _animations.Play(_attackAnimation);
            _attacking = true;
            InvokeRepeating(nameof(Attack), 0, _attackSpeed);
            _navMeshAgent.enabled = false;
        }
        
        if (distance > _attackRange && _attacking)
        {
            _navMeshAgent.enabled = true;
            _attacking = false;
            CancelInvoke(nameof(Attack));
            _animations.Stop(_attackAnimation);
        }
        
        if (_attacking == false) WalkTowards(playerPosition);
    }

    private static float DistanceBetween(Vector3 playerPosition, Vector3 zombiePosition)
    {
        return Vector3.Distance(new Vector3(playerPosition.x, 0, playerPosition.z),
            new Vector3(zombiePosition.x, 0, zombiePosition.z));
    }

    private void WalkTowards(Vector3 playerPosition)
    {
        transform.LookAt(new Vector3(playerPosition.x, transform.position.y, playerPosition.z));
        _navMeshAgent.SetDestination(playerPosition);
    }

    private void Attack()
    {
        IDamagable damageable = _target.GetComponent<IDamagable>();
        damageable?.TakeDamage(_hitDamage);
    }

    private void OnDestroy()
    {
        // TODO: Spawn explosion animation
        _animations.Play(_deathAnimation);
    }
}
