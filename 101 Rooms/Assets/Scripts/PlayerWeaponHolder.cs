using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHolder : MonoBehaviour
{

    Rigidbody2D rb;
    GameObject player;
    Animator animator;

    [SerializeField] float offSet;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = player.GetComponent<Animator>();
    }


    void Update()
    {
        Aim();
    }

    void Aim()
    {
        Vector3 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(player.transform.position);
        direction.z = Camera.main.transform.position.z * -1;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

        transform.position = player.transform.position + (offSet * direction.normalized);
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
            animator.SetFloat("mouseX", 1f);
            animator.SetFloat("mouseY", 0f);
        }
        if (angle > 45 && angle < 135)
        {
            animator.SetFloat("mouseX", 0f);
            animator.SetFloat("mouseY", 1f);
        }
        if (angle >= 135 || angle <= -135)
        {
            animator.SetFloat("mouseX", -1f);
            animator.SetFloat("mouseY", 0f);
        }
        if (angle > -135 && angle < -45)
        {
            animator.SetFloat("mouseX", 0f);
            animator.SetFloat("mouseY", -1f);
        }
    }

}
