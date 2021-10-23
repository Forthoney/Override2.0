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
