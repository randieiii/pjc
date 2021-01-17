using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCombat : MonoBehaviour
{
    public GameObject targetedEnemy;
    public enum HereAttackType {Melee, Range};
    public HereAttackType hereAttackType;

    public float attackRange;
    public float rotateSpeed;

    private MoveToClickPoint movement;
    private Stats statsScript;


    public bool basickAttackIdle = false;
    public bool isHereAlive;
    public bool performMeleeAttack = true;
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<MoveToClickPoint>();
        statsScript = GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetedEnemy != null)
        {
            if (Vector3.Distance(gameObject.transform.position, targetedEnemy.transform.position) > attackRange)
            {
                movement.agent.SetDestination(targetedEnemy.transform.position);
                movement.agent.stoppingDistance = attackRange;
            }
            else
            {
                if (hereAttackType == HereAttackType.Melee)
                {
                    if(performMeleeAttack)
                    {
                        

                        StartCoroutine(MeleeAttackInterval());
                    }
                }
            }
        }
    }

    IEnumerator MeleeAttackInterval() 
    {
        performMeleeAttack = false;
        MeleeAttack();
        Debug.Log("Attacking");
        yield return new WaitForSeconds(statsScript.attackTime / ((100 + statsScript.attackTime) * 0.01f));

        if (targetedEnemy == null)
        {
            performMeleeAttack = true;
        }
    }

    public void MeleeAttack()
    {
        if (targetedEnemy != null )
        {
            if (targetedEnemy.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Minion )
            {
                targetedEnemy.GetComponent<Stats>().health -= statsScript.attackDmg;
            }
        }
        performMeleeAttack = true;
    }
}
