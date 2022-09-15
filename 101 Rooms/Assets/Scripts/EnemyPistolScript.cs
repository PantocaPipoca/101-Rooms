using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPistolScript : MonoBehaviour
{
    public float coolDown;

    float nextShot;
    GameObject enemy;
    EnemyMovement enemyScript;

    [SerializeField] GameObject bullet;
    [SerializeField] GameObject firePoint;

    void Start()
    {
        enemy = transform.parent.gameObject.transform.parent.gameObject;
        enemyScript = enemy.GetComponent<EnemyMovement>();
    }

    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (Time.time > nextShot && !enemyScript.move)
        {
            Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
            nextShot = Time.time + coolDown;
        }
    }
}
