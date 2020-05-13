using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]

public class CommandProcessor : MonoBehaviour
{
    public List<List<Command>> Commands = new List<List<Command>>();
    private Player MyPlayer;
    [SerializeField]
    private bool Automated;
    [SerializeField]
    private int CommandListSize;

    private void Start()
    {
        MyPlayer = GetComponent<Player>();
        Automated = !MyPlayer.CurrentPlaythrough;
    }



    public void ManualExecuteCommand(List<Command> CommandsThisUpdate)
    {
        Commands.Add(CommandsThisUpdate);
        foreach (Command a in CommandsThisUpdate)
        {
            a.Execute();
        }
    }

    private void AutomatedExecuteCommand()
    {
        if (Commands.Count > 0)
        {
            foreach (Command a in Commands[0])
            {
                a.Execute();
            }
            Commands.Remove(Commands[0]);
        }
        else
        {
            MyPlayer.Explode();
        }
    }

    public void ConfirmCommandsForSelf()
    {
        Start();//make sure that my player is assigned before assigning them to the commands
        foreach (List<Command> a in Commands)
        {
            foreach (Command b in a)
            {
                b.Player = MyPlayer;
            }
        }
        CommandListSize = Commands.Count;
    }

    private void FixedUpdate()
    {
        if (Automated)
        {
            AutomatedExecuteCommand();
        }
    }



}
