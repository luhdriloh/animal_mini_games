using UnityEngine;

public class EnemyTurret : Shooter
{
    private static Transform _playerTransform;
    private bool _rightFacing;

    protected override void Start()
    {
        base.Start();
        _playerTransform = GameObject.Find("Player").transform;
        Invoke("FireWeaponAtPlayer", 10);
    }


    public void FireWeaponAtPlayer()
    {
        // if player not in view return, or not within a set distance

        // shoot directly at player
        Vector3 direction = _playerTransform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        FireWeapon(angle);
        Invoke("FireWeaponAtPlayer", _fireDelay);
    }


    private void Update()
    {
        if (transform.right.x <= 0 && _rightFacing == true)
        {
            transform.localScale = new Vector3(1, -1, 0);
            _rightFacing = false;
        }
        else if (transform.right.x > 0 && _rightFacing == false)
        {
            transform.localScale = new Vector3(1, 1, 0);
            _rightFacing = true;
        }


        RotateEnemyTowardsPlayer(_playerTransform);
    }



    private void RotateEnemyTowardsPlayer(Transform target)
    {
        // get target position
        Vector3 direction = target.position - transform.position;

        // get player position, move and rotate towards that position
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // rotate toward target
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }
}
