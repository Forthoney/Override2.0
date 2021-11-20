using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowFleeBehaviour : EnemyBehaviour
{
    private enum followState
    {
        Follow,
        Flee
    }

    private followState state;
    private bool paused;

    SpriteRenderer rend;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;
    private float radius;

    private float playerBoundRadMin;
    private float playerBoundRadMax;
    private float playerBoundRadius;
    private float pauseTimeMin;
    private float pauseTimeMax;
    private float fleeTime;

    private float Timer = 0;

    public FollowFleeBehaviour(ShipControlComponent enemyShip) : base(enemyShip)
    {
        this.enemyShip = enemyShip;

        state = followState.Follow;
        paused = false;

        var vertExtent = Camera.main.GetComponent<Camera>().orthographicSize;
        var horzExtent = vertExtent * Screen.width / Screen.height;

        rend = enemyShip.GetComponent<ShipBodySettings>().Sprite;
        minX = -horzExtent;
        maxX = horzExtent;
        minY = -vertExtent;
        maxY = vertExtent;
        radius = rend.bounds.extents.magnitude * 1.2f;

        playerBoundRadMin = 5f;
        playerBoundRadMax = 10f;
        playerBoundRadius = Random.Range(playerBoundRadMin, playerBoundRadMax);

        pauseTimeMin = 2;
        pauseTimeMax = 4;

        fleeTime = 2f;
    }

    public override void doAction()
    {
        Vector2 playerPos = GameManager.PlayerShip.transform.position;

        float xDiff, yDiff;

        xDiff = playerPos.x - enemyShip.gameObject.transform.position.x;
        yDiff = playerPos.y - enemyShip.gameObject.transform.position.y;

        Vector3 followDirection = new Vector3(xDiff, yDiff);

        // pause switching condition
        if (paused = true && Timer > pauseTimeMin)
        {
            if (Timer >= pauseTimeMax)
            {
                paused = false;
            }
            else
            {
                float stateChangeChance = Mathf.Lerp(.3f, .9f,
                    (pauseTimeMax - pauseTimeMin) / (Timer - pauseTimeMin));
                paused = Random.Range(0f, 1f) < stateChangeChance ?
                    false : true;
            }

            if (!paused)
                Timer = 0;
        }

        // Condition for Switching from follow to flee
        if (!paused && followDirection.magnitude < playerBoundRadius && state == followState.Follow)
        {
            paused = true;
            state = followState.Flee;
        }

        // Condition for switching from flee to follow
        if (!paused && state == followState.Flee && (Timer >= fleeTime ||
            playerPos.x >= maxX - radius ||
            playerPos.x <= minX + radius ||
            playerPos.y >= maxY - radius ||
            playerPos.y <= minY + radius))
        {
            paused = true;
            state = followState.Follow;
            Timer = 0;
        }

        // do the thing if not paused, make timer go brr if paused
        if (paused)
        {
            Timer += Time.deltaTime;
        }
        else
        {
            switch (state)
            {
                case followState.Follow:
                    enemyShip.gameObject.transform.position += Vector3.Normalize(followDirection) * Time.deltaTime * speed;
                    break;
                case followState.Flee:
                    enemyShip.gameObject.transform.position -= Vector3.Normalize(followDirection) * Time.deltaTime * speed;
                    Timer += Time.deltaTime;
                    break;
                default:
                    break;
            }
        }

        rotateTowardsPlayer();
        fireWeapon();
    }
}
