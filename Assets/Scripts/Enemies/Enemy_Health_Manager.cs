using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health_Manager : MonoBehaviour {
    public static Enemy_Health_Manager enemyHealth;

    [SerializeField] float maxHealth;
    [HideInInspector] public bool isHit;
    float currentHealth;

    const string HIT = "Hit";
    const string DEATH = "Death";

    Animator anim;

    void Awake() {
        anim = GetComponent<Animator>();
        enemyHealth = this;
    }
    // Start is called before the first frame update
    void Start() {
        currentHealth = maxHealth;
    }

    public void EnemyHealthManagement(float damage) {
        isHit = true;
        if (currentHealth > 0) {
            anim.Play(HIT);
            currentHealth -= damage;
        }
        else if (currentHealth <= 0) {
            Death();
        }
    }

    void Death() {
        anim.Play(DEATH);
        this.enabled = false;
        Physics2D.IgnoreLayerCollision(3, 8, true);
    }
}
