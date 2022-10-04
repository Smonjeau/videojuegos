using System.Collections;
using System.Collections.Generic;
using Flyweight;
using Strategy;
using UnityEngine;
public class Gun : MonoBehaviour, IGun
{
    [SerializeField] private GunStats _stats;

    // public GameObject BulletPrefab => _stats.BulletPrefab;
    public int MagSize => _stats.MagSize;
    public int Damage => _stats.Damage;
    
    public int BulletCount => _stats.BulletCount;
    public int CurrentMagSize => _currentMagSize;
    [SerializeField] protected int _currentMagSize;



    public GameObject BulletPrefab => _BulletPrefab;
    // public int MagSize => _MagSize;
    // public int Damage => _Damage;

    [SerializeField] public GameObject _BulletPrefab;
    // [SerializeField] public int _MagSize;
    // [SerializeField] public int _Damage;
    //
    // [SerializeField] public int BulletCount;
    //
    // public int CurrentMagSize => _currentMagSize;
    // [SerializeField] protected int _currentMagSize;
    

    private void Start()
    {
        Reload();
    }

    public virtual void Attack()
    {
        if (_currentMagSize > 0)
        {
            var bullet = Instantiate(
                BulletPrefab,
                transform.position,
                transform.rotation);
            
            bullet.name = "Bullet";
            // Debug.LogError(bullet.gameObject.name);
            
            bullet.GetComponent<Bullet>().SetOwner(this);
            
            _currentMagSize--;
            // UI_AmmoUpdater();
        }
        
    }

    public void Reload()
    {
        _currentMagSize = MagSize;
        // UI_AmmoUpdater();
    } 

    // public void UI_AmmoUpdater()
    // {
    //     EventsManager.instance.AmmoChange(_bulletCount, _stats.MagSize);
    // }
}
