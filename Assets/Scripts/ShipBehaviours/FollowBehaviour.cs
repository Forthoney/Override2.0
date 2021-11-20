using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehaviour : EnemyBehaviour
{
    private enum followState
    {
        Follow,
        Pause
    }

    private followState state;

    private float playerBoundRadMin;
    private float playerBoundRadMax;
    private float playerBoundRadius;
    private float pauseTimer = 0;
    private float pauseTimeMin;
    private float pauseTimeMax;


    public FollowBehaviour(ShipControlComponent enemyShip) : base(enemyShip)
    {
        this.enemyShip = enemyShip;

        state = followState.Follow;

        playerBoundRadMin = 5f;
        playerBoundRadMax = 10f;
        playerBoundRadius = Random.Range(playerBoundRadMin, playerBoundRadMax);

        pauseTimeMin = 2;
        pauseTimeMax = 4;
    }

    public override void doAction()
    {
        Vector2 playerPos = GameManager.PlayerShip.transform.position;

        float xDiff, yDiff;

        xDiff = playerPos.x - enemyShip.gameObject.transform.position.x;
        yDiff = playerPos.y - enemyShip.gameObject.transform.position.y;

        Vector3 followDirection = new Vector3(xDiff, yDiff);

        if (state == followState.Pause && pauseTimer > pauseTimeMin)
        {
            if (pauseTimer >= pauseTimeMax)
                state = followState.Follow;
            else
            {
                float stateChangeChance = Mathf.Lerp(.3f, .9f,
                    (pauseTimeMax - pauseTimeMin)/(pauseTimer-pauseTimeMin));
                state = Random.Range(0f, 1f) < stateChangeChance ?
                    followState.Follow : followState.Pause;
            }

            if (state == followState.Follow)
            {
                pauseTimer = 0;
            }
        }

        if (followDirection.magnitude < playerBoundRadius && state == followState.Follow)
        {
            state = followState.Pause;
        }

        switch (state)
        {
            case followState.Follow:
                enemyShip.gameObject.transform.position += Vector3.Normalize(followDirection) * Time.deltaTime * speed;
                break;
            case followState.Pause:
                pauseTimer += Time.deltaTime;
                break;
            default:
                break;
        }

        rotateTowardsPlayer();
        fireWeapon();
    }
}