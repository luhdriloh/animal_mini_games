using UnityEngine;

public delegate void HealthChangeHandler(int newHealthValue, int maxHealthValue);

public class TopDownPlayerController : MonoBehaviour
{
    public event HealthChangeHandler _healthChangedEvent;
    public int _health;

    private Vector2 _newPosition;
    private bool _rightFacing;
    private bool _flip;
    private bool _readyToMove;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _newPosition = transform.position;
        _readyToMove = true;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(horizontalMovement) <= Mathf.Epsilon && Mathf.Abs(verticalMovement) <= Mathf.Epsilon)
        {
            _readyToMove = true;
            return;
        }

        if (_readyToMove == false)
        {
            return;
        }

        if (horizontalMovement < -Mathf.Epsilon)
        {
            if (_rightFacing)
            {
                _rightFacing = false;
                _flip = true;
            }

            _newPosition += Vector2.left;
        }
        else if (verticalMovement > Mathf.Epsilon)
        {
            _newPosition += Vector2.up;
        }
        else if (horizontalMovement > Mathf.Epsilon)
        {
            if (_rightFacing == false)
            {
                _rightFacing = true;
                _flip = true;
            }
            _newPosition += Vector2.right;
        }
        else if (verticalMovement < -Mathf.Epsilon)
        {
            _newPosition += Vector2.down;
        }

        if (_flip)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 0f);
            _flip = false;
        }

        _readyToMove = false;
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_newPosition);
    }

    public void AddHealthChangeSubscriber(HealthChangeHandler healthChangeHandler)
    {
        _healthChangedEvent += healthChangeHandler;
    }


    protected virtual void OnHealthChange()
    {
        if (_healthChangedEvent != null)
        {
        }
    }
}
