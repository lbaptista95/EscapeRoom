using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorColoredButtonsBehaviour : PuzzleObjectBehaviour
{
    private bool isEnergized;
    [SerializeField] private MeshRenderer wireRenderer;
    [SerializeField] private Material onMaterial;
    [SerializeField] private GameObject objectToShow;

    private AudioSource audioSource;
    [SerializeField] private AudioClip energySound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Energize()
    {
        audioSource.PlayOneShot(energySound);
        isEnergized = true;
        wireRenderer.material = onMaterial;
    }

    public override void Interact()
    {
        if (isEnergized)
            GameManager.instance.InspectObject(objectToShow, "Insert the correct color sequence");
        else
        {
            GameManager.instance.ShowErrorMessage("This panel is not energized");
            GameManager.instance.EndInteraction();
        }
    }
}
