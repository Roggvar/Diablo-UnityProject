using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeScript : MonoBehaviour
{

    public float damage = 10f; // dano do Cone
    public static float timeActive = 1.5f; //seg para se destruir
    public static bool coneStarted = false; // bool para test se começou o cone

    void Update()
    {

        BoxDetection(); // chama o metodo
        Destroy(gameObject, timeActive); // dps de x seg ira se destruir

    }

    void BoxDetection()
    {

        coneStarted = true; // bool test

        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale, transform.rotation); // armazena todos os colliders que bateram na box

        foreach (Collider objects in colliders) // pra cada collider
        {

            CharacterStats target = objects.GetComponent<CharacterStats>(); // acessa o componente CharacterStats

            if (target != null)
            {

                if (target.CompareTag("Enemy")) // o collider tiver a tag Enemy
                {

                    target.TakeDamage(damage); // toma dano
                    target.transform.gameObject.tag = "Untagged"; // para evitar multiplas instancias de dano, remove a tag enemy

                }

            }

        }
    }

    //TEMPORARIO
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, transform.localScale);

    }

}
