using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG : Gun
{
    private bool CurrentlyFiring;
    // Start is called before the first frame update
    void Start()
    {
        CurrentlyFiring = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void Shoot(bool a)
    {
        if (a)
        {
            CurrentlyFiring = true;
            StartCoroutine(AutoFire());
        }
        else
        {
            CurrentlyFiring = false;
            StopAllCoroutines();
        }

    }

    private IEnumerator AutoFire()
    {
        while (CurrentlyFiring)
        {
            base.Shoot(true);
            yield return new WaitForSeconds(TBS);
        }
        yield return null;
    }
}
