using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour
{

    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(250f, 250f);

    bool isAlive = true;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private CapsuleCollider2D _bodyCollider;
    private BoxCollider2D _feetCollider;
    private float _gravityScaleAtStart;
    //private GameObject _player;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _bodyCollider = GetComponent<CapsuleCollider2D>();
        _feetCollider = GetComponent<BoxCollider2D>();
        _gravityScaleAtStart = _rigidbody2D.gravityScale;
        //_player = GetComponent<GameObject>();

    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive) { return; }
        Run();
        Jump();
        Climb();
        Die();
        FlipSprite();
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); //wartoœæ od -1 do 1
        bool playerHasHorizontalSpeed = Mathf.Abs(_rigidbody2D.velocity.x) > Mathf.Epsilon;

        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, _rigidbody2D.velocity.y);
        _rigidbody2D.velocity = playerVelocity;

        _animator.SetBool("Running", playerHasHorizontalSpeed);

    }

    private void Jump()
    {
        if(!_feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            _rigidbody2D.velocity += jumpVelocityToAdd;
        }
    }
    private void Climb()
    {
        if (!_feetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
        {  
           _animator.SetBool("Climb", false);
           _rigidbody2D.gravityScale = _gravityScaleAtStart;
            return;
        }

        float climbControl = CrossPlatformInputManager.GetAxis("Vertical");
        bool playerHasVerticalSpeed = Mathf.Abs(_rigidbody2D.velocity.y) > Mathf.Epsilon;
        Vector2 climbVelocityToAdd = new Vector2(_rigidbody2D.velocity.x, climbControl * climbSpeed);
        _rigidbody2D.velocity = climbVelocityToAdd;
        _rigidbody2D.gravityScale = 0f;
        _animator.SetBool("Climb", playerHasVerticalSpeed);
    }
    private void Die()
    {
        if(_bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            _animator.SetTrigger("Die");
            GetComponent<Rigidbody2D>().velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(_rigidbody2D.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(_rigidbody2D.velocity.x), 1);
        }
    }
}
