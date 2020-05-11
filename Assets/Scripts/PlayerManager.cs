using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
    [SerializeField]
    GameObject ClonePrefab;

    [SerializeField]
    List<GameObject> Weapons;


    List<GameObject> Clones = new List<GameObject>();
    [SerializeField]
    List<List<List<Command>>> PastClones;
    private CommandProcessor PlayerCP;
    private Player PlayerScript;
    private int NextWeaponNum;
    private int CloneNum;
    private EnemyManager EnemyManager;

    private
    // Start is called before the first frame update
    void Start()
    {
        NextWeaponNum = 0;
        CloneNum = 0;
        PlayerCP = Player.GetComponent<CommandProcessor>();
        PlayerScript = Player.GetComponent<Player>();
        PastClones = new List<List<List<Command>>>();
        EquipNextWeapon(Player);
        PlayerScript.Manager = this;
        EnemyManager = FindObjectOfType<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Respawn();
        }
    }

    void RecordClone(List<List<Command>> CommandImprint)
    {
        List<List<Command>> Temp = new List<List<Command>>();
        foreach (List<Command> a in CommandImprint)
        {
            List<Command> Temp2 = new List<Command>();
            foreach (Command b in a)
            {
                Command Temp3 = b;
                Temp2.Add(Temp3);
            }
            Temp.Add(Temp2);
        }
        PastClones.Add(Temp);
    }

    void MakeNewClone(List<List<Command>> CommandImprint)
    {

        GameObject NewClone = Instantiate(ClonePrefab,transform.position,transform.rotation,transform);
        Clones.Add(NewClone);
        NewClone.name = "PlayerClone " + CloneNum;
        Player NewP = NewClone.GetComponent<Player>();
        
        CommandProcessor NewCP = NewClone.GetComponent<CommandProcessor>();

        foreach (List<Command> a in CommandImprint)
        {
            foreach (Command b in a)
            {
                b.Player = NewP;
            }
            NewCP.Commands.Add(a);
        }

        NewCP.ConfirmCommandsForSelf();
        Physics2D.IgnoreCollision(NewP.BodyCollider, PlayerScript.BodyCollider);
        foreach (GameObject a in Clones)
        {
            Physics2D.IgnoreCollision(NewP.BodyCollider,a.GetComponent<Player>().BodyCollider);
        }
        EquipNextWeapon(NewClone);
        CloneNum++;
    }

    public void Respawn()
    {
        /*
        string DebugString = PastClones.Count + " | ";
        foreach (List<List<Command>> a in PastClones)
        {
            DebugString += a.Count + " ";
        }
        Debug.Log(DebugString);
        */
        NextWeaponNum = 0;
        CloneNum = 0;
        RecordClone(PlayerCP.Commands);
        foreach (GameObject a in Clones)
        {
            Destroy(a);
        }
        Clones.Clear();
        foreach (List<List<Command>> imprint in PastClones)
        {
            MakeNewClone(imprint);
        }

        Player.transform.position = transform.position;
        PlayerScript.ReInitialize();
        EquipNextWeapon(Player.gameObject);
        EnemyManager.ResetField();
    }

    void EquipNextWeapon(GameObject PlayerToEquipTo)
    {
        GameObject NewGun = Instantiate(Weapons[NextWeaponNum%Weapons.Count],PlayerToEquipTo.transform);
        PlayerToEquipTo.GetComponent<Player>().EquipGun(NewGun.GetComponent<Gun>());
        NewGun.transform.localPosition = new Vector3(0.89f, 0.032f, 0);
        NextWeaponNum++;
    }
}
