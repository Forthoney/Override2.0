using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance = null;

  private static GameObject _playerShip;

  System.Random random = new System.Random();
  public GameObject deathParticleObject;
  public GameObject playerDeathParticleObject;
  public List<GameObject> EnemyShips;

  public TextMeshProUGUI ScoreNumber;
  static float _score;

  public static GameObject PlayerShip
  {
    get { return _playerShip; }
    set
    {
      GameManager.Instance.EnemyShips.Remove(value);
      _playerShip = value;
      _playerShip?.GetComponent<ShipBodySettings>().SetDefaultColor();
    }
  }
  public static float Score
  {
    get => _score; set
    {
      _score = value;
      // ScoreNumber.SetText(_score.ToString());
    }
  }
  public GameObject[] PauseUIObjects;

  public float EnemyRate;
  public float waveSize;
  private float timer;

  private float currWaveSize = 0;

  public GameObject BaseShip;

  private SceneControl scene;
  private bool playerIsDestroyed;

  // Thise pools represents the total pools of scriptable objects 
  // parts have their tier associated with their type
  // parts in tier 0 are the worst, while parts with a higher tier are better
  public ShipBody[] bodyPool = new ShipBody[] { };
  public ShipWeapon[] weaponPool = new ShipWeapon[] { };

  // These pools represent the pool of currently selectable parts. It grows as the game progresses. 
  List<ShipBody> spawnableBodies = new List<ShipBody> { };
  List<ShipWeapon> spawnableWeapons = new List<ShipWeapon> { };

  void Awake()
  {
    Instance = this;
    _playerShip = null;
    if (PauseUIObjects != null)
    {
      // If game is paused
      foreach (GameObject obj in PauseUIObjects)
      {
        // Set everything to inactive
        obj.SetActive(false);
      }
    }
    addRandomBodyToPool(0);
    addRandomWeaponToPool(0);
  }

  // Start is called before the first frame update
  void Start()
  {
    Score = 0;
    timer = 0;
    currWaveSize = 1;
    EnemyShips = new List<GameObject>();
    playerIsDestroyed = false;
  }

  // Update is called once per frame
  void Update()
  {
    if (InputController.Instance.Pausing)
    {
      StartCoroutine(_pause());
    }

    // if we have no player ship, the game should not be running
    if (PlayerShip == null) return;

    Debug.Log(EnemyShips.Count);
    if (currWaveSize == waveSize && EnemyShips.Count < 1)
    {
      timer += EnemyRate - 0.5f;
    }

    timer += Time.deltaTime;
    if (timer > EnemyRate)
    {

      generateWave();
      timer = 0;
    }
  }

  public void ShipDestroy(GameObject destroyedShip)
  {
    if (GameObject.ReferenceEquals(destroyedShip, PlayerShip) && !playerIsDestroyed)
    {
      StartCoroutine(_playerDeathSequence());
    }
    else
    {
      if (GameObject.ReferenceEquals(destroyedShip, PlayerShip))
      {

      }
      else
      {
        EnemyShips.Remove(destroyedShip);
        addScore(100);
        if (deathParticleObject)
        {
          ParticleUtils.EmitOnce(deathParticleObject, destroyedShip.transform.position, Quaternion.identity);
          //   Instantiate(deathParticleObject, destroyedShip.transform.position, Quaternion.identity);
        }
        Destroy(destroyedShip);
      }
    }
  }

  private void addScore(float add)
  {
    Score += add;
    // ScoreNumber.SetText(Score.ToString());
  }

  public void generateWave()
  {
    for (int i = 0; i < currWaveSize; i++)
    {
      GameObject ship = Instantiate<GameObject>(BaseShip, generateEnemyCoords(), Quaternion.identity);
      // better way to randomly choose type from enum without casting int??
      ShipControlComponent shipComponent = ship.GetComponent<ShipControlComponent>();
      shipComponent.ShipBody = getBodyFromPool();
      shipComponent.ShipWeapon = getWeaponFromPool();
      //   shipComponent.ShipWeapon.FiringSource = ship; => not necessarily the case that bullets comes out of the center of the ship. 
      shipComponent.EnemyBehaviour = getBehaviourFromPool(shipComponent);
      shipComponent.InitShip();
      EnemyShips.Add(ship);
    }
    if (currWaveSize < waveSize)
    {
      currWaveSize++;
    }
  }

  private void addRandomBodyToPool(int tier)
  {
    // TODO FIX
    foreach (ShipBody body in bodyPool)
    {
      spawnableBodies.Add(body);
    }
    // spawnableBodies;
  }

  private void addRandomWeaponToPool(int tier)
  {
    // TODO FIX
    foreach (ShipWeapon weapon in weaponPool)
    {
      spawnableWeapons.Add(weapon);
    }
  }

  public ShipBody getBodyFromPool()
  {

    if (spawnableWeapons.Count > 0)
    {
      return Instantiate(spawnableBodies[random.Next(spawnableBodies.Count)]);
    }
    else
    {
      return Instantiate(bodyPool[0]);
    }
  }

  public ShipWeapon getWeaponFromPool()
  {
    if (spawnableWeapons.Count > 0)
    {
      return Instantiate(spawnableWeapons[random.Next(spawnableWeapons.Count)]);
    }
    else
    {
      return Instantiate(weaponPool[0]);
    }
  }

  private Vector3 generateEnemyCoords()
  {
    CameraWallNum wall = (CameraWallNum)Mathf.FloorToInt(Random.Range(0, 4));

    float borderOffset = 3;

    float height = Camera.main.GetComponent<Camera>().orthographicSize;
    float width = height * Screen.width / Screen.height;

    float x = Random.Range(0, width);
    float y = Random.Range(0, height);
    switch (wall)
    {
      case CameraWallNum.North:
        y = height + borderOffset;
        break;
      case CameraWallNum.South:
        y = -height - borderOffset;
        break;
      case CameraWallNum.Weest:
        x = -width - borderOffset;
        break;
      case CameraWallNum.East:
        x = width + borderOffset;
        break;
    }
    return new Vector3(x, y, -5);
  }

  IEnumerator _pause()
  {
    Time.timeScale = 0;
    foreach (GameObject obj in PauseUIObjects)
    {
      obj.SetActive(true);
    }

    while (InputController.Instance.Pausing)
      yield return null;

    foreach (GameObject obj in PauseUIObjects)
    {
      obj.SetActive(false);
    }
    Time.timeScale = 1;
  }

  IEnumerator _playerDeathSequence()
  {
    // Set player is destroyed to true
    playerIsDestroyed = true;

    // Display important console message
    Debug.Log("SUCK IT");

    // Start slow-motion
    Time.timeScale = 0.33f;
    // Instantiate particles
    Instantiate(playerDeathParticleObject, PlayerShip.transform.position, Quaternion.identity);
    // Hide all visible parts of the ship
    PlayerShip.GetComponent<ShipBodySettings>().ToggleDisplay(false);
    // Wait for some time
    yield return new WaitForSecondsRealtime(3);

    // Next...
    TransitionCanvasController controller = FindObjectOfType<TransitionCanvasController>();
    if (controller != null)
    {
      controller.StartHide();
    }
    else
    {
      SceneControl.GameOver();
    }
    // Wait for some time
    yield return new WaitForSecondsRealtime(2);

    // End slow motion
    Time.timeScale = 1;
    SceneControl.GameOver();
  }

  private EnemyBehaviour getBehaviourFromPool(ShipControlComponent shipControlComponent)
  {
    EnemyBehaviour[] behaviourPool = new EnemyBehaviour[] { new FollowBehaviour(shipControlComponent), new BasicBehaviour(shipControlComponent), new FollowFleeBehaviour(shipControlComponent) };

    return behaviourPool[random.Next(behaviourPool.Length)];
  }
}
