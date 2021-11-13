using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class which represents the body of a ship
[CreateAssetMenu(fileName = "ShipBody", menuName = "~/Combat/ShipBody", order = 1)]
public class ShipBody : ScriptableObject
{
  // Backing fields
  [SerializeField] protected float _maxHealth;
  [SerializeField] protected float _currHealth;
  [SerializeField] protected float _speed;
  [SerializeField] protected float _rotationSpeed = 0; // If you forget to limit rotation speed, it will obviously break
  [SerializeField] protected float _colliderRadius;
  [SerializeField] protected float _accelerationLambda;
  [SerializeField] protected float _toZeroAccelerationLambdaModifier = 1f;
  public Sprite _spritePath, _outlineSprite; // FIXME: why public?
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
  public Sprite OutlineSprite
  {
    get => _outlineSprite;
    set => _outlineSprite = value;
  }

  // Important! For PLAYER's movement
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

  // Important! For ARBITRARY GameObject's rotation
  public void rotateTowardsWorldPos(GameObject obj, Vector2 worldPos)
  {
    // Get target direction and angle
    Vector2 currPos = obj.GetComponent<Rigidbody2D>().position;
    Vector2 targetDirection = (worldPos - currPos).normalized;
    float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

    // Player has uncapped rotation speed
    if (obj == GameManager.PlayerShip)
    {
      if (Time.deltaTime != 0)
      {
        GameManager.PlayerShip.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
      }
      return;
    }

    // Enemies have capped rotation speed
    Quaternion currRot = obj.transform.rotation;
    Quaternion newRot = Quaternion.Euler(0, 0, angle - 90);
    obj.transform.rotation = Quaternion.RotateTowards(currRot, newRot, Time.deltaTime * _rotationSpeed);
  }
}
