using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public static GameObject PlayerShip;
    public List<GameObject> EnemyShips;

    public TextMeshProUGUI ScoreNumber;
	float _score;
    public float Score {get => _score; set {
		_score = value;
        ScoreNumber.SetText(_score.ToString());
	}}

    public float EnemyRate;
    private float timer;

    public GameObject BaseShip;

    void Awake()
    {
        Instance = this;
		PlayerShip = GameObject.FindWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        timer = 0;
        EnemyRate = 5;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Debug.Log(timer);
        if (timer > EnemyRate) {
            generateEnemy();
            timer = 0;
        }
    }

    public void generateEnemy()
    {
        Instantiate(BaseShip, new Vector3(Random.value, Random.value, -5), Quaternion.Euler(new Vector3(0, 0, 0)));
        int bodyType = Mathf.FloorToInt(Random.Range(0, 2));
        int weaponType = Mathf.FloorToInt(Random.Range(0, 2));

        switch(bodyType) {
            case 0:
                BaseShip.GetComponent<ShipControlComponent>().setBody(new FastSquish());
                break;
            default:
                BaseShip.GetComponent<ShipControlComponent>().setBody(new SlowChunk());
                break;
        }

        switch(weaponType) {
            case 0:
                BaseShip.GetComponent<ShipControlComponent>().setWeapon(new BulletWeapon("Enemy Bullet", new Vector2(0, 0), BaseShip));
                break;
            default:
                BaseShip.GetComponent<ShipControlComponent>().setWeapon(new LaserWeapon("Bullet", new Vector2(0, 0), BaseShip));
                break;
        }

        EnemyShips.Add(BaseShip);
    }

    public void ShipDestroy(GameObject destroyedShip)
    {
        if (GameObject.ReferenceEquals(destroyedShip, PlayerShip))
        {

        }
        else
        {
            EnemyShips.Remove(destroyedShip);
            AddScore(100);
            Destroy(destroyedShip);
        }
    }

    public void AddScore(float add)
    {
        Score += add;
        // ScoreNumber.SetText(Score.ToString());
    }
}
