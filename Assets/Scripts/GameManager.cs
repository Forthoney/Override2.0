using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance = null;

  private static GameObject _playerShip;

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


  private SceneControl scene;
  private bool playerIsDestroyed;



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
  }

  // Start is called before the first frame update
  void Start()
  {
    Score = 0;
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

    // Let the spawnmanager do its thing
    SpawnManager.Instance.Spawn();
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

}
