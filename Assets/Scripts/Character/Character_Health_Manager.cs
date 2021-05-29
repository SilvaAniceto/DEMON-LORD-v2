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


    void Awake()
    {
        health = this; 
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HealthManagement(float damage)
    {
        if (Character_Combat.combInstance.isBlocking)
            Character_Animation_Manager.animInstance.ChangeAnimationState(BLOCKED);
        else
        {
            isHit = true;
            Character_Animation_Manager.animInstance.ChangeAnimationState(HIT);
            currentHealth -= damage;
        }
    }

    void Death()
    {
        Character_Animation_Manager.animInstance.ChangeAnimationState(DEATH);
    }
}
