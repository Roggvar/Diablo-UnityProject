using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float damage = 10f; // dano do tiro
    public float knockback = 10f; // força do knockback
    public float rbBackCooldown = 2f; // Cooldown pra voltar o isKinematic do objeto
    public static float rbBack = 0f; // Cooldown para mudar o estado do isKinematic do objeto

    public static Rigidbody enemyRB; // rb usado nesse script e no knockbackManager tbm

    private void Update()
    {

        Destroy(gameObject, 5f); // se nao colidir com nada, dps de 5 seg ira se destruir

    }

    // quando colide com algo, inicia o script
    private void OnCollisionEnter(Collision collision)
    {

        CharacterStats target = collision.transform.GetComponent<CharacterStats>(); //Puxa de outro script

        enemyRB = collision.collider.GetComponent<Rigidbody>(); // Pega o rb do objeto que colidiu
        KnockbackManager.enemyRBAfterDestruction = enemyRB;

        if (target != null || collision.gameObject.tag == "Enemy") // testa se o alvo for diferente de vazio e um inimigo
        {

            enemyRB.isKinematic = false; // seta o rb isKinematic do objeto para falso

            Vector3 direction = collision.transform.position - transform.position; // pega a direçao que a bala veio em relaçao ao objeto
            direction.y = 0; // evita que o knockback seja para cima

            enemyRB.AddForce(direction.normalized * knockback, ForceMode.Impulse); // aplica o knockback

            rbBack = Time.time + rbBackCooldown; // adiciona um cooldown para o isKinematic do objeto voltar a ser true

            target.TakeDamage(damage); // uso do script CharacterStats para o dano
            Destroy(gameObject); // quando colidir com um inimigo ira se destruir

        }

    }

}
