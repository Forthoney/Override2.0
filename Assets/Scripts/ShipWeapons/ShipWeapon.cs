using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class which represents the weapon of a ship
public abstract class ShipWeapon : ScriptableObject
{
  // Backing fields
  [SerializeField] protected float _damage;
  [SerializeField] protected float _fireRate; // Attacks per second
  [SerializeField] protected float _bulletSpeed;

  protected bool _isFiring;
  protected GameObject _firingSource;
  protected GameObject _firingEffect;
  [SerializeField] protected GameObject _bulletPrefab;
  [SerializeField] protected Vector2 _spritePosOffset;
  [SerializeField] protected GameObject _shootEffectPrefab;
  [SerializeField] protected GameObject _shootEffectHitPrefab;

  [SerializeField] protected float _tier;
  [SerializeField] protected Material _material;

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
  public float BulletSpeed
  {
    get => _bulletSpeed;
    set => _bulletSpeed = value;
  }
  public bool IsFiring
  {
    get => _isFiring;
    set => _isFiring = value;
  }
  public GameObject FiringSource
  {
    get => _firingSource;
    set {
		bool load = false;
		if (_firingSource != value)
			load = true;
		_firingSource = value;
		if (load) LoadFiringSource();
	}
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
  public Material Material
  {
    get => _material;
    set => _material = value;
  }
  public GameObject ShootEffectPrefab {
	  get => _shootEffectPrefab;
	  set => _shootEffectPrefab = value;
  }
  public GameObject ShootEffectHitPrefab {
	  get => _shootEffectHitPrefab;
	  set => _shootEffectHitPrefab = value;
  }

  // Important! must be overridden in derived class
  public abstract void Fire(bool isEnemyBullet);

  public void LoadFiringSource() {
	  if (FiringSource != null) {
		GameObject eff = Instantiate<GameObject>(ShootEffectPrefab, FiringSource.transform);
		eff.transform.SetParent(FiringSource.transform);
		_firingEffect = eff;
	  }
  }
}
