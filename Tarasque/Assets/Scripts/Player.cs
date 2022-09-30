using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    float moveH;
    float jumpHeight = 8;
    int jumpCounter;
    int jumpCounterMax = 2;
    bool jump;
    bool isJumping;
    bool isAttacking = false;
    bool facingRight = true;
    bool shootingProjectile = false;
    

    [SerializeField] GameObject tackleHitbox;
    [SerializeField] int magicMax;
    [SerializeField] int magic;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private GameUI magicBar;

    //TODO list:
    //Make character input
    //  -movement        -done
    //  -tackling        -done
    //  -projectiles     -done
    //  -UI              -done
    //      -health      -done
    //      -magic       -done

    // Start is called before the first frame update
    void Start()
    {
        tackleHitbox.SetActive(false);
        getHealthBar().Setup(getMaxHP(), getHP());
        magicBar.Setup(getMaxHP(), getHP());
    }

    // Update is called once per frame
    void Update()
    {
        //takes the horizontal input
        moveH = Input.GetAxis("Horizontal");

        //determines if player is facing the right or not
        if(moveH > 0)
        {
            facingRight = true;
        }
        else if(moveH < 0)
        {
            facingRight = false;
        }

        //jumping
        if(Input.GetButtonDown("Jump") && (IsGrounded() || jumpCounter > 0))
        {
            jump = true;
        }

        //player presses e to attack (tackle)
        if (Input.GetKeyDown(KeyCode.E))
        {
            isAttacking = true;

        }

        //player presses r to attack (projectile)
        if (Input.GetKeyDown(KeyCode.R))
        {
            shootingProjectile = true;
        }



    }

    private void FixedUpdate()
    {
        //for flipping the sprite around
        if(facingRight)
        {
            this.transform.eulerAngles = new Vector2(0, 0);
        }
        else
        {
            this.transform.eulerAngles = new Vector2(0, 180);
        }

        //Shooting attack
        if(shootingProjectile)
        {
            Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation).GetComponent<Projectile>().setOwner(this);
            shootingProjectile = false;
        }

        //left or right movement
        rb.velocity = new Vector2(moveH * getSpeed(), rb.velocity.y);

        //jumps
        if(jump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            jumpCounter--;
            jump = false;
        }

        //Tackle attack
        if(isAttacking)
        {
            Tackle();
            isAttacking = false;
        }
    }

    private bool IsGrounded()
    {
        if(Physics2D.OverlapCircle(groundCheck.position, .2f, groundLayer))
        {
            jumpCounter = jumpCounterMax;
            return true;
        }
        return false;
    }

    private void Tackle()
    {
        tackleHitbox.SetActive(true);
        tackleHitbox.GetComponent<TackleHitBox>().TackeDuration();
    }
}
