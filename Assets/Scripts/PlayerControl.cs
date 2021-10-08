using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public GameObject player;

    // This value should be read out of the ship body in the future
    public float speed;

    // Represents how many bullets fired per second. This should be read out of the ship weapon in the future
    public float rateOfFire;

    private float timer;

    private bool canFire;

    public Transform bullet;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        canFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        rotateTowardsMouse();
        movePlayer();

        float attackSpeed = 1/rateOfFire;

        

        if(Input.GetButton("Fire1")){
            if(canFire) {
                instantiateBullet();
                canFire = false;
            } 

        }

        if(timer > attackSpeed){
            canFire = true;
            timer = 0;
        }

        if (!canFire){
            timer += Time.deltaTime;
        }


    }


    void rotateTowardsMouse(){
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 playerToMouse = mousePos - player.transform.position;
        float angle = Mathf.Atan2(playerToMouse.y, playerToMouse.x) * Mathf.Rad2Deg;
        player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    void movePlayer(){
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        player.GetComponent<Rigidbody2D>().velocity += new Vector2(speed * horizontal * Time.deltaTime, speed * vertical * Time.deltaTime);
    }

    void instantiateBullet() {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            Vector3 playerToMouse = mousePos - player.transform.position;
            float angle = Mathf.Atan2(playerToMouse.y, playerToMouse.x) * Mathf.Rad2Deg;

            playerToMouse = playerToMouse.normalized;

            Instantiate(bullet, player.transform.position + playerToMouse, Quaternion.Euler(new Vector3(0, 0, angle - 90)));
    }

}
