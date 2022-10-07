using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    int Life { get; }
    int MaxLife { get; }
    void TakeDamage(int damage);
    void SetLife(int life);
    void Heal(int heal);
}
