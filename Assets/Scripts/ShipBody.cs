using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipBody
{
    private float _health;
    private float _speed;
    private float _colliderRadius;
    
    // _health functions
    public float getHealth()
    {
        return _health;
    }

    public void setHealth(float newHealth)
    {
        _health = newHealth;
    }

    public void incHealth(float upHealth)
    {
        _health += upHealth;
    }

    // _speed functions
    public float getSpeed()
    {
        return _speed;
    }

    public void setSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }

    public void incSpeed(float upSpeed)
    {
        _speed += upSpeed;
    }

    // _health functions
    public float getRadius()
    {
        return _colliderRadius;
    }

    public void setRadius(float newRadius)
    {
        _colliderRadius = newRadius;
    }

    public void incRadius(float upRadius)
    {
        _colliderRadius += upRadius;
    }
}
