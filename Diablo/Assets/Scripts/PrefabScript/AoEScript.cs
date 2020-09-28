using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEScript : MonoBehaviour
{

    public float damage = 10f; // dano do AoE
    public float radius = 5f; // radius do AoE
    public static float timeActive = 3f; //seg para se destruir
    public static bool aoeStarted = false; // teste para ver se o aoe começou

    private void Update()
    {

        SphereDetection(); // chama o metodo
        Destroy(gameObject, timeActive); // dps de X seg ira se destruir

    }

    void SphereDetection ()
    {

        aoeStarted = true; // test bool

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius); // cria um sphere e armazena todos os colliders

        foreach(Collider objects in colliders) // pra cada collider
        {

            CharacterStats target = objects.GetComponent<CharacterStats>(); // acessa o componente CharacterStats

            if(target != null)
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

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);

    }
}
