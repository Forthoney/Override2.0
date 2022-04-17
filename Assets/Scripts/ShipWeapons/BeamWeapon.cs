using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "BeamWeapon", menuName = "~/Combat/Weapons/BeamWeapon", order = 4)]
public class BeamWeapon : ShipWeapon
{
    [SerializeField] private float step = 10f;
    [SerializeField] private float aimDuration = 0.4f;
    [SerializeField] private float slowDownMagnitude = 0.6f;
    public override void Fire(bool isFromEnemy)
    {
        base.Fire(isFromEnemy);
        Aim(isFromEnemy);

        GameObject beam = Instantiate(_bulletPrefab, _firingSource.transform.position, _firingSource.transform.rotation);
        HitscanBehaviour beamBehaviour = beam.GetComponent<HitscanBehaviour>();
        beamBehaviour.SetProperties(isFromEnemy, _damage, ShootEffectHitPrefab);
        beamBehaviour.Fire();
    }
    
    private async void Aim(bool isFromEnemy)
    {
        if (isFromEnemy)
        {
            _firingSource.GetComponent<ShipControlComponent>().EnemyBehaviour.AdjustSpeed(slowDownMagnitude);
        }
        else
        {
            _firingSource.GetComponent<ShipControlComponent>().ShipBody.AdjustSpeed(slowDownMagnitude * 0.5f);
        }
        RenderAimpoint();
        await Task.Delay(TimeSpan.FromSeconds(aimDuration));
        
        if (isFromEnemy)
        {
            _firingSource.GetComponent<ShipControlComponent>().EnemyBehaviour.AdjustSpeed(1f/slowDownMagnitude);
        }
        else
        {
            _firingSource.GetComponent<ShipControlComponent>().ShipBody.AdjustSpeed(1f/(slowDownMagnitude * 0.5f));
        }
    }
    
    //TODO: add a laser point or something to indicate where the weapon is aiming
    private void RenderAimpoint()
    {
        return;
    }
}