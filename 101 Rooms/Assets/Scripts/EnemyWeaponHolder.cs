using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponHolder : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject player;
    GameObject himSelf;
    Animator animator;

    [SerializeField] float offSet;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        himSelf = transform.parent.gameObject;
        animator = himSelf.GetComponent<Animator>();
    }


    void Update()
    {
        Aim();
    }

    void Aim()
    {
        Vector3 direction = player.transform.position - himSelf.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

        transform.position = himSelf.transform.position + (offSet * direction.normalized);

        if (angle > 90 || angle < -90)
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (angle >= -45 || angle <= 45)
        {
            animator.SetFloat("moveX", 1f);
            animator.SetFloat("moveY", 0f);
        }
        if (angle > 45 && angle < 135)
        {
            animator.SetFloat("moveX", 0f);
            animator.SetFloat("moveY", 1f);
        }
        if (angle >= 135 || angle <= -135)
        {
            animator.SetFloat("moveX", -1f);
            animator.SetFloat("moveY", 0f);
        }
        if (angle > -135 && angle < -45)
        {
            animator.SetFloat("moveX", 0f);
            animator.SetFloat("moveY", -1f);
        }
    }

}
