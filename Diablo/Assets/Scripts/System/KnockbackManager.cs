using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackManager : MonoBehaviour
{

    public static Rigidbody enemyRBAfterDestruction; // Rb para evitar um null reference do bullet quando destruido
    public GameObject playerTag; // gameobject para evitar null reference do bullet quando destruido

    public float cooldownReady = 0f;
    public float cooldown = 10f;

    void Update()
    {

        if (Time.time >= cooldownReady)
        {

            playerTag = GameObject.FindGameObjectWithTag("Player"); // acha o objeto com tag Player

            BulletScript.enemyRB = playerTag.GetComponent<Rigidbody>(); // Coloca o rb do objeto com tag player dentro do enemyrb do bullet

            cooldownReady = Time.time + cooldown; // aplica o tempo de cooldown do tiro

        }
        /*
        if (playerTag == null) // sempre que a playertag ficar vazia o script colocara o player como rb, assim evitando null reference
        {

            playerTag = GameObject.FindGameObjectWithTag("Player"); // acha o objeto com tag Player

            BulletScript.enemyRB = playerTag.GetComponent<Rigidbody>(); // Coloca o rb do objeto com tag player dentro do enemyrb do bullet

        }
        */

        if (Time.time >= BulletScript.rbBack) // traz de volta a kinematic do objeto que o player acertar com o tiro
        {

            BulletScript.enemyRB.isKinematic = true; // seta para true a iskinematic do objeto

        }

        //Debug.Log(BulletScript.enemyRB);

    }

}
