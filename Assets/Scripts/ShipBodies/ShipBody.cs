using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class which represents the body of a ship
[CreateAssetMenu(fileName = "ShipBody", menuName = "~/Combat/ShipBody", order = 1)]
public class ShipBody : ScriptableObject
{
  // Backing fields
  [SerializeField] protected float _maxHealth;
  public float _currHealth = 3;
  [SerializeField] protected float _speed;
  [SerializeField] protected float _colliderRadius;
  [SerializeField] protected float _accelerationLambda = 8f;
  public Sprite _spritePath;
  [SerializeField] protected float tier;

  void Awake()
  {
    CurrHealth = MaxHealth;
  }

  // Accessors
  public float MaxHealth
  {
    get => _maxHealth;
    set => _maxHealth = value;
  }
  public float CurrHealth
  {
    get => _currHealth;
    set => _currHealth = value;
  }
  public float Speed
  {
    get => _speed;
    set => _speed = value;
  }
  public float AccelerationLambda
  {
    get => _accelerationLambda;
    set => _accelerationLambda = value;
  }
  public Sprite SpritePath
  {
    get => _spritePath;
    set => _spritePath = value;
  }

  // Copy constructor
  public ShipBody(ShipBody other)
  {

    _maxHealth = other.MaxHealth;
    _currHealth = other.CurrHealth;
    _speed = other.Speed;
    _colliderRadius = other._colliderRadius;
    _accelerationLambda = other._accelerationLambda;
    _spritePath = other.SpritePath;
  }

  // Constructor
  protected ShipBody(float h, float s, float cr, float accelerationLambda, string sp)
  {
    _maxHealth = h;
    _currHealth = h;
    _speed = s;
    _colliderRadius = cr;
    _accelerationLambda = accelerationLambda;
    _spritePath = Resources.Load<Sprite>(sp);
  }

  // Important!
  public void move()
  {
    Vector2 vel = GameManager.PlayerShip.GetComponent<Rigidbody2D>().velocity;
    GameManager.PlayerShip.GetComponent<Rigidbody2D>().velocity =
        Vector2.Lerp(vel, InputController.Instance.Movement * _speed, 1 - Mathf.Exp(-AccelerationLambda * Time.deltaTime));
  }
}
