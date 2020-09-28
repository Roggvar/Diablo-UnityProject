using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{

    public HealthBar healthbar;

    void Start()
    {

        healthbar.SetMaxHealth(maxHealth);

    }

    void Update()
    {

        healthbar.setHealth((int)currentHealth);

    }

    // Substitui o Virtual de outro script
    public override void Die()
    {

        base.Die();


        Destroy(gameObject);
    }

}
