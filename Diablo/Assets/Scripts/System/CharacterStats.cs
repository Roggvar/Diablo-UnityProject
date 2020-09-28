using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public int maxHealth = 100; // vida do objeto

    public float currentHealth { get; private set; }

    public Stat damage; // dano do objto
    public Stat armor; // armadura do objeto

    private void Awake()
    {

        currentHealth = maxHealth; // atualiza caso seja mudado no editor

    }

    // Gerencia a matematica por traz do dano
    public void TakeDamage (float damage)
    {

        damage -= armor.GetValue(); // Testa o dano contra a armadura
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage; // alica o dano ao objeto

        // se a vida cgear a 0, aciona o outro script
        if(currentHealth <= 0)
        {

            Die();

        }

    }

    // Gerencia a morte do objeto, pode ser substituido em outros scripts devido ao Virtual
    public virtual void Die ()
    {

        //Debug.Log(transform.name + " MORREU");

    }

}
