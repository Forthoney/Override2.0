using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BasicBehaviour : EnemyBehaviour
{
  private float minX;
  private float maxX;
  private float minY;
  private float maxY;
  private float radius;
  SpriteRenderer rend;

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
    rend = enemyShip.GetComponent<ShipBodySettings>().Sprite;

    // A sphere that fully encloses the bounding box.
    radius = rend.bounds.extents.magnitude * 1.2f;
  }

  public override void doAction()
  {
    Vector2 playerPos = enemyShip.gameObject.transform.position;

    float topDist, botDist, leftDist, rightDist;

    rightDist = Mathf.Abs(playerPos.x - maxX);
    leftDist = Mathf.Abs(playerPos.x - minX);
    botDist = Mathf.Abs(playerPos.y - maxY);
    topDist = Mathf.Abs(playerPos.y - minY);

    // Fly into view once spaawned outside of camera
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
    // Fly around the edge of the view
    else
    {
      float min = Mathf.Min(Mathf.Min(rightDist, leftDist), Mathf.Min(topDist, botDist));

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
    fireWeapon();
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
}
