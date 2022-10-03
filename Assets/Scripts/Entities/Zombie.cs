using UnityEngine;
using UnityEngine.AI;

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
    
    private GameObject _target;
    [SerializeField] private string _state;

    private void Start()
    {
        _animations = GetComponent<Animation>();
        _lifeController = GetComponent<LifeController>();
        _navMeshAgent.stoppingDistance = _attackRange;
        _target = GameObject.FindGameObjectWithTag("Player");
        _state = ZombieState.WALK;
        _animations.Play(_state);
    }

    private void Update()
    {
        if (_lifeController.IsDead()) Destroy(this);

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
            _navMeshAgent.enabled = true;
            CancelInvoke(nameof(Attack));
            ChangeStateTo(ZombieState.WALK);
        }
        
        if (_state == ZombieState.WALK) WalkTowards(playerPosition);
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
        ChangeStateTo(ZombieState.DIE);
    }
    
    private void ChangeStateTo(string state)
    {
        _animations.Stop(_state);
        _state = state;
        _animations.Play(_state);
    }
}
