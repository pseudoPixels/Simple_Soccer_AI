using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MovingEntity
{
    public PlayerBase(GameObject go, int enitityType, Vector2 velocity, Vector2 heading, double mass, double maxForce, double maxSpeed, double maxTurnRate) : base(go, enitityType, velocity, heading, mass, maxForce, maxSpeed, maxTurnRate)
    {
    }

    public override void UpdateGameEntity(double time_elapsed)
    {
        throw new NotImplementedException();
    }
}
