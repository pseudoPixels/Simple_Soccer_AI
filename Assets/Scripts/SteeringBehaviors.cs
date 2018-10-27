using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Behavior_type
{
    none,
    seek,
    arrive,
    separation,
    pursuit,
    interpose
}


public class SteeringBehaviors
{
    //references to the objects
    PlayerBase player;
    SoccerPitch world;
    SoccerBall ball;


    //current target
    private Vector2 currentTarget = new Vector2(0,0);




    bool behavior_seek;
    bool behavior_arrive;
    bool behavior_separation;
    bool behavior_pursuit;
    bool behavior_interpose;



    public SteeringBehaviors(PlayerBase agent, SoccerPitch pitch, SoccerBall soccerBall)
    {
        this.player = agent;
        this.world = pitch;
        this.ball = soccerBall;
    }



    //======================================================================
    //Different Steering Behaviors starts
    //======================================================================


    //**********************************************************************
    //                           Seek
    //**********************************************************************
    //Seek towards a target 
    public Vector2 Seek(Vector2 targetPosition)
    {
        Vector2 desiredVelocity = targetPosition - this.player.GetPosition();
        desiredVelocity = desiredVelocity.normalized;
        desiredVelocity = desiredVelocity * (float)this.player.GetMaxSpeed();

        return desiredVelocity - this.player.GetVelocity();

    }






    //**********************************************************************
    //                           Flee
    //**********************************************************************
    //Flee from a target, which is just opposite to Seek behavior
    public Vector2 Flee(Vector2 targetPosition)
    {
        Vector2 desiredVelocity = this.player.GetPosition() - targetPosition;
        desiredVelocity = desiredVelocity.normalized;
        desiredVelocity = desiredVelocity * (float)this.player.GetMaxSpeed();

        return desiredVelocity - this.player.GetVelocity();

    }







    //**********************************************************************
    //                           Arrive
    //**********************************************************************
    //Arrive to the target gently. The arrival can be tuned with second parameter
    //de-acceleration rates: 3=> slow, 2=> normal, 1=> fast
    //TODO: create enemurated list for de_accelerationRate
    public Vector2 Arrive(Vector2 targetPosition, int de_accelerationRate)
    {
        Vector2 toTarget = targetPosition - this.player.GetPosition();

        // calculate the distance to the target
        float dist = toTarget.magnitude;

        if (dist > 0)
        {
            //because Deceleration is enumerated as an int, this value is required
            //to provide fine tweaking of the deceleration..
            float de_accelerationTweaker = 0.3f;

            //calculate the speed required to reach the target given the desired
            //deceleration
            float speed = dist / (de_accelerationRate * de_accelerationTweaker);

            //we make sure the velocity does not exceed its max speed
            speed = Mathf.Min(speed, (float)this.player.GetMaxSpeed());


            Vector2 desiredVelocity = toTarget * speed / dist;

            return desiredVelocity - this.player.GetVelocity();

        }

        return new Vector2(0, 0);
    }









    //**********************************************************************
    //                           Pursuit
    //**********************************************************************
    //this behavior creates a force that steers the agent towards the 
    //evader
    public Vector2 Pursuit(PlayerBase evader)
    {
        //if the evader is ahead and facing the agent then we can just seek
        //for the evader's current position.
        Vector2 toEvader = evader.GetPosition() - this.player.GetPosition();


        float relativeHeading = Vector2.Dot(this.player.GetHeading(), evader.GetHeading());

        if (Vector2.Dot(toEvader, this.player.GetHeading()) > 0 && relativeHeading < -0.95)//acos(0.95)=18 degs
        {
            return Seek(evader.GetPosition());
        }


        //Not considered ahead so we predict where the evader will be.

        //the lookahead time is propotional to the distance between the evader
        //and the pursuer; and is inversely proportional to the sum of the
        //agent's velocities
        float lookAheadTime = (float)(toEvader.magnitude /
                              (this.player.GetMaxSpeed() + evader.GetSpeed()));

        //now seek to the predicted future position of the evader
        return Seek(evader.GetPosition() + evader.GetVelocity() * lookAheadTime);
    }


    //**********************************************************************
    //                           Evade
    //**********************************************************************
    //similar to pursuit except the agent Flees from the estimated future
    //  position of the pursuer
    public Vector2 Evade(PlayerBase pursuer)
    {
        /* Not necessary to include the check for facing direction this time */
        Vector2 toPursuer = pursuer.GetPosition() - this.player.GetPosition();

        //uncomment the following two lines to have Evade only consider pursuers 
        //within a 'threat range'
        float threatRange = 100.0f;
        if (toPursuer.magnitude > threatRange) return new Vector2(0, 0);

        //the lookahead time is propotional to the distance between the pursuer
        //and the pursuer; and is inversely proportional to the sum of the
        //agents' velocities
        float lookAheadTime = (float)(toPursuer.magnitude / (this.player.GetMaxSpeed() + pursuer.GetSpeed()));

        //now flee away from predicted future position of the pursuer
        return Flee(pursuer.GetPosition() + pursuer.GetVelocity() * lookAheadTime);

    }




    private Vector2 SumForces()
    {
        Vector2 summedForces = new Vector2(0,0);

        if (this.behavior_seek)
        {
            summedForces += Seek(currentTarget);
        }

        if (this.behavior_arrive)
        {
            summedForces += Arrive(currentTarget, 1);
        }



        return summedForces;
    }





    public bool Behavior_seek
    {
        get
        {
            return behavior_seek;
        }

        set
        {
            behavior_seek = value;
        }
    }

    public bool Behavior_arrive
    {
        get
        {
            return behavior_arrive;
        }

        set
        {
            behavior_arrive = value;
        }
    }

    public bool Behavior_separation
    {
        get
        {
            return behavior_separation;
        }

        set
        {
            behavior_separation = value;
        }
    }

    public bool Behavior_pursuit
    {
        get
        {
            return behavior_pursuit;
        }

        set
        {
            behavior_pursuit = value;
        }
    }

    public bool Behavior_interpose
    {
        get
        {
            return behavior_interpose;
        }

        set
        {
            behavior_interpose = value;
        }
    }
}