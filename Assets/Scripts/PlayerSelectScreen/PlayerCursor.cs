using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    public PlayerSelectArea _playerSelectArea;
    public Color _cursorColor;
    public int _playerNumber;
    public float _speed;
    public string _xAxis;
    public string _yAxis;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidBody;
    private Vector2 _newPosition;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = _cursorColor;
        _rigidBody = GetComponent<Rigidbody2D>();

        _xAxis = PlayersPlayingInformation._playerPlayingInformation._players[_playerNumber]._xAxis;
        _yAxis = PlayersPlayingInformation._playerPlayingInformation._players[_playerNumber]._yAxis;
    }

    private void Update()
    {
        // get axis for sniper dude and move him about
        Vector2 movement = new Vector2(Input.GetAxis(_xAxis), Input.GetAxis(_yAxis)).normalized * _speed;
        _newPosition = transform.position + (Vector3)movement;
    }

    private void FixedUpdate()
    {
        _rigidBody.MovePosition(new Vector2(_newPosition.x, _newPosition.y));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RuntimeAnimatorController controller = collision.GetComponent<Character>()._playerController;
        _playerSelectArea.SetAnimationController(controller);

        PlayersPlayingInformation._playerPlayingInformation._players[_playerNumber]._characterSelectedController = controller;
    }
}
