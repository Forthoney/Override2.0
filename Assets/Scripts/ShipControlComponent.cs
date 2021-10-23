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
	public float CurrentHealth {
		get => currentHealth;
	}

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth < 0) {
            GameManager.Instance.ShipDestroy(gameObject);
        }

        Color tint = new Color(1, currentHealth/maxHealth, currentHealth/maxHealth);


        gameObject.GetComponent<SpriteRenderer>().color = tint;

        if (GameManager.PlayerShip != this.gameObject) {
            _enemyBehaviour?.doAction();
        }
    }

    public void takeDamage(float damage){
        currentHealth -= damage;
    }

    public void hijackShip(GameObject shipToHijack){

    }

    public void setWeapon(ShipWeaponType weaponType){
        switch(weaponType) {
            case ShipWeaponType.BulletWeapon:
                _shipWeapon = new BulletWeapon("Prefabs/Bullet", new Vector2(0, 0), gameObject);
                break;
            default:
                _shipWeapon = new LaserWeapon("Prefabs/Bullet", new Vector2(0, 0), gameObject);
                break;
        }
    }

    public void setBody(ShipBodyType bodyType){
        switch(bodyType) {
            case ShipBodyType.FastSquish:
                _shipBody = new FastSquish();
                break;
            default:
                _shipBody = new SlowChunk();
                break;
        }
    }

    public ShipWeapon getWeapon(){
        return _shipWeapon;
    }

    public ShipBody getBody(){
        return _shipBody;
    }

    public EnemyBehaviour getEnemyBehaviour() {
      return _enemyBehaviour;
    }

    public void setEnemyBehaviour(EnemyBehaviour newBehaviour) {
      _enemyBehaviour = newBehaviour;
    }

}
