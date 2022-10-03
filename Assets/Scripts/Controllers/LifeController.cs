
using System;
using UnityEngine;

public class LifeController : MonoBehaviour, IDamagable
{
    public int Life => _life;
    [SerializeField] private int _life;
    public int MaxLife => _maxLife;
    [SerializeField] private int _maxLife = 100;

    private void Start()
    {
        _life = _maxLife;
    }

    public void TakeDamage(int damage)
    {
        _life -= damage;
        if (_life <= 0)
        {
            Die();
        }
    }
    
    public bool IsDead() => _life <= 0;
    
    private void Die()
    {
        Destroy(gameObject);
    }
}