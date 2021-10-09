using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControlComponent : MonoBehaviour
{

    private ShipBody _shipBody;
    private ShipWeapon _shipWeapon;

    private EnemyBehaviour _enemyBehaviour;




    public float maxHealth;
    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }

        Color tint = new Color(1, currentHealth/maxHealth, currentHealth/maxHealth);


        gameObject.GetComponent<SpriteRenderer>().color = tint;
        
        if (GameManager.playerShip != this.gameObject) {    
            _enemyBehaviour.doAction();
        }
    }

    public void takeDamage(float damage){
        currentHealth -= damage;
    }

    public void hijackShip(GameObject shipToHijack){
        
    }

    public void setWeapon(ShipWeapon newWeapon){
        _shipWeapon = newWeapon;
    }

    public void setBody(ShipBody newBody){
        _shipBody = newBody;
    }

    public ShipWeapon getWeapon(){
        return _shipBody;
    }

    public ShipBody getBody(){
        return _shipBody;
    }

}
