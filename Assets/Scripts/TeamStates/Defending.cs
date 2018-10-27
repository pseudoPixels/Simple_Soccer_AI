using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defending : State<SoccerTeam>
{


    //states are made singleton
    private static Defending instance = null;

    //for thread safety creation of the singleton
    private static readonly object padlock = new object();



    private Defending()
    {

    }

    public static Defending Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new Defending();
                }

                return instance;
            }

        }
    }





    public override void Enter(SoccerTeam ent)
    {
        throw new NotImplementedException();
    }

    public override void Execute(SoccerTeam ent)
    {
        throw new NotImplementedException();
    }

    public override void Exit(SoccerTeam ent)
    {
        throw new NotImplementedException();
    }

    public override bool OnMessage(SoccerTeam ent, Telegram msg)
    {
        throw new NotImplementedException();
    }
}
