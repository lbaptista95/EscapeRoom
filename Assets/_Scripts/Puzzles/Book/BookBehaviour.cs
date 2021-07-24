using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBehaviour : PuzzleObjectBehaviour
{
    [SerializeField] private GameObject bookToShow;

    public override void Interact()
    {
        GameManager.instance.InspectObject(bookToShow, "< - Previous Page   > - NextPage");
    }


   
}
