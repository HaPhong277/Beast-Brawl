using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackpoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        UseSkill();
    }
    private void OnDrawGizmosSelected()
    {
        if (attackpoint == null) return;
        Gizmos.DrawWireSphere(attackpoint.position, attackRange);
    }
    public void UseSkill()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetBool("Skill1", true);
            HandleDamaged();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool("Skill2", true);
            HandleDamaged();

        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetBool("Skill3", true);
            HandleDamaged();
        }
    }
    private void HandleDamaged()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("BEM");
        }
    }
}
