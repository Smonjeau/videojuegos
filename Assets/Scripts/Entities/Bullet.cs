using System.Collections.Generic;
using Entities;
using Strategy;
using UnityEngine;

[RequireComponent(typeof(Collider),typeof(Rigidbody))]
public class Bullet : MonoBehaviour, IBullet
{
    public float LifeTime => _lifeTime;
    [SerializeField] private float _lifeTime;
    
    public float Speed => _speed;
    [SerializeField] private float _speed;

    public Gun Owner => _owner;
    [SerializeField] private Gun _owner;

    public Rigidbody Rigidbody => _rigidbody;
    [SerializeField] private Rigidbody _rigidbody;

    public Collider Collider => _collider;
    [SerializeField] private Collider _collider;

    [SerializeField] private List<int> _layerTarget;

    public void Travel() => transform.Translate(Vector3.forward * Time.deltaTime * _speed);

    public void OnTriggerEnter(Collider collider)
    {
        //if bullet hits player ignore
        if (collider.gameObject.name=="Soldier"||collider.gameObject.GetComponentInParent<Character>()?.name == "Soldier") return;
        
        //if whatever is hits is damageable, damage it
        IDamagable damageable = collider.GetComponent<IDamagable>();
        damageable?.TakeDamage(_owner.Damage);
        
        Destroy(this.gameObject);
       
    }

    private void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _collider = gameObject.GetComponent<Collider>();

        _collider.isTrigger = true;
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
    }

    private void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0) Destroy(this.gameObject);

        Travel();
    }

    public void SetOwner(Gun owner) => _owner = owner;
}