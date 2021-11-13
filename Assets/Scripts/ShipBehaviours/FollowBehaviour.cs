using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehaviour : EnemyBehaviour
{
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;
    private float radius;
    SpriteRenderer rend;



    public FollowBehaviour(ShipControlComponent enemyShip) : base(enemyShip)
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
        Vector2 playerPos = GameManager.PlayerShip.transform.position;

        float xDiff, yDiff;

        xDiff = playerPos.x - enemyShip.gameObject.transform.position.x;
        yDiff = playerPos.y - enemyShip.gameObject.transform.position.y;

        Vector3 followDirection = new Vector3(xDiff, yDiff);
        enemyShip.gameObject.transform.position += Vector3.Normalize(followDirection) * Time.deltaTime * speed;

        rotateTowardsPlayer();
        fireWeapon();
    }
}