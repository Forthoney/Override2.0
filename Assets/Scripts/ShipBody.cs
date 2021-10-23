using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class which represents the body of a ship
public abstract class ShipBody
{
    // Backing fields
    protected float _maxHealth;
    protected float _currHealth;
    protected float _speed;
    protected float _colliderRadius;
	protected float _accelerationLambda = 8f;
 
    // Constructor
    protected ShipBody(float h, float s, float cr, float accelerationLambda)
    {
        _maxHealth = h;
        _currHealth = h;
        _speed = s;
        _colliderRadius = cr;
		_accelerationLambda = accelerationLambda;
    }

    // Accessors
    public float MaxHealth { 
        get => _maxHealth;
        set => _maxHealth = value;
    }
    public float CurrHealth { 
        get => _currHealth;
        set => _currHealth = value;
    }
    public float Speed { 
        get => _speed;
        set => _speed = value;
    }
	public float AccelerationLambda { 
		get => _accelerationLambda;
		set => _accelerationLambda = value;
	}

    // Important!
    public void move()
    {
		Vector2 vel = GameManager.PlayerShip.GetComponent<Rigidbody2D>().velocity;
        GameManager.PlayerShip.GetComponent<Rigidbody2D>().velocity 
			= Vector2.Lerp(vel,InputController.Instance.Movement*_speed,1-Mathf.Exp(-AccelerationLambda*Time.deltaTime));
    }
   }
