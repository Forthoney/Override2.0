using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public static GameObject PlayerShip;
    public List<GameObject> EnemyShips;

    public float Score;

    void Awake()
    {
        Instance = this;
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
            Score += destroyedShip.GetComponent<ShipControlComponent>().maxHealth;
            Destroy(destroyedShip);
        }
    }
}
