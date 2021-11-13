using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipControlComponent : MonoBehaviour
{

  public UnityEvent OnDamageTaken;
  public UnityEvent OnDeath;

  // Backing fields
  private ShipBody _shipBody;
  private ShipWeapon _shipWeapon;
  private EnemyBehaviour _enemyBehaviour;

  public GameObject FiringSource;
  public SpriteRenderer OutlineSprite;

  // Start is called before the first frame update
  void Start()
  {
    // Debug.Log(_shipBody);
    // Debug.Log(_shipBody.spritePath);
    InitShip();
  }

  // Update is called once per frame
  void Update()
  {
    // Check if ship body has zero health
    if (_shipBody.CurrHealth <= 0)
    {
      GameManager.Instance.ShipDestroy(gameObject);
    }

    // If this is an enemy ship, ask the behaviour to do things
    if (GameManager.PlayerShip != this.gameObject)
    {
      _enemyBehaviour?.doAction();
    }
  }

  public void takeDamage(float damage)
  {
    _shipBody.CurrHealth -= damage;

    if (GameManager.PlayerShip == this.gameObject)
    {
      Debug.Log("Player Hit");
      PlayerControl.Instance?.OnDamageTaken?.Invoke();
      if (_shipBody.CurrHealth <= 0)
        PlayerControl.Instance?.OnDeath?.Invoke();
    }

    OnDamageTaken?.Invoke();
    if (_shipBody.CurrHealth <= 0) OnDeath?.Invoke();
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


  bool _initiated = false;
  // Run once when Ship is created, after ShipBody and ShipWeapon have been loaded, or immediately on start
  public void InitShip()
  {
    if (!_initiated) {
      ShipWeapon.FiringSource = FiringSource == null ? gameObject : FiringSource;
      _initiated = true;
      SpriteRenderer renderer = GetComponent<SpriteRenderer>();
      if (renderer != null)
      {
        renderer.sprite = _shipBody._spritePath;
        renderer.material = _shipWeapon.Material;
      }
      if (OutlineSprite != null)
      {
        OutlineSprite.sprite = _shipBody._outlineSprite;
      }
	  }
  }
}
