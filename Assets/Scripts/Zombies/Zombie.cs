using System.Linq;
using Controllers;
using Flyweight;
using Managers;
using Strategy;
using UnityEngine;
using UnityEngine.AI;

namespace Zombies
{
    public class Zombie : MonoBehaviour, IDieable
    {

        public ZombieStats Stats => _stats;
        [SerializeField] public ZombieStats _stats;

        // ATTACK CONFIGURATION
        public int _hitDamage => _stats.AttackDamage;
        private float _attackRange => _stats.AttackRange;
        private float _attackSpeed => _stats.AttackSpeed;
        private float _movementSpeed => _stats.MovementSpeed;

        // COMPONENTS
        public ZombieSoundEffectController _soundEffectController;
        private NavMeshAgent _navMeshAgent;
        Collider[] _colliders;
        private Transform _rightHand;
        private bool _isBoss => _stats.IsBoss;

    
        // ANIMATIONS
        private Animation _animations;

        private GameObject _target;
        [SerializeField] private string _state;
        private string _baseState;
        private bool _gameOver;

        private void Start()
        {
            _animations = GetComponent<Animation>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _soundEffectController = GetComponent<ZombieSoundEffectController>();
            _colliders = GetComponents<Collider>();
            _rightHand = GetRightHandTransform();

            _navMeshAgent.stoppingDistance = _attackRange;
            _navMeshAgent.speed = _movementSpeed;

            _baseState = _movementSpeed > 1 ? ZombieState.RUN : ZombieState.WALK;
        
            _target = GameObject.FindGameObjectWithTag("Player");
            _state = _baseState;
            _animations.Play(_state);
        
            InvokeRepeating(nameof(PlaySound), 2, 5);
            InvokeRepeating(nameof(RangeAttackAction),1,7);
            
            EventsManager.Instance.OnGameOver += OnGameOver;
        }

        private void Update()
        {
            if (_state == ZombieState.DIE || _gameOver) return;

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
                ChangeStateTo(_baseState);
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

        public virtual void Attack()
        {
            _soundEffectController.PlayOnHit();
            IDamageable damageable = _target.GetComponent<IDamageable>();
            damageable?.TakeDamage(_hitDamage);
        }

        private void RangeAttack()
        {

            GameObject throwable = Instantiate(_stats.ThrowablePrefab, _rightHand.position, transform.rotation);
            
            Rigidbody rb = throwable.GetComponent<Rigidbody>();
    

            var targetPos = _target.transform.position;
            targetPos.y += 1;
            var cam = GameObject.FindGameObjectWithTag("cam-position").transform.position;
            cam.y += 5;
            rb.AddForce(-(_rightHand.position - cam) , ForceMode.VelocityChange);
            _soundEffectController.PlayOnZombieThrow();
            ChangeStateTo(_baseState);

        }
    
        private void ChangeStateTo(string state)
        {
            _animations.Stop(_state);
            _state = state;
            _animations.Play(_state);
        }

        private void PlaySound() => _soundEffectController.Play();

        public void Die()
        {
            _colliders.ToList().ForEach(c => c.enabled = false);
            _navMeshAgent.enabled = false;
            ChangeStateTo(ZombieState.DIE);
     
            if (_isBoss)
                LevelManager.Instance.BossKill();
            else
                LevelManager.Instance.ZombieKill();
            Destroy(gameObject, 2f);
        }

        private void OnGameOver(bool gameOver)
        {
            _gameOver = true;
            CancelInvoke(nameof(Attack));
            ChangeStateTo(ZombieState.IDLE);
            Destroy(this);
        }


        private Transform GetRightHandTransform()
        {
     
            return transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0)
                .GetChild(0).GetChild(0);
        }

        private void RangeAttackAction()
        {
            var playerPosition = _target.transform.position;
            float distance = DistanceBetween(playerPosition, transform.position);
            if (_stats.CanDoRangeAttack && distance > _attackRange && _state != ZombieState.RANGE_ATTACK )
            {
                ChangeStateTo(ZombieState.RANGE_ATTACK);
                
            }

        }
    }
}
