using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Flyweight;
using Strategy;
using UnityEditor;
using UnityEngine;
public class Gun : MonoBehaviour, IGun
{
    [SerializeField] private GunStats _stats;
    public GameObject BulletPrefab => _stats.BulletPrefab;
    public GameObject GunPrefab => _stats.GunPrefab;
    public int MagSize => _stats.MagSize;
    public int Damage => _stats.Damage;

    public string Name => _stats.WeaponName;

    public int BulletCount => _stats.BulletCount;

    public float ShotCooldown => 60/_stats.RateOfFire;
    public float ReloadCooldown => _stats.ReloadCooldown;

    private float _cooldownTimer = 0;

    public int CurrentMagSize => _currentMagSize;
    
    private int _currentMagSize;

    

    private Transform _barrelExitTransform;
    

    private void Start()
    {
        // _stats 
        // Debug.Log("test");
        // Debug.Log("magsize is"+_stats.MagSize);
        // Debug.Log("found"+MagSize);
        _currentMagSize = MagSize;
        // _barrelExitTransform = transform.GetChild(0).GetChild(0);
        _barrelExitTransform = transform.GetChild(0);
        
    }

    private void Update()
    {
        _cooldownTimer -= Time.deltaTime;
    }

    public virtual void Attack()
    {
        if (!CanFire()) return; //not ready to fire
        
        if (_currentMagSize <= 0) //empty mag
        {
            //play click sound
            return;
        }

        
        var bullet = Instantiate(BulletPrefab, _barrelExitTransform.position, transform.rotation);
        bullet.name = "Bullet";
        bullet.GetComponent<Bullet>().SetOwner(this);
        
        
        _currentMagSize--;
        // UI_AmmoUpdater();

        _cooldownTimer = ShotCooldown;
    }
        
    
    public void Reload()
    {
        if (!CanFire()) return; //not ready to fire
        _currentMagSize = MagSize;
        _cooldownTimer = ReloadCooldown;
        // UI_AmmoUpdater();
    }

    public bool CanFire()
    {
        return ! (_cooldownTimer > 0);
    }

    public void Reset()
    {
        _currentMagSize = MagSize;
        _cooldownTimer = 0;
    }

    // public void UI_AmmoUpdater()
    // {
    //     EventsManager.instance.AmmoChange(_bulletCount, _stats.MagSize);
    // }
}
