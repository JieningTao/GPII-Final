using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Damageable
{

    private List<Transform> Targets;


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
        if (Targets.Count > 0 && Targets.Count < 4) 
        {
            LightResponse();
        }
        if ( Targets.Count <= 10)
        {
            MediumResponse();
        }
        if ( Targets.Count >10)
        {
            HeavyResponse();
        }
    }



    public override void Hit()
    {
        Destroy(this.gameObject);
    }

    private void LightResponse()
    {

    }

    private void MediumResponse()
    {

    }

    private void HeavyResponse()
    {

    }
}
