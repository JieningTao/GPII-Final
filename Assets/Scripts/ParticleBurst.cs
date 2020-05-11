using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBurst : MonoBehaviour
{
    private void Start()
    {
        GetComponent<ParticleSystem>().Emit(20);
        Destroy(this.gameObject, 5);
    }
}
