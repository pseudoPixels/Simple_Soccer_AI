using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State <entityType>  {

    public abstract void Enter(entityType ent);

    public abstract void Execute(entityType ent);

    public abstract void Exit(entityType ent);

    public abstract bool OnMessage(entityType ent, Telegram msg);


}
