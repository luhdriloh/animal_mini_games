using System.Collections.Generic;
using UnityEngine;

using ActionGameFramework.Projectiles;
using ActionGameFramework.Helpers;

public class ProjectileSpawner : Shooter
{
    public List<Transform> _firepoints;

    protected override void Start()
    {
        _firepointGameObject = this.gameObject;
        base.Start();
        
        _fireDelay = 1f / _weaponProjectile.GetComponent<LinearProjectile>()._startSpeed;
        FireWeapon();
    }

    public void FireWeapon()
    {
        transform.position = _firepoints[Random.Range(0, _firepoints.Count)].position;
        float angle = Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg;
        FireWeapon(angle);
        Invoke("FireWeapon", _fireDelay);
    }
}
