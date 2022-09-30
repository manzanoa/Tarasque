using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBoss : Character
{
    [SerializeField] private float jumpHeight;
    [SerializeField] GameObject projectile, projectileDown;
    [SerializeField] Transform projectileSpawnPoint;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Text text;

    [SerializeField] Transform shootDownL, shootDownR;


    private bool jumpingShooting = false;

    //TODO list
    //-AI
    //    -random attacks
    //        -physical attacks
    //        -projectiles
    //        -teleporting
    //        -animations
    //-UI                           -done
    //    -health                   -done
    //-





    // Start is called before the first frame update
    void Awake()
    {
        getHealthBar().Setup(getMaxHP(), getHP());
        StartCoroutine(AttackSystem());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator AttackSystem()
    {
        yield return new WaitForSeconds(.5f);


        while(!this.isDead)
        {
            int rand = Random.Range(0, 3);
            //rand = 2; //for testing purposes

            switch (rand)
            {
                case 0:
                    //do something
                    text.text = "Shoot";
                    yield return new WaitForSeconds(.3f);
                    GameObject x = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
                    x.GetComponent<SpriteRenderer>().color = Color.gray;
                    x.GetComponent<Projectile>().setOwner(this);
                    break;
                case 1:
                    text.text = "Jump Shoot";
                    yield return new WaitForSeconds(.3f);
                    yield return StartCoroutine(JumpAndShoot());
                    break;
                //add more cases
                case 2:
                    text.text = "Shoot Down";
                    yield return new WaitForSeconds(.3f);
                    yield return StartCoroutine(ShootDown());
                    break;

            }

            yield return new WaitForSeconds(2.5f);
        }
    }

    IEnumerator JumpAndShoot()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);

        jumpingShooting = true;


        while(jumpingShooting)
        {
            Debug.Log("Shoot");
            GameObject x = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            x.GetComponent<SpriteRenderer>().color = Color.gray;
            x.GetComponent<Projectile>().setOwner(this);

            yield return new WaitForSeconds(.2f);

            jumpingShooting = !IsGrounded();
        }

    }

    IEnumerator ShootDown()
    {
        Vector3 oldPosition = rb.transform.position;
        rb.isKinematic = true;
        rb.transform.position = shootDownR.position;


        while (rb.transform.position.x > shootDownL.position.x)
        {
            rb.transform.position = new Vector3(rb.transform.position.x - 2f, rb.transform.position.y, rb.transform.position.z);

            GameObject y = Instantiate(projectileDown, new Vector3(this.transform.position.x, this.transform.position.y - 1, this.transform.position.z), this.transform.rotation);
            y.GetComponent<SpriteRenderer>().color = Color.yellow;
            y.GetComponent<Projectile>().setOwner(this);
            yield return new WaitForSeconds(.3f);
        }

        rb.transform.position = oldPosition;
        rb.isKinematic = false;

    }

    private bool IsGrounded()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, .1f, groundLayer))
        {
            return true;
        }
        return false;
    }
}
