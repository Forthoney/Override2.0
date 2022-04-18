using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

  System.Random random = new System.Random();
  public static SpawnManager Instance = null;
  private float currWaveSize = 0;
  public GameObject BaseShip;

  // These pools represents the total pools of scriptable objects 
  // parts have their tier associated with their type
  // parts in tier 0 are the worst, while parts with a higher tier are better
  public ShipBody[] bodyPool = new ShipBody[] { };
  public ShipWeapon[] weaponPool = new ShipWeapon[] { };

  // These pools represent the pool of currently selectable parts. It grows as the game progresses. 
  List<ShipBody> spawnableBodies = new List<ShipBody> { };
  List<ShipWeapon> spawnableWeapons = new List<ShipWeapon> { };

  [Tooltip("The number of seconds between each wave")]
  public float EnemyRate;

  // The amount of enemies spawned each wave
  [Tooltip("The amount of enemies spawned each wave")]
  public float waveSize;
  private float timer;


  // Start is called before the first frame update
  void Start()
  {
    currWaveSize = 1;
    timer = 0;
  }
  void Awake()
  {
    Instance = this;
    addRandomBodyToPool(0);
    addRandomWeaponToPool(0);
  }

  public void Spawn()
  {
    if (currWaveSize == waveSize && GameManager.Instance.EnemyShips.Count < 1)
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
      GameManager.Instance.EnemyShips.Add(ship);
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

  private EnemyBehaviour getBehaviourFromPool(ShipControlComponent shipControlComponent)
  {
    EnemyBehaviour[] behaviourPool = new EnemyBehaviour[] { new FollowBehaviour(shipControlComponent), new BasicBehaviour(shipControlComponent), new FollowFleeBehaviour(shipControlComponent) };

    return behaviourPool[random.Next(behaviourPool.Length)];
  }

}
