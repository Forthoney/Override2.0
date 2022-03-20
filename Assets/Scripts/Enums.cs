using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShipBodyType {
    SlowChunk,
    FastSquish
}

public enum ShipWeaponType {
    BulletWeapon,
    LaserWeapon
}

public enum CameraWallNum {
    North,
    East,
    South,
    Weest
}

public enum DamageRelation
{
    EnemyToPlayer,
    EnemyToEnemy,
    PlayerToEnemy,
    PlayerToPlayer
} 