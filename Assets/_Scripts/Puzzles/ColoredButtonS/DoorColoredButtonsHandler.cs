using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorColoredButtonsHandler : MonoBehaviour
{

    [SerializeField] private List<int> sequence = new List<int>();
    [SerializeField] private List<int> userSequence = new List<int>();
    int correctNumbers = 0;

    private AudioSource audioSource;
    [SerializeField] private AudioClip pressedButtonSound;
    private void Update()
    {
        CheckButtonPressing();
    }

    private void CheckButtonPressing()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 10))
        {
            if (hit.collider)
            {
                if (hit.collider.GetComponent<ColoredButtonStorage>() != null)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        ColoredButtonStorage storage = hit.collider.GetComponent<ColoredButtonStorage>();
                        storage.Click();
                        CheckNumber(storage.sequenceIndex);
                    }
                }
            }
        }
    }

    private void CheckNumber(int num)
    {
        if (userSequence.Count < sequence.Count)
        {
            userSequence.Add(num);
        }

        if (userSequence.Count == sequence.Count)
        {
            for (int x = 0; x < sequence.Count; x++)
            {
                if (userSequence[x] == sequence[x])
                {
                    correctNumbers++;
                }
            }

            if (correctNumbers == sequence.Count)
            {
                DoorLockBehaviour.instance?.Energize();
                GameManager.instance.EndInteraction();
            }
            else
            {
                correctNumbers = 0;
                GameManager.instance.ShowErrorMessage("Incorrect Sequence");
                userSequence.Clear();
            }
        }


    }
}
