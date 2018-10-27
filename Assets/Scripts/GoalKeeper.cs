using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalKeeper : PlayerBase
{
    public GoalKeeper(GameObject go, int enitityType, Vector2 velocity, Vector2 heading, double mass, double maxForce, double maxSpeed, double maxTurnRate) : base(go, enitityType, velocity, heading, mass, maxForce, maxSpeed, maxTurnRate)
    {
    }
}
