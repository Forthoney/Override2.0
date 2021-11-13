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
  [SerializeField] protected float _accelerationLambda;
  [SerializeField] protected float _toZeroAccelerationLambdaModifier;
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
  public float ToZeroAccelerationLambdaModifier
  {
    get => _toZeroAccelerationLambdaModifier;
    set => _toZeroAccelerationLambdaModifier = value;
  }
  public Sprite SpritePath
  {
    get => _spritePath;
    set => _spritePath = value;
  }

  // Important!
  public void move()
  {
    Vector2 currVel = GameManager.PlayerShip.GetComponent<Rigidbody2D>().velocity;
    Vector2 targetVel = InputController.Instance.Movement * _speed;

    // Different acceleration bonuses for different inputs
    float otherDirectionBonus = 
      targetVel == Vector2.zero ? 
      _toZeroAccelerationLambdaModifier : // When decelerating to zero (no player input)
      Mathf.Max(2 * (-Vector2.Dot(currVel.normalized, targetVel.normalized) + 1), 1); // When actively accelerating in opposite direction
    
    GameManager.PlayerShip.GetComponent<Rigidbody2D>().velocity =
      Vector2.Lerp(currVel, targetVel, 1 - Mathf.Exp(-_accelerationLambda * otherDirectionBonus * Time.deltaTime));
  }
}
