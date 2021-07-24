using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatterySlotBehaviour : PuzzleObjectBehaviour
{
    [SerializeField] private Transform batterySlot;
    [SerializeField] private GameObject batteryPrefab;

    [SerializeField] private DoorColoredButtonsBehaviour coloredBtnsBehaviour;

    private AudioSource audioSource;
    [SerializeField] private AudioClip batterySound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public override void Interact()
    {
        Collectable collectable = PlayerInventory.instance.Collectables.Find(x => x.GetType() == typeof(Battery));
        if (collectable != null)
        {
            audioSource.PlayOneShot(batterySound);
            PlayerInventory.instance.UseDisposableCollectable(collectable);
            GameObject batteryInstance = Instantiate(batteryPrefab, batterySlot);
            batteryInstance.transform.localPosition = Vector3.zero;
            batteryInstance.transform.localRotation = Quaternion.identity;
            batteryInstance.transform.localScale = Vector3.one;
            coloredBtnsBehaviour.Energize();
            GameManager.instance.EndInteraction();
        }
        else
        {
            GameManager.instance.ShowErrorMessage("You don't have a battery");
            GameManager.instance.EndInteraction();
        }
    }
}
