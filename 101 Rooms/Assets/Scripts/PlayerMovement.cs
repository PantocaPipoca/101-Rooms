using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public float rollTime;
    public float rollCoolDown;

    Rigidbody2D rb;
    Vector2 moveDirection;
    bool SpikesCanHit = true;
    bool CanRool = true;
    bool OnSpikes = false;
    Animator animator;

    [SerializeField] Sprite leftHalf;
    [SerializeField] Sprite rightHalfFull;
    [SerializeField] Sprite rightHalfEmpty;
    [SerializeField] Sprite emptyHeart;
    [SerializeField] Image[] halfHearts;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        ProcessInput();
        HeartSystem();
        Roll();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Roll ()
    {
        if (Input.GetMouseButtonDown(1))
        {
            float[] rollTimes = new float[2] { rollTime, rollCoolDown };
            StartCoroutine("RollCoroutine", rollTimes);
        }
    }

    private void ProcessInput()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");

    }

    private void Move()
    {
        rb.velocity = moveDirection.normalized * speed;
        if (rb.velocity.x == 0 && rb.velocity.y == 0)
        {
            animator.SetBool("Walking", false);
        }
        else
        {
            animator.SetBool("Walking", true);
        }
    }

    private void HeartSystem ()
    {
        for (int i = 0; i < halfHearts.Length; i++)
        {

            if (i >= maxHealth || i >= health)
            {
                halfHearts[i].enabled = false;
            }
            if (i < maxHealth && i < health)
            {
                halfHearts[i].enabled = true;
            }

        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spikes"))
        {
            StartCoroutine("SpikesCoroutine2");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnSpikes = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnSpikes = false;
    }

    IEnumerator SpikesCoroutine1()
    {
            SpikesCanHit = false;
            health -= 1;
            yield return new WaitForSeconds(2.7f);
            SpikesCanHit = true;
    }

    IEnumerator SpikesCoroutine2()
    {
        if (OnSpikes)
        {
            yield return new WaitForSeconds(0.3f);
            if (OnSpikes && SpikesCanHit)
            {
                StartCoroutine("SpikesCoroutine1");
            }
        }
    }

    IEnumerator RollCoroutine(float[] rollTimes)
    {
        if (CanRool)
        {
            CanRool = false;
            speed *= 2;
            yield return new WaitForSeconds(rollTimes[0]);
            speed /= 2;
            yield return new WaitForSeconds(rollTimes[1]);
            CanRool = true;
        }
    }
}
