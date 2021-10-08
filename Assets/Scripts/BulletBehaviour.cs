using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    // Set by the ship weapon after spawn
    public float speed;

    public float damage;

    public string whatShouldIDamage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += transform.rotation * Vector2.up * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit " + other.gameObject.name);

        if(other.gameObject.CompareTag(whatShouldIDamage)){
            Debug.Log("Hit an enemy");
            other.gameObject.GetComponent<ShipControlComponent>().takeDamage(damage);
            this.Die();
        }
    }


    // TODO: add animation
    public void playDieAnimation(){
        
    }

    public void Die(){
        Destroy(gameObject);
    }

    public void setSpeed(){}

    public void setDamage(){}

}
