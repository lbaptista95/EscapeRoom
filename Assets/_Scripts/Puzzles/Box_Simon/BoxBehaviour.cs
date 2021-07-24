using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BoxBehaviour : PuzzleObjectBehaviour
{
    [SerializeField] private bool isOpen;
    [SerializeField] private GameObject objToShow;
    private Key _key;
    private Animator anim;

    private AudioSource audioSource;
    [SerializeField] private AudioClip unlockSound;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        GameManager.OnInspectionClosed += GameManager_OnInspectionClosed;
    }
    private void OnDisable()
    {
        GameManager.OnInspectionClosed -= GameManager_OnInspectionClosed;
    }
    public override void Interact()
    {

        Collectable col = PlayerInventory.instance.Collectables.Find(x => x.GetType() == typeof(Key));

        if (col == null)
        {
            GameManager.instance.ShowErrorMessage("You don't have the key");
            GameManager.instance.EndInteraction();
            return;
        }

        _key = col as Key;

        anim.SetBool("open", true);
    }

    private void GameManager_OnInspectionClosed()
    {
        anim.SetBool("open", false);
    }   

    public void InspectObj()
    {
        GameManager.instance.InspectObject(objToShow, "Press the PLAY button");
    }

    public void PlayUnlockSound()
    {
        audioSource.PlayOneShot(unlockSound);
    }
}
