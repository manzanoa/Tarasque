using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class is used to set up the initial stats for all of the players and enemies to borrow
public class Character : MonoBehaviour
{
    [SerializeField] private int maxHP;
    [SerializeField] private int HP;
    [SerializeField] private string charName;
    [SerializeField] private int attack;
    [SerializeField] private int defense;
    [SerializeField] private float speed = 8;
    [SerializeField] private GameUI healthBar;
    public bool isDead = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //getter for ui
    public int getMaxHP()
    {
        return maxHP;
    }
    public int getHP()
    {
        return HP;
    }

    public GameUI getHealthBar()
    {
        return healthBar;
    }

    public int getAttack()
    {
        return attack;
    }

    //a method where the character takes the attacker's attack points to calculate the damage
    //it also tells the game if the character died or not
    public bool takeDamage(int att)
    {
        HP = HP - (att - defense);
        HP = Mathf.Clamp(HP, 0, maxHP);


        healthBar.Setup(getMaxHP(), getHP());

        if(HP == 0)
        {
            isDead = true;
            return true;
        }
        else
        {
            isDead = false;
            return false;
        }
    }

    // gets the speed of the character
    public float getSpeed()
    {
        return speed;
    }

    public void setSpeed(float s)
    {
        speed = s;
    }


}
