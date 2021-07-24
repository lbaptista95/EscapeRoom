using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableTypes { KEY, BATTERY }
public class CollectableStorage : MonoBehaviour
{
    public CollectableTypes type;
    public Collectable collectable;
    private void Start()
    {
        switch(type)
        {
            case CollectableTypes.KEY:
                collectable = new Key();
                break;
            case CollectableTypes.BATTERY:
                collectable = new Battery();
                break;
        }

    }
}
