using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletSpeed;

    EnemyMovement enemyScript;
    PlayerMovement playerScript;

    void Update()
    {
        BulletFly();
    }

    void BulletFly()
    {
        transform.Translate(Vector2.right.normalized * bulletSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            if(collision.gameObject.CompareTag("Enemy"))
            {
                enemyScript = collision.gameObject.GetComponent<EnemyMovement>();
                enemyScript.health -= 1;
            }
            if(collision.gameObject.CompareTag("Player"))
            {
                playerScript = collision.gameObject.GetComponent<PlayerMovement>();
                playerScript.health -= 1;
            }
        }
    }
}
