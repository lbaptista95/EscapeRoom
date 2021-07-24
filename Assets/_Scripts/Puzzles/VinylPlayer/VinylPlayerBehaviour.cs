using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinylPlayerBehaviour : PuzzleObjectBehaviour
{
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public override void Interact()
    {
        source.Play();
        GameManager.instance.EndInteraction();
    }
}
