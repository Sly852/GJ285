using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private BoxCollider2D playerCollider;

    public Animator animator;
    public Transform attackPoint;
    public LayerMask bulletLayers;
    public float jumpForce = 10.5f;
    public float attackRange = 0.5f;

    private GameObject currentPlatform;
    private Rigidbody2D rb;
    private bool canJump = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Attack();
        }
        if (canJump && Input.GetKeyDown(KeyCode.Z))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (currentPlatform != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }

    void Attack()
    {
        //Play an attack animation
        animator.SetTrigger("Attack");

        //Detect enmies in range of attack
        Collider2D[] hitBullet = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, bulletLayers);
    
        foreach(Collider2D bullet in hitBullet)
        {
            Debug.Log("We hit" + bullet.name);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            currentPlatform = null;
        }
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = currentPlatform.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(1.0f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        canJump = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            canJump = true;
        }
        if (collision.gameObject.CompareTag("Platform"))
        {
            currentPlatform = collision.gameObject;
        }
    }
}

