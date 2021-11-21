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
  bool _hijackCooldownStopped = true;
  private Timer _hijackCooldown;
  private float _swapMaxDistFromMouse = 5f; // FIXME: magic number was arbitrarily selected (it works ofc)

  private bool _isDead = false;

  private CursorSet currentCursorSet; // FIXME: janky way to change cursors

  public float LowCapHealth = 1;         // Lower limit of HP due to HP decay
  public float HealthToLowCapTime = 15f; // Time to reach lower limit of HP, via decay

  public float FreezeDurationOnSwap = 1f;
  public float HijackCooldownTime = 5f;

  [FMODUnity.EventRef, SerializeField]
  string HijackCooldownEnd;
  [FMODUnity.EventRef, SerializeField]
  string HijackFail;

  public UnityEvent OnDamageTaken = new UnityEvent();
  public UnityEvent OnDeath = new UnityEvent();

  private void Awake()
  {
    Instance = this;
    currentCursorSet = GameObject.Find("Cursor Manager").GetComponent<CursorSet>();
  }

  // Start is called before the first frame update
  void Start()
  {
    OnDeath.AddListener(die);
  }

  // Update is called once per frame
  void Update()
  {
    // Check for player's ship's null-ness
    if (GameManager.PlayerShip == null)
    {
      currentCursorSet.SetCursorCrosshair(); // FIXME: a just-in-case set
      return;
    }

    // Check for player's dead-ness
    if (_isDead)
    {
      currentCursorSet.SetCursorCrosshair(); // FIXME: a just-in-case set
      GameManager.PlayerShip.GetComponent<ShipControlComponent>().ShipBody.zeroVelocity();
    }

    // Change rotation and velocity
    rotateTowardsMouse();
    movePlayer();

    float attackSpeed = 1 / rateOfFire;

    if (GameManager.PlayerShip.GetComponent<ShipControlComponent>().ShipBody.CurrHealth >= 1)
    {
      float maxHealth = GameManager.PlayerShip.GetComponent<ShipControlComponent>().ShipBody.MaxHealth;
      GameManager.PlayerShip.GetComponent<ShipControlComponent>().ShipBody.CurrHealth -=
        (maxHealth - LowCapHealth) * Time.deltaTime / HealthToLowCapTime;

    }

    if (_hijackCooldown)
    {
      float percentage = 100f * _hijackCooldown.TimeLeft / HijackCooldownTime;
      //Debug.Log(percentage);
      if (percentage > 50f)
      {
        GameManager.PlayerShip.GetComponent<ShipBodySettings>()?.Sprite?.material.SetFloat("_Intensity", 2f * Mathf.Sin(Mathf.PI / HijackCooldownTime * 40f * Time.time) + 1.5f);
      }
      //halfway done with cooldown
      else if (percentage > 15f)
      {
        GameManager.PlayerShip.GetComponent<ShipBodySettings>()?.Sprite?.material.SetFloat("_Intensity", 2f * Mathf.Sin(Mathf.PI / HijackCooldownTime * 80f * Time.time) + 1.5f);
      }
      else
      {
        GameManager.PlayerShip.GetComponent<ShipBodySettings>()?.Sprite?.material.SetFloat("_Intensity", 2f * Mathf.Sin(Mathf.PI / HijackCooldownTime * 160f * Time.time) + 1.5f);
      }
    }
    else
    {
      GameManager.PlayerShip.GetComponent<ShipBodySettings>()?.Sprite?.material.SetFloat("_Intensity", 4f);
      if (!_hijackCooldownStopped)
      {
        OnHijackCooldownEnd();
        _hijackCooldownStopped = true;
      }
      GameManager.PlayerShip.GetComponent<ShipBodySettings>()?.Sprite?.material.SetFloat("_Intensity", 1f);
    }

    if (InputController.Instance.Firing && !_firingCooldown && GameManager.PlayerShip.GetComponent<ShipControlComponent>().ShipWeapon != null)
    {
      GameManager.PlayerShip.GetComponent<ShipControlComponent>().ShipWeapon.Fire(false);
      GameManager.PlayerShip.GetComponent<Animator>()?.SetTrigger("AttackRecovery");
      _firingCooldown = new Timer((float)(1f / GameManager.PlayerShip.GetComponent<ShipControlComponent>().ShipWeapon.FireRate));
      _firingCooldown.Start();
    }

    // Handle searching
    if (InputController.Instance.Searching) {
      currentCursorSet.SetCursorArrow();
      ColorNearestShip();
    } else {
      currentCursorSet.SetCursorCrosshair(); // FIXME: a just-in-case set
    }

    // Handle swapping
    if (InputController.Instance.Swapping && !_isDead)
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
          float currDist = (mouseWorldPos - (Vector2)currShip.transform.position).magnitude;
          if (currDist < swapTargetShipDist && currDist < _swapMaxDistFromMouse)
          {
            swapTargetShipDist = currDist;
            swapTargetShip = currShip;
          }
        }
      }

      // If another ship to swap to was found
      if (swapTargetShip != null && !_hijackCooldown)
      {
        _hijackCooldown = new Timer((float)(HijackCooldownTime));
        _hijackCooldown.Start();
        _hijackCooldownStopped = false;
        StartCoroutine(_hijack(swapTargetShip));
      }
      else FMOD_Thuleanx.AudioManager.Instance?.PlayOneShot(HijackFail);

      InputController.Instance.Swapping.Stop();

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

  void ColorNearestShip()
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
        float currDist = (mouseWorldPos - (Vector2)currShip.transform.position).magnitude;
        if (currDist < swapTargetShipDist && currDist < _swapMaxDistFromMouse)
        {
          swapTargetShipDist = currDist;
          swapTargetShip = currShip;
        }
      }
    }

    foreach (var currShip in GameObject.FindObjectsOfType<ShipControlComponent>())
    {
      //not nearest
      if (currShip.gameObject != GameManager.PlayerShip && currShip != swapTargetShip)
      {
        currShip.GetComponent<ShipBodySettings>().SetColor(false);
      }
      else if (currShip == swapTargetShip)
      {
        //overridable
        if (!_hijackCooldown)
        {
          currShip.GetComponent<ShipBodySettings>().SetGlowColor(true);
        }
        //wait for cooldown
        else
        {

        }
      }
    }
  }

  // Move the player back in the bounds if they leave
  private void LateUpdate()
  {
    if (GameManager.PlayerShip == null) return;
    Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    float objectWidth = GameManager.PlayerShip.GetComponent<ShipBodySettings>().Outline.bounds.extents.x; //extents = size of width / 2
    float objectHeight = GameManager.PlayerShip.GetComponent<ShipBodySettings>().Outline.bounds.extents.y; //extents = size of height / 2
    Vector3 viewPos = GameManager.PlayerShip.transform.position;
    viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
    viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
    GameManager.PlayerShip.transform.position = viewPos;
  }

  void OnHijackCooldownEnd()
  {
    GameManager.PlayerShip.GetComponent<Animator>()?.SetTrigger("Shine");
    FMOD_Thuleanx.AudioManager.Instance?.PlayOneShot(HijackCooldownEnd);
  }

  void die()
  {
    _isDead = true;
  }
}
