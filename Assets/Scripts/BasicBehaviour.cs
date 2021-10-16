using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BasicBehaviour : EnemyBehaviour
{

    private double minX;
    private double maxX;
    private double minY;
    private double maxY;
    private double mapX = 30.0;
    private double mapY = 30.0;
    private Vector3 radius;
    SpriteRenderer rend;

    public BasicBehaviour(ShipControlComponent enemyShip) : base(enemyShip) {
      this.enemyShip = enemyShip;

      // var vertExtent = Camera.main.GetComponent<Camera>().orthographicSize;
      // var horzExtent = vertExtent * Screen.width / Screen.height;
      //
      //    // Calculations assume map is position at the origin
      // minX = horzExtent - mapX / 2.0;
      // maxX = mapX / 2.0 - horzExtent;
      // minY = vertExtent - mapY / 2.0;
      // maxY = mapY / 2.0 - vertExtent;
      rend = enemyShip.gameObject.GetComponent<SpriteRenderer>();
      // A sphere that fully encloses the bounding box.
      radius = rend.bounds.extents;

      minX = -10;
      maxX = 10;
      minY = -5;
      maxY = 5;

    }

    public override void doAction()
    {



      if (enemyShip.gameObject.transform.position.x < (maxX - radius.x))
      {
        enemyShip.gameObject.transform.position += new Vector3(Time.deltaTime * 10, 0, 0);
      }
      // } elif (enemyShip.gameObject.transform.position.y) {
      //
      //   enemyShip.gameObject.transform.position += new Vector3(0, Time.deltaTime * 10, 0);
      // }
    }
}
