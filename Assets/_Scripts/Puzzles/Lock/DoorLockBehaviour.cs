using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLockBehaviour : PuzzleObjectBehaviour
{
    public static DoorLockBehaviour instance = null;
    private bool isEnergized;
    [SerializeField] private List<MeshRenderer> wireRenderers = new List<MeshRenderer>();
    [SerializeField] private GameObject doorLockPrefab;
    [SerializeField] private Material onMaterial;

    private AudioSource audioSource;
    [SerializeField] private AudioClip energySound;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Energize()
    {
        audioSource.PlayOneShot(energySound);
        isEnergized = true;
        foreach (MeshRenderer r in wireRenderers)
        {
            r.material = onMaterial;
        }
    }

    public override void Interact()
    {
        if (isEnergized)
            GameManager.instance.InspectObject(doorLockPrefab, "Insert password and Press # to submit or * to clear");
        else
        {
            GameManager.instance.ShowErrorMessage("The door is not energized");
            GameManager.instance.EndInteraction();
        }
    }
}
