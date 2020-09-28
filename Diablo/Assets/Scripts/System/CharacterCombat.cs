using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{

    public float attackSpeed = 1f; // nao lembro
    public float attackDelay = 0.6f; // nao lembro

    private float attackCooldown = 0f; // cooldowm do ataque

    public event System.Action OnAttack; // nao lembro, extremamente importante thought

    CharacterStats myStats; // alocamento dos status do objeto

    void Start()
    {

        myStats = GetComponent<CharacterStats>(); // aplica o alocamento dos status do objeto, puxando de outro script

    }

    void Update()
    {

        attackCooldown -= Time.deltaTime;

    }

    // Gerencia o ataque
    public void Attack (CharacterStats targetStats)
    {

        if(attackCooldown <= 0)
        {
            StartCoroutine(DoDamage(targetStats, attackDelay)); // começa o segundo script a rodar simultaneamente

            if(OnAttack != null)
            {

                OnAttack();

            }

            attackCooldown = 1f / attackSpeed; // gerencia o cooldown do ataque

        }

    }

    IEnumerator DoDamage (CharacterStats stats, float delay)
    {

        yield return new WaitForSeconds(delay);

        stats.TakeDamage(myStats.damage.GetValue());

    }

}
