using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredButtonStorage : MonoBehaviour
{
    public int sequenceIndex;

    private Animator anim;
    private AudioSource audioSource;
    [SerializeField] private AudioClip clickSound;
    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    public void Click()
    {
       anim.SetTrigger("click");
    }

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }

}
