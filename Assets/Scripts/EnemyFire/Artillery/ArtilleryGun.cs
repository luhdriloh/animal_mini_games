using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ReturnToArtilleryPool(Artillery projectile);

public class ArtilleryGun : MonoBehaviour
{
    public GameObject _artillery;
    public float _fireRate;
    public float _maxDistanceFromPlayer;

    private Stack<Artillery> _artilleryProjectiles;
    private float _fireRateTime;

    private void Start()
    {
        _artilleryProjectiles = new Stack<Artillery>();
        _fireRateTime = 60f / _fireRate;
        AddProjectilesToPool(10);
        Launch();
    }


    private void Update()
    {

    }

    private void Launch()
    {
        Artillery artillery = GetObjectFromPool();
        artillery.LaunchArtillery(new Vector2(Random.Range(-5, 5), Random.Range(-5, 5)), 3f);
        Invoke("Launch", Random.Range(0, _fireRateTime));
    }

    // POOL FUNCTIONALITY // 

    public void HandleReturnToArtilleryPool(Artillery projectile)
    {
        _artilleryProjectiles.Push(projectile);
    }


    public Artillery GetObjectFromPool()
    {
        if (_artilleryProjectiles.Count == 0)
        {
            AddProjectilesToPool(3);
        }

        Artillery projectileToReturn = _artilleryProjectiles.Pop();
        return projectileToReturn;
    }


    private void AddProjectilesToPool(int amountToAdd)
    {
        ReturnToArtilleryPool delegateToUse = HandleReturnToArtilleryPool;

        for (int i = 0; i < amountToAdd; i++)
        {
            GameObject newGameObject = Instantiate(_artillery, transform.position, Quaternion.identity);
            Artillery projectile = newGameObject.GetComponent<Artillery>();
            projectile._returnToPool = delegateToUse;
            _artilleryProjectiles.Push(projectile);
        }
    }
}
