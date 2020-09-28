using UnityEngine;

public class Interactable : MonoBehaviour
{

    public float radius = 3f; // area do objeto

    public Transform interactionTransform;

    bool isFocus = false; // testa se eh o focus do player
    bool hasInteracted = false; // se ja foi interagido pelo player

    Transform player; // pega o player

    // server para outros objetos substituirem esse script, por isso o virtual
    public virtual void Interact()
    {



    }

    private void Update()
    {
        
        //Garante que so interaje com o foco, e que nunca foi acionado antes
        if(isFocus && !hasInteracted)
        {

            float distance = Vector3.Distance(player.position, interactionTransform.position); // pega a distancia do player

            if(distance <= radius) // testa para ver se a distancia do player bate com a interaçao do objeto
            {

                Interact(); // aciona o virtual Interact que o objeto substituiu
                hasInteracted = true;

            }

        }

    }

    public void OnFocused(Transform playerTransform)
    {

        isFocus = true;
        player = playerTransform;
        hasInteracted = false;

    }

    public void OnDefocused()
    {

        isFocus = false;
        player = null;
        hasInteracted = false;

    }

    // desenho do gizmo
    private void OnDrawGizmosSelected()
    {

        if (interactionTransform == null)
        {

            interactionTransform = transform;

        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);

    }

}
