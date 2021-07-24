using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{

    [SerializeField] private GameObject callToAction;

    private Camera playerCam;
    private bool canInteract;
    private bool canCheckObject = true;
    private PuzzleObjectBehaviour objectToInteract;

    private void OnEnable()
    {
        GameManager.OnInspectionClosed += GameManager_OnInspectionClosed;
    }

    private void OnDisable()
    {
        GameManager.OnInspectionClosed -= GameManager_OnInspectionClosed;
    }

    private void GameManager_OnInspectionClosed()
    {
        canCheckObject = true;
        objectToInteract = null;
        canInteract = false;
    }

    void Start()
    {
        playerCam = GetComponentInChildren<Camera>();
        callToAction.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RayCastToObject();

        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            InspectObject();
        }
    }

    private void RayCastToObject()
    {
        RaycastHit hit;
        Ray ray = playerCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out hit, 1, 1 << 7) && canCheckObject)
        {
            if (hit.collider)
            {
                callToAction.SetActive(true);
                objectToInteract = hit.collider.gameObject.GetComponent<PuzzleObjectBehaviour>();
                canInteract = true;
            }
        }
        else
        {
            canInteract = false;
            objectToInteract = null;
            callToAction.SetActive(false);
        }
    }

    private void InspectObject()
    {
        canCheckObject = false;
        objectToInteract?.Interact();
    }
}
