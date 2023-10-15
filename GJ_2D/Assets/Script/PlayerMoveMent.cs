using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveMent : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private bool canJump = true;
    private bool canAttack = true;

    public float jump;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canJump && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.Keypad1) && canAttack)
        {
            // Call the function to perform the attack here
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        // For demonstration purposes, we'll just print a message.
        Debug.Log("Attacking!");

        // To prevent rapid attacks, you can add a cooldown
        StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        // Set a cooldown time (e.g., 1 second)
        yield return new WaitForSeconds(0.1f);
        canAttack = true;
    }
    private void Jump()
    {
        rb.AddForce(new Vector2(rb.velocity.x, jump));
        canJump = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Replace "Ground" with the tag of your ground objects
        {
            canJump = true;
        }
    }
}
