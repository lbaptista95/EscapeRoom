using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable
{
    public abstract void Use();
    
    protected Collectable() { }
}

public class Key : Collectable
{
    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}

public class Battery : Collectable
{
    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
