using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.RestService;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    public Enemy enemy;

    public float health = 4;

    private float moveX;
    private float moveY;

    private bool canTakeDamage = true;
    public float damageCooldown = 1f;

    public GameObject deathScreen;


    public GameObject grave;

    public Animator redOverlayAnimator;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Movement();
        
    }

    private void Movement()
    {
        

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // We need to place these 2 values somewhere, make a Vector2 box
        // move is a box that holds 2 nums, horizontal and vertical movement (Vector2)
        Vector2 move = new Vector2(moveX, moveY).normalized;

        rb.velocity = move * speed;

        animator.SetFloat("Horizontal", moveX);
        animator.SetFloat("Vertical", moveY);

        // Flip sprite based on left and right since same anim
        if (moveX > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveX < 0)
        {
            spriteRenderer.flipX = true;
        }


    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && canTakeDamage)
        {
            StartCoroutine(DamageCooldown());
        }
    }

    private IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        TakeDamagePlayer();
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }




    public void TakeDamagePlayer()
    {
        
        health -= 1;
        

        if (health <= 0)
        {
            ScatterYourSorrowsToTheHeartlessWorld();
        }

        // Moving right
        if (moveX > 0)
        {
            animator.SetTrigger("PlayerDmgWalk");

        }

        else if (moveX < 0)
        {
            animator.SetTrigger("PlayerDmgWalk");
        }

        else
        {
            animator.SetTrigger("PlayerDmgIdle");
        }

        }

    private void ScatterYourSorrowsToTheHeartlessWorld()
    {
        redOverlayAnimator.Play("DeathScreen");
        Destroy(gameObject);
        Instantiate(grave, transform.position, transform.rotation);
    }

    
    
       
    


}
