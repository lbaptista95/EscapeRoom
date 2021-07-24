using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance = null;
    private Camera playerCam;

    [SerializeField] private List<Collectable> collectables = new List<Collectable>();
    public List<Collectable> Collectables { get { return collectables; } }

    [SerializeField] private GameObject collectableImgPrefab;
    [SerializeField] private Transform collectablesSpriteParent;

    private AudioSource audioSource;
    [SerializeField] private AudioClip keySound;
    [SerializeField] private AudioClip batterySound;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        playerCam = GetComponentInChildren<Camera>();

        audioSource = GetComponent<AudioSource>();
    }

    public void UseDisposableCollectable(Collectable selectedCollectable)
    {
        Collectable col = collectables.Find(x => x.GetType() == selectedCollectable.GetType());
        if (col != null)
        {
            collectables.Remove(col);
        }

        for (int x = 0; x < collectablesSpriteParent.childCount; x++)
        {
            CollectableStorage storage = collectablesSpriteParent.GetChild(x).GetComponent<CollectableStorage>();
            if (storage != null)
            {
                if(storage.collectable.GetType() == selectedCollectable.GetType())
                {
                    Destroy(storage.gameObject);
                }
            }
        }
    }
    public void GetCollectable(Collectable col)
    {
        collectables.Add(col);

        if (col.GetType() == typeof(Battery))
            PlayBatterySound();
        else if (col.GetType() == typeof(Key))
            PlayKeySound();
    }

    private void Update()
    {
        CheckCollectables();
    }

    private void CheckCollectables()
    {
        RaycastHit hit;
        Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 8))
        {
            if (hit.collider)
            {
                GameObject go = hit.collider.gameObject;

                if (go.GetComponent<CollectableStorage>() != null)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        CollectableStorage colStorage = go.GetComponent<CollectableStorage>();
                        GetCollectable(colStorage.collectable);
                        ShowCollectableImg(colStorage);
                        Destroy(go);
                    }
                }
            }
        }
    }

    private void ShowCollectableImg(CollectableStorage storage)
    {
        GameObject collectableIcon = Instantiate(collectableImgPrefab, collectablesSpriteParent);
        Image img = collectableIcon.GetComponent<Image>();
        img.sprite = GameManager.instance.GetCollectableSprite(storage.type);
        collectableIcon.GetComponent<CollectableStorage>().type = storage.type;
        img.preserveAspect = true;
    }

    private void PlayKeySound()
    {
        audioSource.PlayOneShot(keySound);
    }
    private void PlayBatterySound()
    {
        audioSource.PlayOneShot(batterySound);
    }
}
