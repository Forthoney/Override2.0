using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public static GameObject PlayerShip;
    public List<GameObject> EnemyShips;

    public float Score;
    public TextMeshProUGUI ScoreNumber;

    void Awake()
    {
        Instance = this;
		PlayerShip = GameObject.FindWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        ScoreNumber.SetText(Score.ToString());
    }
}
