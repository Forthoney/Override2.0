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
	private float _accelerationLambda = 8f;
	public float AccelerationLambda { 
		get=> _accelerationLambda;
		set => _accelerationLambda = value;
	}

    public void move(){
		Vector2 vel = GameManager.PlayerShip.GetComponent<Rigidbody2D>().velocity;
        GameManager.PlayerShip.GetComponent<Rigidbody2D>().velocity 
			= Vector2.Lerp(vel,InputController.Instance.Movement*_speed,1-Mathf.Exp(-AccelerationLambda*Time.deltaTime));
    }
    
    // Constructor
    protected ShipBody(float h, float s, float cr, float accelerationLambda) {
        _maxHealth = h;
        _currHealth = h;
        _speed = s;
        _colliderRadius = cr;
		_accelerationLambda = accelerationLambda;
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
}
