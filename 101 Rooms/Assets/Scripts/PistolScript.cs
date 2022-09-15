using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PistolScript : MonoBehaviour
{

    public float coolDown;
    public int maxAmmunition;
    public int maxBulletsInMagazine;
    public float reloadTime;

    Animator animator;
    float nextShot;
    int bulletsInMagazine;
    bool reloading = false;
    bool coroutineRunning = false;
    int ammunition;

    [SerializeField] GameObject bullet;
    [SerializeField] GameObject firePoint;
    [SerializeField] TextMeshProUGUI bulletNumber;

    void Start()
    {
        ammunition = maxAmmunition;
        bulletsInMagazine = maxBulletsInMagazine;
        bulletNumber.text = ($"{bulletsInMagazine}/{ammunition}");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Shoot();
        Reload();
    }

    void Shoot()
    {
        if (Time.time > nextShot && Input.GetMouseButtonDown(0) && bulletsInMagazine > 0 && !reloading)
        {
            ammunition -= 1;
            bulletsInMagazine -= 1;
            Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
            nextShot = Time.time + coolDown;
            bulletNumber.text = ($"{bulletsInMagazine}/{ammunition}");
        }

    }
    void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            reloading = true;
        }

        else if (reloading && !coroutineRunning)
        {
            StartCoroutine(Reload());
            coroutineRunning = true;
        }

        IEnumerator Reload()
        {
            animator.SetBool("reloading", true);

            if (ammunition <= maxBulletsInMagazine && reloading)
            {
                bulletsInMagazine = ammunition;
            }
            else if (ammunition > maxBulletsInMagazine && reloading)
            {
                bulletsInMagazine = maxBulletsInMagazine;
            }
            yield return new WaitForSeconds(reloadTime);
            bulletNumber.text = ($"{bulletsInMagazine}/{ammunition}");
            animator.SetBool("reloading", false);
            coroutineRunning = false;
            reloading = false;
        }
    }

}
