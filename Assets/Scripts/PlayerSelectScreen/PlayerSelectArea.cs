using UnityEngine;

public class PlayerSelectArea : MonoBehaviour
{
    public SpriteRenderer _background;
    public Color _playerColor;
    public bool _doneSelecting;
    public RuntimeAnimatorController _playerAnimationController;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _background.color = _playerColor;
    }

    public void SetAnimationController(RuntimeAnimatorController controller)
    {
        _playerAnimationController = controller;
        _animator.runtimeAnimatorController = controller;
    }
}
