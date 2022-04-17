using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

// Class outlining the behavior of damage-inflicting object's behavior
public abstract class DamageBehaviour : MonoBehaviour
{
    [FormerlySerializedAs("isEnemyBullet")] public bool isFromEnemy;

    public float explosionRadius = 5;

    public float Damage { get; set; }

    public GameObject OnHitEffect { get; set; }

    protected void SetProperties(bool fromEnemy, float damage, GameObject onHitEffect)
    {
        isFromEnemy = fromEnemy;
        Damage = damage;
        OnHitEffect = onHitEffect;
    }

    protected bool DamagedShip(Collider2D other)
    {
        switch (RelationWith(other.gameObject))
        {
            case DamageRelation.EnemyToEnemy:
            case DamageRelation.PlayerToPlayer:
                return false;
            case DamageRelation.PlayerToEnemy:
                TriggerExplosion(other);
                ShockManager.Instance.StartShake(new Vector3(0, -2, 0));
                return true;
            case DamageRelation.EnemyToPlayer:
                other.gameObject.GetComponent<ShipControlComponent>()?.takeDamage(Damage);
                return true;
            default: // Default should not be reached
                return false;
        }
    }
    
    private void TriggerExplosion(Collider2D enemyCollider)
    {
        if (explosionRadius < 0) return;
        
        foreach (GameObject ship in GameManager.Instance.EnemyShips)
        {
            if ((enemyCollider.transform.position - ship.transform.position).magnitude <= explosionRadius)
            {
                ship.GetComponent<ShipControlComponent>()?.takeDamage(Damage);
            }
        }
    }

    private DamageRelation RelationWith(GameObject target)
    {
        if (isFromEnemy)
        {
            return target == GameManager.PlayerShip ? DamageRelation.EnemyToPlayer : DamageRelation.EnemyToEnemy;
        }
        return target == GameManager.PlayerShip ? DamageRelation.PlayerToPlayer : DamageRelation.PlayerToEnemy;
    }
}
