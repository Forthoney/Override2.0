using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BasicBehaviour : EnemyBehaviour
{

  private float minX;
  private float maxX;
  private float minY;
  private float maxY;
  private float mapX = 30.0f;
  private float mapY = 30.0f;
  private float radius;
  SpriteRenderer rend;
  float speed = 5;
  float attackSpeed = 0.5f;
  float timer = 0;

  public BasicBehaviour(ShipControlComponent enemyShip) : base(enemyShip)
  {
    this.enemyShip = enemyShip;

    var vertExtent = Camera.main.GetComponent<Camera>().orthographicSize;
    var horzExtent = vertExtent * Screen.width / Screen.height;

    // Calculations assume map is position at the origin
    minX = -horzExtent;
    maxX = horzExtent;
    minY = -vertExtent;
    maxY = vertExtent;
    rend = enemyShip.gameObject.GetComponent<SpriteRenderer>();
    // A sphere that fully encloses the bounding box.
    radius = rend.bounds.extents.magnitude;

  }

  public override void doAction()
  {

    // FOLLOWS PLAYER BEHAVIOR
    // Vector3 playerShip = GameManager.PlayerShip.transform.position;
    // Vector3 enemyToOrigin = playerShip - enemyShip.gameObject.transform.position;
    // float angle = Mathf.Atan2(enemyToOrigin.y, enemyToOrigin.x) * Mathf.Rad2Deg;
    // enemyShip.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    // enemyShip.gameObject.transform.position += 0.03f * Vector3.Normalize(enemyToOrigin);

    Vector2 playerPos = enemyShip.gameObject.transform.position;

    float topDist, botDist, leftDist, rightDist;

    rightDist = Mathf.Abs(playerPos.x - maxX);
    leftDist = Mathf.Abs(playerPos.x - minX);
    botDist = Mathf.Abs(playerPos.y - maxY);
    topDist = Mathf.Abs(playerPos.y - minY);

    if (playerPos.x > maxX - radius)
    {
      moveLeft();
    }
    else if (playerPos.x < minX + radius)
    {
      moveRight();
    }
    else if (playerPos.y < minY + radius)
    {
      moveDown();
    }
    else if (playerPos.y > maxY - radius)
    {
      moveUp();
    }
    else
    {

      float min = Mathf.Min(Mathf.Min(rightDist, leftDist), Mathf.Min(topDist, botDist));

      // TODO: more robust movement that doesnt assume the enemies are spawned on the edge 
      if (min == rightDist)
      {
        moveDown();
      }
      else if (min == leftDist)
      {
        moveUp();
      }
      else if (min == botDist)
      {
        moveLeft();
      }
      else if (min == topDist)
      {
        moveRight();
      }
    }

    rotateTowardsPlayer();

    if (timer >= 1 / attackSpeed)
    {
      enemyShip.ShipWeapon.Fire(true);
      timer = 0;
    }
    else
    {
      timer += Time.deltaTime;
    }

  }

  private void moveRight()
  {
    enemyShip.gameObject.transform.position += new Vector3(1, 0) * Time.deltaTime * speed;
  }

  private void moveLeft()
  {
    enemyShip.gameObject.transform.position += new Vector3(-1, 0) * Time.deltaTime * speed;
  }

  private void moveUp()
  {
    enemyShip.gameObject.transform.position += new Vector3(0, -1) * Time.deltaTime * speed;

  }

  private void moveDown()
  {
    enemyShip.gameObject.transform.position += new Vector3(0, 1) * Time.deltaTime * speed;
  }

  private void rotateTowardsPlayer()
  {
    Vector3 thisToPlayer = GameManager.PlayerShip.transform.position - enemyShip.gameObject.transform.position;
    float angle = Mathf.Atan2(thisToPlayer.y, thisToPlayer.x) * Mathf.Rad2Deg;
    enemyShip.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
  }


}
