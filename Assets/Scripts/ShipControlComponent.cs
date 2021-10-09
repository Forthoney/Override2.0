using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControlComponent : MonoBehaviour
{
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
        if (currentHealth < 0) {
            GameManager.Instance.ShipDestroy(gameObject);
        }

        Color tint = new Color(1, currentHealth/maxHealth, currentHealth/maxHealth);


        gameObject.GetComponent<SpriteRenderer>().color = tint;
    }

    public void takeDamage(float damage){
        currentHealth -= damage;
    }

}
