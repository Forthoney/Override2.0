using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    // Set by the ship weapon after spawn
    public float speed {
        get; set;
    }

    public float damage {
        get; set;
    }

    public bool isEnemyBullet;

    // Update is called once per frame
    void Update()
    {

        transform.position += transform.rotation * Vector2.up * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(isEnemyBullet) {
            if(other.gameObject == GameManager.PlayerShip){
                other.gameObject.GetComponent<ShipControlComponent>().takeDamage(damage);
                this.Die();
            }
        } else {
            if(other.gameObject.CompareTag("Enemy")){
                other.gameObject.GetComponent<ShipControlComponent>().takeDamage(damage);
                this.Die();
            }
        }
    }


    // TODO: add animation
    public void playDieAnimation(){
        
    }

    public void Die(){
        Destroy(gameObject);
    }

}
