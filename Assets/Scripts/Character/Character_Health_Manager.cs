using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Health_Manager : MonoBehaviour
{
    public static Character_Health_Manager health;

    [SerializeField] float maxHealth;
    [HideInInspector] public bool isHit;
    float currentHealth;

    const string HIT = "Hit";
    const string DEATH = "Death";
    const string BLOCKED = "Blocked";

    Rigidbody2D rb;

    void Awake()
    {
        health = this; 
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }
    public void HealthManagement(float damage, int throwDir) {
        if (Character_Combat.combInstance.isBlocking)
            Character_Animation_Manager.animInstance.ChangeAnimationState(BLOCKED);
        else {
            isHit = true;
            if (currentHealth > 0) {
                Character_Animation_Manager.animInstance.ChangeAnimationState(HIT);
                rb.AddForce(new Vector2(6 * throwDir, 5), ForceMode2D.Impulse);
                currentHealth -= damage;
            }
            else if (currentHealth <= 0) {
                Death();
            }
        }
    }

    void Death() {
        rb.velocity = new Vector2(0, rb.velocity.y);
        Character_Animation_Manager.animInstance.ChangeAnimationState(DEATH);
        this.enabled = false;
        Physics2D.IgnoreLayerCollision(3, 8, true);
        Character_Moviment.moveInstance.enabled = false;
        Character_Combat.combInstance.enabled = false;
        Character_Animation_Manager.animInstance.enabled = false;
    }
}
