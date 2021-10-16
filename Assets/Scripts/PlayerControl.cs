using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    // This value should be read out of the ship body in the future
    public float speed;

    // Represents how many bullets fired per second. This should be read out of the ship weapon in the future
    public float rateOfFire;

    public Transform bullet;

	private Timer _firingCooldown;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rotateTowardsMouse();
        movePlayer();

        float attackSpeed = 1/rateOfFire;

        if (InputController.Instance.Firing && !_firingCooldown) {
            GameManager.PlayerShip.GetComponent<ShipControlComponent>().getWeapon().Fire();
			_firingCooldown = new Timer((float) (1f / GameManager.PlayerShip.GetComponent<ShipControlComponent>().getWeapon().FireRate));
			_firingCooldown.Start();
        } 
    }

    void rotateTowardsMouse(){
        Vector3 mousePos = InputController.Instance.MouseWorldPos;
        Vector3 playerToMouse = mousePos - GameManager.PlayerShip.transform.position;
        float angle = Mathf.Atan2(playerToMouse.y, playerToMouse.x) * Mathf.Rad2Deg;
        GameManager.PlayerShip.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    void movePlayer(){
        GameManager.PlayerShip.GetComponent<ShipControlComponent>().getBody().move();
    }

    void instantiateBullet() {
            Vector3 mousePos = InputController.Instance.MouseWorldPos;

            Vector3 playerToMouse = mousePos - GameManager.PlayerShip.transform.position;
            float angle = Mathf.Atan2(playerToMouse.y, playerToMouse.x) * Mathf.Rad2Deg;

            playerToMouse = playerToMouse.normalized;

            Instantiate(bullet, GameManager.PlayerShip.transform.position + playerToMouse, Quaternion.Euler(new Vector3(0, 0, angle - 90)));
    }

}
