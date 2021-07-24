using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBookHandler : MonoBehaviour
{
    [SerializeField] private List<GameObject> leftPages = new List<GameObject>();
    [SerializeField] private List<GameObject> rightPages = new List<GameObject>();

    [SerializeField] private GameObject keyObj;
    [SerializeField] private GameObject batteryObj;
    private int index = 0;

    private AudioSource audioSource;
    
    [SerializeField] private AudioClip pageSound;
    private void Start()
    {
        for (int x = 0; x < leftPages.Count; x++)
        {
            leftPages[x].SetActive(x == 0);
        }
        for (int x = 0; x < rightPages.Count; x++)
        {
            rightPages[x].SetActive(x == 0);
        }

        if (PlayerInventory.instance.Collectables.Find(x => x.GetType() == typeof(Key)) != null)
            Destroy(keyObj);

        if (PlayerInventory.instance.Collectables.Find(x => x.GetType() == typeof(Battery)) != null)
            Destroy(batteryObj);

        audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            NextPages();
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            PreviousPages();
    }

    private void NextPages()
    {
        if (index < leftPages.Count - 1 && index < rightPages.Count - 1)
        {
            index++;
            for (int x = 0; x < leftPages.Count; x++)
            {
                leftPages[x].SetActive(x == index);
            }
            for (int x = 0; x < rightPages.Count; x++)
            {
                rightPages[x].SetActive(x == index);
            }

            PlayPageSound();
        }
    }

    private void PreviousPages()
    {
        if (index > 0 && index > 0)
        {
            index--;
            for (int x = 0; x < leftPages.Count; x++)
            {
                leftPages[x].SetActive(x == index);
            }
            for (int x = 0; x < rightPages.Count; x++)
            {
                rightPages[x].SetActive(x == index);
            }

            PlayPageSound();
        }
    }

    private void PlayPageSound()
    {
        audioSource.PlayOneShot(pageSound);
    }
  
}
