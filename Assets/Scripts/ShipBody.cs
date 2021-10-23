using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class which represents the body of a ship
public abstract class ShipBody
{
    // Backing fields
    private float _maxHealth;
    private float _currHealth;
    private float _speed;
    private float _colliderRadius;
    // Note: Might potentially hold the sprite as well
    
    // Constructor
    protected ShipBody(float h, float s, float cr) {
        _maxHealth = h;
        _currHealth = h;
        _speed = s;
        _colliderRadius = cr;
    }

    // Accessors
    public float MaxHealth { 
        get { return _maxHealth; }
    }
    public float CurrHealth { 
        get { return _currHealth; }
        set { _currHealth = value; }
    }
    public float Speed { 
        get { return _speed; }
    }
    
    // Called by the player's ship controller; uses player input to move ship
    public void move()
    {
        GameManager.PlayerShip.GetComponent<Rigidbody2D>().velocity =
            InputController.Instance.Movement * _speed;
    }
}
