using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonHandler : MonoBehaviour
{
    private Animator anim;
    private AudioSource audioSource;

    [SerializeField] private AudioClip buttonSound;
    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        CheckPlayButton();
    }

    private void CheckPlayButton()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 9))
        {
            if (hit.collider)
            {
                if (Input.GetMouseButtonDown(0))
                    anim.SetTrigger("play");
            }
        }
    }

    public void PlayButtonSound()
    {
        audioSource.PlayOneShot(buttonSound);
    }
}
