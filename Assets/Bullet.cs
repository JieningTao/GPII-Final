using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float DespawnTimer;
    [SerializeField]
    private float TravelSpeed;


    //i am considering setting up an object pool for the bullets



    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, DespawnTimer);
    }

    // Update is called once per frame
    void Update()
    {
        Fly();
    }

    private void Fly()
    {
        /*
        RaycastHit2D Hit = Physics2D.Raycast(transform.position, transform.right, TravelSpeed * Time.deltaTime);
        if (Hit.collider != null)
        {
            transform.Translate(transform.right * Hit.distance);
            Hit.collider.gameObject.GetComponent<Damageable>().Hit();
        }
        else
            */
            transform.Translate(transform.right * TravelSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
        collision.gameObject.GetComponent<Damageable>().Hit();
    }


}
