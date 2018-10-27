using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareForKickOff : State<SoccerTeam>
{


    //states are made singleton
    private static PrepareForKickOff instance = null;

    //for thread safety creation of the singleton
    private static readonly object padlock = new object();



    private PrepareForKickOff()
    {

    }

    public static PrepareForKickOff Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new PrepareForKickOff();
                }

                return instance;
            }

        }
    }






    public override void Enter(SoccerTeam team)
    {
        //reset key players
        team.ControllingPlayer = null;
        team.SupportingPlayer = null;
        team.ReceivingPlayer = null;
        team.PlayerClosestToBall = null;

        //send Msg_GoHome to each player.
        team.ReturnAllFieldPlayersToHome();

    }

    public override void Execute(SoccerTeam team)
    {
        //if both teams in position, start the game
        if (team.AllPlayersAtHome() && team.OpponentTeam.AllPlayersAtHome())
        {
            team.StateMachine.ChangeState(Defending.Instance);
        }
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
