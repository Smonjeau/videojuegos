using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Entities;
using Flyweight;
using Managers;
using Strategy;
using UnityEditor;
using UnityEngine;
public class Gun : MonoBehaviour, IGun
{
    [SerializeField] private GunStats _stats;
    public GameObject BulletPrefab => _stats.BulletPrefab;
    public GameObject GunPrefab => _stats.GunPrefab;
    public int MagSize => _stats.MagSize;
    public int MaxAmmo => _stats.MaxAmmo;
    public int Damage => _stats.Damage;
    public bool InfiniteAmmo => _stats.InfiniteAmmo;

    public string Name => _stats.WeaponName;

    public int BulletCount => _stats.BulletCount;

    public float ShotCooldown => 60/_stats.RateOfFire;
    public float ReloadCooldown => _stats.ReloadCooldown;

    private float _cooldownTimer = 0;

    public int CurrentMagSize => _currentMagSize;
    
    [SerializeField] private int _currentMagSize;
    [SerializeField] private int _currentAmmo;

    

    private Transform _barrelExitTransform;
    

    private void Start()
    {
        _currentMagSize = MagSize;
        EventsManager.Instance.EventAmmoChange(_currentMagSize,_currentAmmo);
    
        if (!InfiniteAmmo) _currentAmmo = MaxAmmo - _currentMagSize;
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
        EventsManager.Instance.EventAmmoChange(_currentMagSize,_currentAmmo);
        // UI_AmmoUpdater();

        _cooldownTimer = ShotCooldown;
    }
        
    
    public void Reload()
    {
        if (!CanFire() || OutOfAmmo()) return; //not ready to fire
        
        if (InfiniteAmmo)
        {
            _currentMagSize = MagSize;
        }
        else
        {
            var ammoToAdd = _currentAmmo < MagSize ? _currentAmmo : MagSize - _currentMagSize;
            _currentMagSize += ammoToAdd;
            _currentAmmo -= ammoToAdd;
        }
        _cooldownTimer = ReloadCooldown;
        StartCoroutine(UI_AmmoUpdater());
    }

    public void AddAmmo(int amount)
    {
        if (InfiniteAmmo) return;
        if (_currentAmmo + amount > MaxAmmo) _currentAmmo = MaxAmmo;
        else _currentAmmo += amount;
    }

    public void FullAmmo() => _currentAmmo = MaxAmmo;

    private bool CanFire() => ! (_cooldownTimer > 0);

    public void Reset()
    {
        _currentMagSize = MagSize;
        _currentAmmo = MaxAmmo;
        _cooldownTimer = 0;
    }

     private IEnumerator UI_AmmoUpdater()
     {
         yield return new WaitForSeconds(_cooldownTimer);
         EventsManager.Instance.EventAmmoChange(_currentMagSize, _stats.MaxAmmo);
     }
    private bool OutOfAmmo() => _currentAmmo <= 0;
}
