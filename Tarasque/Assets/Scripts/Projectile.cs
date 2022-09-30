using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float velocity;
    private Character attacker;

    [SerializeField] private Rigidbody2D rb;
    public void setOwner(Character c)
    {
        attacker = c;
    }

    private void Awake()
    {
        rb.velocity = transform.right * velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy") && attacker.tag.Equals("Player"))
        {
            collision.GetComponent<EnemyBoss>().takeDamage(attacker.getAttack());
        }
        else if (collision.tag.Equals("Player") && attacker.tag.Equals("Enemy"))
        {
            collision.GetComponent<Character>().takeDamage(attacker.getAttack());
        }

        Destroy(this.gameObject);
    }
}
