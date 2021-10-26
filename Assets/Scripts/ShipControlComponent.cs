using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControlComponent : MonoBehaviour
{
  // Backing fields
  private ShipBody _shipBody;
  private ShipWeapon _shipWeapon;
  private EnemyBehaviour _enemyBehaviour;

  // Start is called before the first frame update
  void Start()
  {
    SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
    // Debug.Log(_shipBody);
    // Debug.Log(_shipBody.spritePath);
    var sprite = Resources.Load<Sprite>(_shipBody.spritePath);
    renderer.sprite = sprite;
  }

  // Update is called once per frame
  void Update()
  {
    // Check if ship body has zero health
    if (_shipBody.CurrHealth < 0)
    {
      GameManager.Instance.ShipDestroy(gameObject);
    }

    // TEMPORARY: change color of ship based on fraction of health
    float hFrac = _shipBody.CurrHealth / _shipBody.MaxHealth;
    gameObject.GetComponent<SpriteRenderer>().color = new Color(1, hFrac, hFrac);

    // If this is an enemy ship, ask the behaviour to do things
    if (GameManager.PlayerShip != this.gameObject)
    {
      _enemyBehaviour.doAction();
    }
  }

  public void takeDamage(float damage)
  {
    if (GameManager.PlayerShip == this.gameObject)
    {
      Debug.Log("player hit");
      _shipBody.CurrHealth = Mathf.FloorToInt(_shipBody.CurrHealth);
    }
    else
    {
      _shipBody.CurrHealth -= damage;
    }
  }

  // Accessors
  public ShipBody ShipBody
  {
    get { return _shipBody; }
    set { _shipBody = value; }
  }
  public ShipWeapon ShipWeapon
  {
    get { return _shipWeapon; }
    set { _shipWeapon = value; }
  }
  public EnemyBehaviour EnemyBehaviour
  {
    get { return _enemyBehaviour; }
    set { _enemyBehaviour = value; }
  }

  // Create a new ShipBody or ShipWeapon
  public void setNewBodyFromType(ShipBodyType bodyType)
  {
    switch (bodyType)
    {
      case ShipBodyType.FastSquish:
        {
          _shipBody = new FastSquish();
          break;
        }
      case ShipBodyType.SlowChunk:
        {
          _shipBody = new SlowChunk();
          break;
        }
      default:
        {
          Debug.LogWarning("Unknown ShipBodyType passed to ShipControlComponent::setBody!");
          _shipBody = new SlowChunk();
          break;
        }
    }
  }
  public void setNewWeaponFromType(ShipWeaponType weaponType)
  {
    switch (weaponType)
    {
      case ShipWeaponType.BulletWeapon:
        {
          _shipWeapon = new BulletWeapon("Prefabs/Bullet", new Vector2(0, 0), gameObject);
          break;
        }
      case ShipWeaponType.LaserWeapon:
        {
          _shipWeapon = new LaserWeapon("Prefabs/Bullet", new Vector2(0, 0), gameObject);
          break;
        }
      default:
        {
          Debug.LogWarning("Unknown ShipWeaponType passed to ShipControlComponent::setWeapon!");
          _shipWeapon = new LaserWeapon("Prefabs/Bullet", new Vector2(0, 0), gameObject);
          break;
        }
    }
  }
}
