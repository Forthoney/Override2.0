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
    Aim(isFromEnemy, _firingSource.transform.parent.gameObject);
  }

  private async void Aim(bool isFromEnemy, GameObject firingSource)
  {
    if (isFromEnemy)
    {
      firingSource.GetComponent<ShipControlComponent>().EnemyBehaviour.AdjustSpeed(slowDownMagnitude);
    }
    else
    {
      firingSource.GetComponent<ShipControlComponent>().ShipBody.AdjustSpeed(slowDownMagnitude * 0.5f);
    }
    GameObject beam = Instantiate(_bulletPrefab, _firingSource.transform.position, _firingSource.transform.rotation);
    HitscanBehaviour beamBehaviour = beam.GetComponent<HitscanBehaviour>();
    beamBehaviour.SetProperties(isFromEnemy, _damage, ShootEffectHitPrefab);

    // Wait twice as long if enemy firing
    if (isFromEnemy)
      await Task.Delay(TimeSpan.FromSeconds(aimDuration));
    await Task.Delay(TimeSpan.FromSeconds(aimDuration));

    if (isFromEnemy)
    {
      if (firingSource != null)
        firingSource.GetComponent<ShipControlComponent>().EnemyBehaviour.AdjustSpeed(1f / slowDownMagnitude);
    }
    else
    {
      firingSource.GetComponent<ShipControlComponent>().ShipBody.AdjustSpeed(1f / (slowDownMagnitude * 0.5f));
    }
    beamBehaviour.Fire();
    beam.GetComponent<LineRenderer>().endColor = Color.red;
    beam.GetComponent<LineRenderer>().startColor = Color.red;
    beam.GetComponent<ParticleSystem>().Stop();
    beam.GetComponent<ParticleSystem>().Clear();
    beam.GetComponent<ParticleSystem>().Play();
    await Task.Delay(TimeSpan.FromSeconds(0.3f));
    Destroy(beam);

  }
}