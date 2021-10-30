using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class which represents the weapon of a ship
public abstract class ShipWeapon : ScriptableObject
{
  // Backing fields
  [SerializeField]
  protected float _damage;
  [SerializeField]
  protected float _fireRate; // Attacks per second
  [SerializeField]
  protected float _bulletSpeed;
  protected bool _isFiring;
  protected GameObject _firingSource;
  [SerializeField] 
  protected GameObject _bulletPrefab;
  [SerializeField]
  protected Vector2 _spritePosOffset;

  public string material { get; set; }

  // Constructor
  protected ShipWeapon(
      float damage,
      float fireRate,
      float bulletSpeed,
      GameObject fSource,
      GameObject bPrefab,
      Vector2 sPosOffset,
      string weaponMaterial)
  {
    _damage = damage;
    _fireRate = fireRate;
    _bulletSpeed = bulletSpeed;
    _isFiring = false;
    _firingSource = fSource;
    _bulletPrefab = bPrefab;
    _spritePosOffset = sPosOffset;
    material = weaponMaterial;
    fSource.GetComponent<SpriteRenderer>().material = Resources.Load<Material>(material);
  }

  // Accessors
  public float Damage
  {
    get => _damage;
    set => _damage = value;
  }
  public float FireRate
  {
    get => _fireRate;
    set => _fireRate = value;
  }
  public bool IsFiring
  {
    get => _isFiring;
    set => _isFiring = value;
  }
  public GameObject FiringSource
  {
    get => _firingSource;
    set => _firingSource = value;
  }
  public GameObject BulletPrefab
  {
    get => _bulletPrefab;
    set => _bulletPrefab = value;
  }
  public Vector2 SpritePosOffset
  {
    get => _spritePosOffset;
    set => _spritePosOffset = value;
  }

  // Important!
  public abstract void Fire(bool isEnemyBullet);
}
