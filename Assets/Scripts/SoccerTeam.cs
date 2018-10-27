using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerTeam {
    public enum team_color {blue, red};

    //state machine of this team
    private StateMachine<SoccerTeam> stateMachine;

    //assigned color of the team
    team_color teamColor;

    //the reference to the soccer pitch
    SoccerPitch soccerPitch;

    //list of players of the team
    List<PlayerBase> players;

    //references to the goal posts
    Goal homeGoal;
    Goal opponentsGoal;

    //reference to the opposing Team
    SoccerTeam opponentTeam;


    //key players of the team
    PlayerBase controllingPlayer;
    PlayerBase supportingPlayer;
    PlayerBase receivingPlayer;
    PlayerBase playerClosestToBall;

    //the distance between ball and closest player to it
    double distSqToBallOfClosestPlayer;




    public SoccerTeam(Goal home_goal, Goal opponents_goal, SoccerPitch pitch, team_color team_color)
    {
        this.homeGoal = home_goal;
        this.opponentsGoal = opponents_goal;
        this.soccerPitch = pitch;
        this.teamColor = team_color;

        this.controllingPlayer = null;
        this.supportingPlayer = null;
        this.receivingPlayer = null;
        this.playerClosestToBall = null;

        this.distSqToBallOfClosestPlayer = 0;

        //setup the state machine for this team
        this.StateMachine = new StateMachine<SoccerTeam>(this);

        //the first state of a team is defending
        this.StateMachine.SetPreviousState(Defending.Instance);
        this.StateMachine.SetCurrentState(Defending.Instance);
        this.StateMachine.SetGlobalState(null);





    }


    public void ReturnAllFieldPlayersToHome()
    {

    }

    public bool AllPlayersAtHome()
    {

        return false;
    }





    public PlayerBase ControllingPlayer
    {
        get
        {
            return controllingPlayer;
        }

        set
        {
            controllingPlayer = value;
        }
    }

    public PlayerBase SupportingPlayer
    {
        get
        {
            return supportingPlayer;
        }

        set
        {
            supportingPlayer = value;
        }
    }

    public PlayerBase ReceivingPlayer
    {
        get
        {
            return receivingPlayer;
        }

        set
        {
            receivingPlayer = value;
        }
    }

    public PlayerBase PlayerClosestToBall
    {
        get
        {
            return playerClosestToBall;
        }

        set
        {
            playerClosestToBall = value;
        }
    }

    public SoccerTeam OpponentTeam
    {
        get
        {
            return opponentTeam;
        }

        set
        {
            opponentTeam = value;
        }
    }

    public StateMachine<SoccerTeam> StateMachine
    {
        get
        {
            return stateMachine;
        }

        set
        {
            stateMachine = value;
        }
    }
}
