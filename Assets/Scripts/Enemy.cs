using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Damageable
{
    [SerializeField]
    private Transform Gun;

    [SerializeField]
    private Transform TipOfBarrel;

    [SerializeField]
    private float AimSpeed = 1;

    [SerializeField]
    GameObject EnemyBullet;



    public List<Transform> Targets;//public for testing purposes, doesn't need to be public to play
    private float AttackCoolDown;

    //the enemy will get a weapon with which it can kill the player
    //This enemy class will onday use strategy pattern to scale up it's attacks as more and more copies of players appear


    // Start is called before the first frame update
    void Start()
    {
        Targets = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Targets.Count == 0)
        {
        }
        else if (Targets.Count > 0 && Targets.Count < 4) 
        {
            LightResponse();
        }
        else if ( Targets.Count <= 10)
        {
            MediumResponse();
        }
        else if ( Targets.Count >10)
        {
            HeavyResponse();
        }
        Debug.DrawRay(TipOfBarrel.position, TipOfBarrel.right, Color.red);
    }



    public override void Hit()
    {
        this.gameObject.SetActive(false);
    }

    private void LightResponse()
    {
        AimGun(AimSpeed);
        if (AttackCoolDown > 0)
        {
            AttackCoolDown -= Time.deltaTime;
        }
        else
        {
            Shoot(2);
            AttackCoolDown = 1.5f;
        }
    }

    private void MediumResponse()
    {
        AimGun(AimSpeed);
        if (AttackCoolDown > 0)
        {
            AttackCoolDown -= Time.deltaTime;
        }
        else
        {
            StartCoroutine( MultiShot(3,3));
            AttackCoolDown = 2;
        }
    }

    private void HeavyResponse()
    {
        AimGun(AimSpeed);
        if (AttackCoolDown > 0)
        {
            AttackCoolDown -= 1.5f*Time.deltaTime;
        }
        else
        {
            for(int i = 0; i<5;i++)
                Shoot(10);
            AttackCoolDown = 2.5f;
        }
    }



    private void AimGun(float Speed)
    {
        Vector3 vectorToTarget = Targets[0].position - Gun.transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        Gun.transform.rotation = Quaternion.Slerp(Gun.transform.rotation, q, Time.deltaTime * AimSpeed);
    }

    private void Shoot(float AimDeviation)
    {
        GameObject NewLaser = Instantiate(EnemyBullet, TipOfBarrel.position, Gun.rotation);
        NewLaser.transform.Rotate(Vector3.forward, Random.Range(-AimDeviation, AimDeviation));
    }

    private IEnumerator MultiShot(int ShotNum,float AimDeviation)
    {
        for (int i = 0; i < ShotNum; i++)
        {
            Shoot(AimDeviation);
            yield return new WaitForSeconds(0.1f);
        }
        
    }





    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!Targets.Contains(collision.transform))
                Targets.Add(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Targets.Remove(collision.transform);
    }
}
