﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]

public class CommandProcessor : MonoBehaviour
{
    private List<List<Command>> Commands = new List<List<Command>>();
    private Player MyPlayer;
    private bool Automated;

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
    }

    private void FixedUpdate()
    {
        if (Automated)
        {
            AutomatedExecuteCommand();
        }
    }



}