using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    #region Mudanças Vitor 0.1.3
    public float damage = 10f; // dano do tiro

    private void Update()
    {

        Destroy(gameObject, 5f); // se nao colidir com nada, dps de 5 seg ira se destruir

    }

    // quando colide com algo, inicia o script
    private void OnCollisionEnter(Collision collision)
    {

        CharacterStats target = collision.transform.GetComponent<CharacterStats>(); //Puxa de outro script

        if (target != null || collision.gameObject.tag == "Player") // testa se o alvo for diferente de vazio e um player
        {

            Vector3 direction = collision.transform.position - transform.position; // pega a direçao que a bala veio em relaçao ao objeto

            target.TakeDamage(damage); // uso do script CharacterStats para o dano
            Destroy(gameObject); // quando colidir com um inimigo ira se destruir

        }

    }
    #endregion

}
