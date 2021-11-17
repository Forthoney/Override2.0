using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControl : MonoBehaviour
{
  public static PlayerControl Instance;

  // This value should be read out of the ship body in the future
  public float speed;

  // Represents how many bullets fired per second. This should be read out of the ship weapon in the future
  public float rateOfFire;

  public Transform bullet;

  private Timer _firingCooldown;
  private Timer _hijackCooldown;
  private float _swapMaxDistFromMouse = 5f; // FIXME: magic number was arbitrarily selected (it works ofc)

  private bool _isDead = false;

  public float FreezeDurationOnSwap = 1f;
  public float HealthDecrement = 2f;
  public float HijackCooldownTime = 10f;

  public UnityEvent OnDamageTaken;
  public UnityEvent OnDeath;

  private void Awake()
  {
    Instance = this;
  }

  // Start is called before the first frame update
  void Start()
  {
    OnDeath.AddListener(die);
  }

  // Update is called once per frame
  void Update()
  {
    if (!_isDead && GameManager.PlayerShip != null)
    {
      rotateTowardsMouse();
      movePlayer();

      float attackSpeed = 1 / rateOfFire;

      if (GameManager.PlayerShip.GetComponent<ShipControlComponent>().ShipBody.CurrHealth >= 1)
        GameManager.PlayerShip.GetComponent<ShipControlComponent>().ShipBody.CurrHealth -= HealthDecrement * Time.deltaTime;

      if (_hijackCooldown)
      {
        float percentage = 100f * _hijackCooldown.TimeLeft / HijackCooldownTime;
        //Debug.Log(percentage);
        if (percentage > 50f)
        {
          GameManager.PlayerShip.GetComponent<ShipBodySettings>()?.Sprite?.material.SetFloat("_Intensity", 0.4f * Mathf.Sin(Mathf.PI / HijackCooldownTime * 10f * Time.time) + 0.6f);
        }
        //halfway done with cooldown
        else if (percentage > 15f)
        {
          GameManager.PlayerShip.GetComponent<ShipBodySettings>()?.Sprite?.material.SetFloat("_Intensity", 0.4f * Mathf.Sin(Mathf.PI / HijackCooldownTime * 30f * Time.time) + 0.6f);
        }
        else
        {
          GameManager.PlayerShip.GetComponent<ShipBodySettings>()?.Sprite?.material.SetFloat("_Intensity", 0.3f * Mathf.Sin(Mathf.PI / HijackCooldownTime * 60f * Time.time) + 0.7f);
        }
      }
      else
      {
        GameManager.PlayerShip.GetComponent<ShipBodySettings>()?.Sprite?.material.SetFloat("_Intensity", 1f);
      }

      if (InputController.Instance.Firing && !_firingCooldown && GameManager.PlayerShip.GetComponent<ShipControlComponent>().ShipWeapon != null)
      {
        GameManager.PlayerShip.GetComponent<ShipControlComponent>().ShipWeapon.Fire(false);
        _firingCooldown = new Timer((float)(1f / GameManager.PlayerShip.GetComponent<ShipControlComponent>().ShipWeapon.FireRate));
        _firingCooldown.Start();
      }

      // Handle swapping
      if (InputController.Instance.Swapping)
      {
        // Prepare stuff
        ShipControlComponent swapTargetShip = null;
        float swapTargetShipDist = float.PositiveInfinity;
        Vector2 mouseWorldPos = InputController.Instance.MouseWorldPos;

        // Iterate over all ships to find a ship to swap to
        foreach (var currShip in GameObject.FindObjectsOfType<ShipControlComponent>())
        {
          // If it isn't the player's ship
          if (currShip.gameObject != GameManager.PlayerShip)
          {
            float currDist = (mouseWorldPos - (Vector2) currShip.transform.position).magnitude;
            if (currDist < swapTargetShipDist && currDist < _swapMaxDistFromMouse)
            {
              swapTargetShipDist = currDist;
              swapTargetShip = currShip;
            }
          }
        }

        // If another ship to swap to was found
        if (swapTargetShip != null)
        {
          if (!_hijackCooldown) // FIXME? consider tradeoffs of moving cooldown check to "handle swapping" if
          {
            _hijackCooldown = new Timer((float)(HijackCooldownTime));
            _hijackCooldown.Start();
            StartCoroutine(_hijack(swapTargetShip));
          }
        }
        
        InputController.Instance.Swapping = false;
      }
    }
  }

  // Tell the ship body to rotate towards the mouse
  void rotateTowardsMouse()
  {
    Vector2 mouseWorldPos = InputController.Instance.MouseWorldPos;
    Vector2 currPos = GameManager.PlayerShip.GetComponent<Rigidbody2D>().position;
    Vector2 direction = (mouseWorldPos - currPos).normalized;

    GameManager.PlayerShip.GetComponent<ShipControlComponent>().ShipBody.rotateTowardsWorldPos(GameManager.PlayerShip, mouseWorldPos);
  }

  void movePlayer()
  {
    GameManager.PlayerShip.GetComponent<ShipControlComponent>().ShipBody.move();
  }

  void instantiateBullet()
  {
    Vector3 mouseWorldPos = InputController.Instance.MouseWorldPos;

    Vector3 playerToMouse = mouseWorldPos - GameManager.PlayerShip.transform.position;
    float angle = Mathf.Atan2(playerToMouse.y, playerToMouse.x) * Mathf.Rad2Deg;

    playerToMouse = playerToMouse.normalized;

    Instantiate(bullet, GameManager.PlayerShip.transform.position + playerToMouse, Quaternion.Euler(new Vector3(0, 0, angle - 90)));
  }

  IEnumerator _hijack(ShipControlComponent swapTargetShip)
  {
    Time.timeScale = 0;
    TimerUnscaled pause = new TimerUnscaled(FreezeDurationOnSwap);
    pause.Start();

    swapTargetShip.ShipBody.CurrHealth = swapTargetShip.ShipBody.MaxHealth;

    FindObjectOfType<Override>()?.Trigger(GameManager.PlayerShip.transform.position, swapTargetShip.transform.position);

    Destroy(GameManager.PlayerShip);
    GameManager.PlayerShip = swapTargetShip.gameObject;

    while (pause) yield return null;
    Time.timeScale = 1;
  }

  void die()
  {
    _isDead = true;
  }



}