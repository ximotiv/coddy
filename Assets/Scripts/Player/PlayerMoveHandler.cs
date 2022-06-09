using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMoveHandler : MonoBehaviour
{
    [SerializeField] private AnimationCurve _curve;
    
    private float _speed = 10f;
    private float _currentTime, _totalTime;
    private bool _isJump;
    private bool _isPlayerFlipX = true;
    private bool _isPlayerMove = false;
    private Animator _animator;
    private AnimHash _animhash;

    private Rigidbody2D _rigidbody;
    public bool IsPlayerJump => _isJump;
    public bool IsPlayerFlipX => _isPlayerFlipX;

    private void Start()
    {
        _totalTime = _curve.keys[_curve.keys.Length-1].time;
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _animhash = new AnimHash();
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow) && !_isJump)
        {
            _isJump = true;
        }
        if (_isJump)
        {
            PlayerIsJumping();
        }

        int axis = 0;
        if (Input.GetKey(KeyCode.RightArrow))
        {

            axis = 1;
            FlipPlayer(true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            axis = -1;
            FlipPlayer(false);
        }
        
        float translation = axis * _speed;
        _rigidbody.velocity = new Vector3(translation, 0, 0);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _isPlayerMove = false;
            _animator.CrossFadeInFixedTime(_animhash.HeroIdle, 0.1f);
        }
    }

    private void PlayerIsJumping()
    {
        transform.position = new Vector3(transform.position.x, _curve.Evaluate(_currentTime), 1);
        _currentTime += Time.deltaTime;
        if (_currentTime >= _totalTime)
        {
            _currentTime = 0;
            _isJump = false;
        }
    }

    private void FlipPlayer(bool flip)
    {
        transform.localScale = new Vector3(!flip ? 0.35f : -0.35f, 0.35f, 0);
        _isPlayerFlipX = flip;
        if(!_isPlayerMove)
        {
            _animator.CrossFadeInFixedTime(_animhash.HeroRun, 0.1f);
            _isPlayerMove = true;
        }
    }
}