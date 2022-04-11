using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBulletBehaviour : BulletBehaviour
{

  public float turnSpeed = 20;
  public float homingRadius = 30;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    // find the closest ship


    if (GameManager.Instance.EnemyShips.Count > 0)
    {
      Transform closest = GameManager.Instance.EnemyShips[0].transform;

      if (isFromEnemy)
      {
        closest = GameManager.PlayerShip.transform;
      }
      else
      {
        foreach (GameObject o in GameManager.Instance.EnemyShips)
        {
          if (Vector2.Distance(transform.position, o.transform.position) < Vector2.Distance(transform.position, closest.transform.position))
          {
            closest = o.transform;
          }
        }
      }

      // if the ship is close enough
      if (Vector2.Distance(transform.position, closest.position) < homingRadius)
        // Turn towards the closest ship with turn speed
        rotateTowardsWorldPos(gameObject, closest.position);

    }

    transform.position += transform.right * Speed * Time.deltaTime;

  }
  void rotateTowardsWorldPos(GameObject obj, Vector2 worldPos)
  {
    // Get target direction and angle
    Vector2 currPos = obj.GetComponent<Rigidbody2D>().position;
    Vector2 targetDirection = (worldPos - currPos).normalized;
    float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
    // Enemies have capped rotation speed
    Quaternion currRot = obj.transform.rotation;
    Quaternion newRot = Quaternion.Euler(0, 0, angle);
    obj.transform.rotation = Quaternion.RotateTowards(currRot, newRot, Time.deltaTime * turnSpeed);
  }

}
