using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    private int attackCount;

    [SerializeField] private PlayerConfig config;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private LayerMask enemyLayer;
    [Header("Move")]
    float dirx = 0;

    [Header("Attack")]

    public Transform attackpoint;
    public float attackRange = 0.5f;


    private enum MovementState { idle, running, jumping, falling, idleattack }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

    }
    void Update()
    {
        checkMove();
        if (config.MODE_ATTACK == true)
        {
            UseSkill();
        }
    }
    public void AttackDone()
    {
        anim.SetBool("attack", false);
    }

    private void moveplayer()
    {
        dirx = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirx * config.MoveSpeed, rb.velocity.y);
    }
    private void JumpingPlayer()
    {
        if (IsGroundeds() && !Input.GetButton("Jump"))
        {
            config.doubleJump = false;
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGroundeds() || config.doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, config.JumpForce);
                config.doubleJump = !config.doubleJump;
            }
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }
    private void checkMove()
    {
        MovementState state;
        JumpingPlayer();
        moveplayer();
        if (dirx > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirx < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            if (config.MODE_ATTACK == true)
            {
                state = MovementState.idleattack;
            }
            else
            {
                state = MovementState.idle;
            }
        }
        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;

        }
        if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        anim.SetInteger("state", (int)state);
    }
    public void UseSkill()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0                                     ))
        {
            DOVirtual.DelayedCall(0.21f, () =>
            {
                attackCount++;
                if (attackCount >= 3) attackCount = 0;
                anim.SetBool("attack", true);
                anim.SetInteger("count", attackCount);
                HandleDamaged();
            });
        }
    }

    private void HandleDamaged()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("BEM");
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackpoint == null) return;
        Gizmos.DrawWireSphere(attackpoint.position, attackRange);
    }
    private bool IsGroundeds()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}