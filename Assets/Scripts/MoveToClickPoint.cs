// MoveToClickPoint.cs
using UnityEngine;
using UnityEngine.AI;

public class MoveToClickPoint : MonoBehaviour
{
    public NavMeshAgent agent;

    private HeroCombat heroCombat;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        heroCombat = GetComponent<HeroCombat>();
    }

    void Update()
    {
        if (heroCombat.targetedEnemy != null)
        {
            if (heroCombat.targetedEnemy.GetComponent<HeroCombat>() != null )
            {
                if (heroCombat.targetedEnemy.GetComponent<HeroCombat>().isHereAlive)
                {
                    heroCombat.targetedEnemy = null;
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                if (hit.collider.tag == "Floor")
                {
                    heroCombat.targetedEnemy = null;
                    agent.stoppingDistance = 0;
                    agent.destination = hit.point;
                }
                
            }
        }
    }
}