using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TackleHitBox : MonoBehaviour
{
    [SerializeField]
    Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Enemy"))
        {
            collision.GetComponent<EnemyBoss>().takeDamage(player.getAttack());
        }
    }
    public void TackeDuration()
    {
        StartCoroutine(tackleDuration());
    }

    public IEnumerator tackleDuration()
    {
        float s = player.getSpeed();

        player.setSpeed(s * 2);

        yield return new WaitForSeconds(2.0f);

        this.gameObject.SetActive(false);
        player.setSpeed(s);


    }
}
