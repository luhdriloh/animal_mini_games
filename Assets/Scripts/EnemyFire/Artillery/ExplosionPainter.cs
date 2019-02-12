using UnityEngine;

public class ExplosionPainter : MonoBehaviour
{
    public static ExplosionPainter _explosionPainter;
    public GameObject _craterPrototype;
    public int _numberOfExplosionsToTrack;

    private GameObject[] _craters;
    private int _indexOfCurrentExplosion;

	private void Start ()
    {
        if (_explosionPainter == null)
        {
            _explosionPainter = this;
            InitializeCraters();
        }
        else
        {
            Destroy(this);
        }
	}

    private void InitializeCraters()
    {
        _craters = new GameObject[_numberOfExplosionsToTrack];
        for (int i = 0; i < _numberOfExplosionsToTrack; i++)
        {
            _craters[i] = Instantiate(_craterPrototype, transform.position, Quaternion.identity);
            _craters[i].SetActive(false);
        }

        _indexOfCurrentExplosion = 0;
    }

    public void PaintCrater(Vector3 position)
    {
        _craters[_indexOfCurrentExplosion].transform.position = position;
        _craters[_indexOfCurrentExplosion].transform.eulerAngles = new Vector3(0f, 0f, Random.Range(0, 360));
        _craters[_indexOfCurrentExplosion].SetActive(true);

        _indexOfCurrentExplosion = (_indexOfCurrentExplosion + 1) % _numberOfExplosionsToTrack;
    }
}
