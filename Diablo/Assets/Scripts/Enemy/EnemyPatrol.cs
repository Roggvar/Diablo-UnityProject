using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public float timePatrol = 15;
    private WaitForSeconds time;
    public Transform[] waypoints;
    private int index;
    private Animator anim;
    private NavMeshAgent agent;

    void Start()
    {
        time = new WaitForSeconds(timePatrol);
        agent = GetComponent<NavMeshAgent>();
        index = Random.Range(0, waypoints.Length);
        anim = GetComponentInChildren<Animator>();
        StartCoroutine(CallPatrol());
    }

    private void Update() 
    {
        anim.SetFloat("Move",agent.velocity.sqrMagnitude,0.06f,Time.deltaTime);
    }

    IEnumerator CallPatrol()
    {
        while (true)
        {
            yield return time;
            Patrol();
        }
    }

    void Patrol()
    {
        index = index == waypoints.Length - 1 ? 0 : index + 1;
        agent.destination = waypoints[index].position;
    }
}
