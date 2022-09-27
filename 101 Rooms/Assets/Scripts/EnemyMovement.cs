using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{

    public float health = 6;
    public bool move = true;
    public float stayRadius;
    public float followRadius;

    GameObject physicsObject;
    AIPath aiPath;
    Animator animator;



    void Start()
    {
        physicsObject = transform.parent.gameObject;
        aiPath = physicsObject.GetComponent<AIPath>();
        aiPath.endReachedDistance = followRadius;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (health < 0)
        {
            Destroy(physicsObject.gameObject);
        }
        if (aiPath.reachedEndOfPath)
        {
            aiPath.endReachedDistance = followRadius;
            move = false;
            animator.SetBool("Walking", false);
        }
        else
        {
            aiPath.endReachedDistance = stayRadius;
            move = true;
            animator.SetBool("Walking", true);
        }
    }
}
