using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DoorLockButtonsHandler : MonoBehaviour
{
    [SerializeField] private string correctSequence;
    private string userSequence;

    [SerializeField] private TMP_Text userInputText;

    private AudioSource audioSource;

    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip errorSound;
    [SerializeField] private AudioClip correctSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ClearPassword();
    }

    void Update()
    {
        CheckButtonPressing();
    }

    private void CheckButtonPressing()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 11))
        {
            if (hit.collider)
            {
                if (hit.collider.GetComponent<LockButtonStorage>() != null)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        audioSource.PlayOneShot(clickSound);
                        LockButtonStorage storage = hit.collider.GetComponent<LockButtonStorage>();
                        if (storage.value == "#")
                        {
                            SubmitPassword();
                        }
                        else if (storage.value == "*")
                        {
                            ClearPassword();
                        }
                        else
                        {
                            userSequence += storage.value;
                            userInputText.text = userSequence;
                        }                        
                    }
                }
            }
        }
    }

    public void SubmitPassword()
    {
        if (userSequence == correctSequence)
        {
            GameManager.instance.escaped = true;
            GameManager.instance.ShowMainCanvas("YOU ESCAPED");
            audioSource.PlayOneShot(correctSound);
        }
        else
        {
            GameManager.instance.ShowErrorMessage("Incorrect password");
            audioSource.PlayOneShot(errorSound);
            ClearPassword();
        }
    }

    public void ClearPassword()
    {
        userInputText.text = userSequence = string.Empty;
    }
}
