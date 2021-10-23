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
        if (timer > EnemyRate) {
            generateEnemy();
            timer = 0;
        }
    }

    public void generateEnemy()
    {
        Instantiate(BaseShip, generateEnemyCoords(), Quaternion.Euler(new Vector3(0, 0, 0)));
        // better way to randomly choose type from enum without casting int??
        ShipBodyType bodyType = (ShipBodyType) Mathf.Round(Random.Range(0, 1));
        ShipWeaponType weaponType = (ShipWeaponType) Mathf.Round(Random.Range(0, 1));
        BaseShip.GetComponent<ShipControlComponent>().setBody(bodyType);
        BaseShip.GetComponent<ShipControlComponent>().setWeapon(weaponType);
        EnemyShips.Add(BaseShip);
    }

    private Vector3 generateEnemyCoords() {
        CameraWallNum wall = (CameraWallNum) Mathf.FloorToInt(Random.Range(0, 4));

        float borderOffset = 3;

        float height = Camera.main.GetComponent<Camera>().orthographicSize;
        float width = height * Screen.width / Screen.height;

        float x = Random.Range(0, width);
        float y = Random.Range(0, height);
        switch (wall) {
            case CameraWallNum.North:
                Debug.Log("north");
                y = height + borderOffset;
                break;
            case CameraWallNum.South:
            Debug.Log("south");
                y = -height - borderOffset;
                break;
            case CameraWallNum.Weest:
            Debug.Log("weest");
                x = -width - borderOffset;
                break;
            case CameraWallNum.East:
            Debug.Log("east");
                x = width + borderOffset;
                break;
        }
        return new Vector3(x, y, -5);
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
