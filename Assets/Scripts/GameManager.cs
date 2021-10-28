using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance = null;

  public static GameObject PlayerShip;
  public GameObject deathParticleObject;
  public List<GameObject> EnemyShips;

  public TextMeshProUGUI ScoreNumber;
  float _score;
  public float Score
  {
    get => _score; set
    {
      _score = value;
      ScoreNumber.SetText(_score.ToString());
    }
  }
  public GameObject[] PauseUIObjects;

  public float EnemyRate;
  private float timer;

  public GameObject BaseShip;

  private SceneControl scene;
  private bool playerdestruction;

  void Awake()
  {
    Instance = this;
    PlayerShip = GameObject.FindWithTag("Player");
    foreach (GameObject obj in PauseUIObjects)
    {
      obj.SetActive(false);
    }
  }

  // This pool represents the ship part tiers. 
  // parts in tier 0 are the worst, while parts with a higher tier are better
  ShipBody[,] bodyPool = new ShipBody[,] { { } };
  ShipWeapon[,] weaponPool = new ShipWeapon[,] { { } };


  // Start is called before the first frame update
  void Start()
  {
    Score = 0;
    timer = 0;
    EnemyShips = new List<GameObject>();
    scene = new SceneControl();
    playerdestruction = false;
  }

  // Update is called once per frame
  void Update()
  {
    if (InputController.Instance.Pausing)
    {
      StartCoroutine(_pause());
    }

    timer += Time.deltaTime;
    if (timer > EnemyRate)
    {
      for (int i = 0; i < (int)Random.Range(1, 4); i++)
      {
        generateEnemy();
      }
      timer = 0;
    }
  }

  public void ShipDestroy(GameObject destroyedShip)
  {
    if (GameObject.ReferenceEquals(destroyedShip, PlayerShip) && !playerdestruction)
    {
      StartCoroutine(_playerDeathSequence());
	  if (deathParticleObject != null)
      	Instantiate(deathParticleObject, destroyedShip.transform.position, Quaternion.identity);
    }
    else
    {
      if (GameObject.ReferenceEquals(destroyedShip, PlayerShip))
      {
        
      }
      else
      {
        EnemyShips.Remove(destroyedShip);
        AddScore(100);
		if (deathParticleObject != null)
        	Instantiate(deathParticleObject, destroyedShip.transform.position, Quaternion.identity);
        Destroy(destroyedShip);
      }
    }
  }

  public void AddScore(float add)
  {
    Score += add;
    // ScoreNumber.SetText(Score.ToString());
  }

  public void generateEnemy()
  {
    GameObject ship = Instantiate(BaseShip, generateEnemyCoords(), Quaternion.Euler(new Vector3(0, 0, 0)));
    // better way to randomly choose type from enum without casting int??
    ShipBodyType bodyType = (ShipBodyType)Mathf.Floor(Random.Range(0, 2));
    ShipWeaponType weaponType = (ShipWeaponType)Mathf.Floor(Random.Range(0, 2));
    ShipControlComponent shipComponent = ship.GetComponent<ShipControlComponent>();
    shipComponent.setNewBodyFromType(bodyType);
    shipComponent.setNewWeaponFromType(weaponType);
    shipComponent.EnemyBehaviour = new BasicBehaviour(shipComponent);
    EnemyShips.Add(ship);
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
        playerdestruction = true;
        PlayerShip.GetComponent<SpriteRenderer>().enabled ^= true;
        Time.timeScale = 0.5f;
        yield return new WaitForSecondsRealtime(5);
        Time.timeScale = 1;
        scene.GameOver();
    }
}
