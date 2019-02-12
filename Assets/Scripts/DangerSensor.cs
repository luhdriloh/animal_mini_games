using UnityEngine;

public class DangerSensor : MonoBehaviour
{
    public Color _dangerColor;

    private SpriteRenderer _spriteRenderer;
    private int _numSensors;
	
	public void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _numSensors = 0;
	}

    private void OnTriggerEnter2D()
    {
        _numSensors++;
        _spriteRenderer.color = _dangerColor;
    }

    private void OnTriggerExit2D()
    {
        _numSensors--;

        if (_numSensors <= 0)
        {
            _spriteRenderer.color = new Color(1, 1, 1, 0);
        }
    }
}
