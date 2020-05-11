using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    List<Enemy> Enemies;

    // Start is called before the first frame update
    void Start()
    {
        Enemies = new List<Enemy>();
        foreach (Enemy a in FindObjectsOfType<Enemy>())
        {
            if (!Enemies.Contains(a))
            {
                Enemies.Add(a);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ResetField()
    {
        foreach (Enemy a in Enemies)
        {
            a.gameObject.SetActive(true);
        }
    }
}
