using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

    public static bool isUsingHability = false; // Variavel para testar se o player esta usando habilidades
    public static float movementReady = 0f; // Cooldown para o player começar a se mexer

    public Interactable focus;
    public LayerMask movementMask; // Mask que o player ira interagir
    public static NavMeshAgent agent; // aloca navmesh

    Camera cam;
    PlayerMotor motor;
    

    void Start()
    {

        cam = Camera.main; // cam
        motor = GetComponent<PlayerMotor>(); // motor
        agent = GetComponent<NavMeshAgent>(); // agent

    }


    void Update()
    {
        
        //Evitar conflito no clique do mouse
        if(EventSystem.current.IsPointerOverGameObject())
        {

            return;

        }

        //Botao esquerdo do mouse
        if(Input.GetMouseButton(0))
        {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //Move o player para o ponto do click
            if(Physics.Raycast(ray, out hit, 100, movementMask) && isUsingHability == false)
            {

                motor.MoveToPoint(hit.point);

                RemoveFocus();

            }

        }

        //Botao direito do mouse
        if (Input.GetMouseButtonDown(1))
        {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100) && isUsingHability == false)
            {

                Interactable interactable = hit.collider.GetComponent<Interactable>(); //Faz todo o foco no objeto

                if (interactable != null)
                {

                    SetFocus(interactable);

                }

            }

        }

        // Botao espaço para o dodge
        if (Input.GetKeyDown(KeyCode.Space))
        {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //Move o player para o ponto do click
            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {

                motor.Dodge(hit.point);

                RemoveFocus();

            }

        }

    }

    // Gerencia Foco no objeto
    public void SetFocus(Interactable newFocus)
    {

        if(newFocus != focus)
        {

            if(focus != null)
            {

                focus.OnDefocused();

            }

            focus = newFocus;
            motor.FollowTarget(newFocus);

        }

        
        newFocus.OnFocused(transform);
        

    }

    // Gerencia o Remove o foco no objeto
    void RemoveFocus()
    {

        if (focus != null)
        {

            focus.OnDefocused();

        }

        focus = null;
        motor.StopFollowingTarget();

    }

    //Gerencia o uso de habilidades e movimentaçao do player durante o uso
    public static void HabilityUse ()
    {

        if (isUsingHability == true && Time.time >= movementReady)
        {

            isUsingHability = false; // Diz se o player esta usando uma habilidade
            agent.isStopped = false; // Retorna a movimentaçao pra o player

        }

    }
}
