using UnityEngine;

public class Artillery : MonoBehaviour
{
    public AnimationClip _explosionClip;
    public GameObject _explosion;
    public GameObject _artilleryShadow;
    public ReturnToArtilleryPool _returnToPool;

    public Color _startShadowColor;
    public Color _endShadowColor;
    public float _startSizeScaling;
    public float _endSizeScaling;

    private Vector3 _locationToImpact;
    private SpriteRenderer _shadowSpriteRenderer;
    private float _timeToImpact;
    private float _timePassedSinceLaunch;
    private float _explosionTime;
    private bool _exploded;

    private void Start()
    {
        _shadowSpriteRenderer = _artilleryShadow.GetComponent<SpriteRenderer>();
        _artilleryShadow.SetActive(false);

        _timeToImpact = 3f;
        _timePassedSinceLaunch = 0f;
        _explosionTime = _explosionClip.length;
        _exploded = true;
    }

    private void Update()
    {
        _timePassedSinceLaunch += Time.deltaTime;

        if (_timePassedSinceLaunch >= _timeToImpact && _exploded == false)
        {
            Explode();
        }

        float percentTimeToImpact = _timePassedSinceLaunch / _timeToImpact;
        float newScale = Mathf.Lerp(_startSizeScaling, _endSizeScaling, percentTimeToImpact);
        _artilleryShadow.transform.localScale = new Vector3(newScale, newScale, 0f);

        _shadowSpriteRenderer.color = Color.Lerp(_startShadowColor, _endShadowColor, percentTimeToImpact);
    }

    private void Explode()
    {
        _exploded = true;
        _explosion.SetActive(true);
        _artilleryShadow.SetActive(false);
        Invoke("TurnOffExplosion", _explosionTime);
    }

    private void TurnOffExplosion()
    {
        ExplosionPainter._explosionPainter.PaintCrater(transform.position);
        _explosion.SetActive(false);
        _returnToPool(this);
    }

    public void LaunchArtillery(Vector2 position, float timeToImpact)
    {
        transform.position = position;
        _timeToImpact = timeToImpact;
        _timePassedSinceLaunch = 0f;
        _artilleryShadow.transform.localScale = new Vector3(_startSizeScaling, _startSizeScaling, 0f);
        _artilleryShadow.SetActive(true);
        _exploded = false;
    }
}
