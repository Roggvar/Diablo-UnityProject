using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{

    #region Mudanças Vitor 0.1.3
    private Transform target; // armazena o alvo
    public float range = 15f; // area da turret
    public float speedRotation = 10f; // velocidade de rotaçao do inimigo
    public float fireRate = 1f; // x tiros por segundo
    public float fireCooldown = 0f; // cooldown para atirar
    public float bulletForce = 10f; // velocidade do tiro

    public GameObject bulletPrefab;
    public Transform enemyFirepoint;

    void Start()
    {

        InvokeRepeating("UpdateTarget", 0f, 0.5f); // Chama o metodo x vezes por segundo em vez de 60 ou mais

    }

    void Update()
    {

        if(target == null)
        {

            return;

        }

        //ROTAÇAO DO INIMIGO
        Vector3 direction = target.position - transform.position; //aponta pro target
        Quaternion lookRotation = Quaternion.LookRotation(direction); // matematica da rotaçao do alvo
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * speedRotation).eulerAngles; // transforma a matematica do quaternion em angulos que a unity compreende (i.e. X, Y, Z)
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f); // rotaciona o alvo apenas no eixo y

        if(fireCooldown <= 0f)
        {

            Shoot(); // chama o metodo
            fireCooldown = 1f / fireRate; // seta o cooldown

        }

        fireCooldown -= Time.deltaTime; //reduz o cooldown do tiro

    }

    void UpdateTarget ()
    {

        GameObject enemyPlayer = GameObject.FindGameObjectWithTag("Player"); // acha o objeto com tag player

        float distanceToEnemyPlayer = Vector3.Distance(transform.position, enemyPlayer.transform.position); // pega a distancia entre esse objeto e o player

        // seta o player como alvo quanado dentro da range, caso saia da range se torna null
        if(distanceToEnemyPlayer <= range)
        {

            target = enemyPlayer.transform;

        }
        else
        {

            target = null;

        }

    }

    void Shoot()
    {

        GameObject bullet = Instantiate(bulletPrefab, enemyFirepoint.position, enemyFirepoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        rb.AddForce(enemyFirepoint.forward * bulletForce, ForceMode.Impulse); // adiciona a velocidade ao tiro

    }

    //TEMPORARIO
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, range);

    }
    #endregion

}
