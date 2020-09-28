using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float lookRadius = 10f; // Radius que o inimigo detecta o player

    //LER O README E CHANGELOG PRA SABER OQ FAZER
    #region Mudanças Vitor #1 v0.1.2
    private float aoeCooldown = AoEScript.timeActive + 0.5f; // tempo do cooldown do AoE para mudar a tag
    private float CooldownReady = 0f; // tempo de cooldown
    private float coneCooldown = ConeScript.timeActive + 0.5f; // tempo do cooldown do cone para mudar a tag
    private bool cooldownStarted = false; // bool de teste do cooldown
    #endregion


    Transform target; // aloca um target
    NavMeshAgent agent; // aloca um navmesh
    CharacterCombat combat; // aloca um combat do CharacterCombat script

    void Start()
    {

        target = PlayerManager.instance.player.transform; // aloca o target ao player
        agent = GetComponent<NavMeshAgent>(); // aloca o navmesh da cena
        combat = GetComponent<CharacterCombat>(); // aloca o combat

    }

    void Update()
    {

        float distance = Vector3.Distance(target.position, transform.position); // aloca a distancia do alvo

        //Gerencia o destino do objeto
        if(distance <= lookRadius)
        {

            agent.SetDestination(target.position); // calculo do navmesh para chegar no destino do objeto

            if(distance <= agent.stoppingDistance)
            {

                CharacterStats targetStats = target.GetComponent<CharacterStats>(); // aloca um alvo

                if(targetStats != null)
                {

                    combat.Attack(targetStats); // ataca o alvo usando os status de outro script

                }
                
                FaceTarget(); //Rotaciona o objeto em relaçao ao player

            }

        }

        //LER O README E CHANGELOG PRA SABER OQ FAZER
        #region Mudanças Vitor #2 v0.1.2
        if (gameObject.CompareTag("Untagged")) // quando o gameobject tiver a tag Untagged
        {

            if(AoEScript.aoeStarted == true && cooldownStarted == false) // começa o cooldown para voltar a ser Enemy
            {

                CooldownReady = Time.time + aoeCooldown;
                cooldownStarted = true;

            }

            if (ConeScript.coneStarted == true && cooldownStarted == false) // começa o cooldown para voltar a ser Enemy
            {

                CooldownReady = Time.time + coneCooldown;
                cooldownStarted = true;

            }

            if (Time.time >= CooldownReady)
            {

                gameObject.tag = "Enemy"; // Se torna Enemy novamente
                cooldownStarted = false;

            }
        }
        #endregion


    }

    //Gerencia a rotaçao do objeto em relaçao ao alvo
    void FaceTarget()
    {

        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

    }

    // Gerencia o desenho do gizmo
    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

    }
}
