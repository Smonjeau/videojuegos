using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    int Life { get; }
    int MaxLife { get; }
    void TakeDamage(int damage);
}
