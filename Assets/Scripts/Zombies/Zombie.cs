using Controllers;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent( 
    typeof(NavMeshAgent),
    typeof(LifeController),
    typeof(ZombieSoundEffectController))]
[RequireComponent(typeof(Animation), typeof(NavMeshObstacle))]
public class Zombie : MonoBehaviour
{

    public ZombieStats Stats => _stats;
    [SerializeField] public ZombieStats _stats;

    // ATTACK CONFIGURATION
    private int _hitDamage => _stats.AttackDamage;
    private float _attackRange => _stats.AttackRange;
    private float _attackSpeed => _stats.AttackSpeed;
    private float _movementSpeed => _stats.MovementSpeed;

    // COMPONENTS
    private LifeController _lifeController;
    private ZombieSoundEffectController _soundEffectController;
    private NavMeshAgent _navMeshAgent;
    
    // ANIMATIONS
    private Animation _animations;
    
    private GameObject _target;
    [SerializeField] private string _state;

    private void Start()
    {
        _animations = GetComponent<Animation>();
        _lifeController = GetComponent<LifeController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _soundEffectController = GetComponent<ZombieSoundEffectController>();
        
        _navMeshAgent.stoppingDistance = _attackRange;
        _navMeshAgent.speed = _movementSpeed;
        
        _target = GameObject.FindGameObjectWithTag("Player");
        _state = ZombieState.WALK;
        _animations.Play(_state);
        
        InvokeRepeating(nameof(PlaySound), 2, 5);
    }

    private void Update()
    {
        if (_state == ZombieState.DIE) return;

        var playerPosition = _target.transform.position;
        float distance = DistanceBetween(playerPosition, transform.position);

        if (distance <= _attackRange && _state != ZombieState.ATTACK)
        {
            ChangeStateTo(ZombieState.ATTACK);
            InvokeRepeating(nameof(Attack), 0, _attackSpeed);
            _navMeshAgent.enabled = false;
        }
        
        if (distance > _attackRange && _state == ZombieState.ATTACK)
        {
            _soundEffectController.Stop();
            _navMeshAgent.enabled = true;
            CancelInvoke(nameof(Attack));
            ChangeStateTo(ZombieState.WALK);
        }
        
        if (_state == ZombieState.WALK || _state == ZombieState.RUN) MoveTowards(playerPosition);
    }

    private static float DistanceBetween(Vector3 playerPosition, Vector3 zombiePosition)
    {
        return Vector3.Distance(new Vector3(playerPosition.x, 0, playerPosition.z),
            new Vector3(zombiePosition.x, 0, zombiePosition.z));
    }

    private void MoveTowards(Vector3 playerPosition)
    {
        transform.LookAt(new Vector3(playerPosition.x, transform.position.y, playerPosition.z));
        _navMeshAgent.SetDestination(playerPosition);
    }

    private void Attack()
    {
        _soundEffectController.PlayOnHit();
        IDamageable damageable = _target.GetComponent<IDamageable>();
        damageable?.TakeDamage(_hitDamage);
    }
    
    private void ChangeStateTo(string state)
    {
        _animations.Stop(_state);
        _state = state;
        _animations.Play(_state);
    }

    private void PlaySound()
    {
        _soundEffectController.Play();
    }

    public void Die()
    {
        _navMeshAgent.enabled = false;
        ChangeStateTo(ZombieState.DIE);
        Destroy(gameObject, 2f);
    }
}
