using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTrackerV3 : MonoBehaviour
{
    private ARTrackedImageManager trackedImages;
    public GameObject[] ArPrefabs;
    private Dictionary<string, GameObject> ARObjects = new Dictionary<string, GameObject>();

    public bool IsTracked { get; private set; }

    public GameObject CloseButton;

    void Awake()
    {
        IsTracked = true;
        trackedImages = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        trackedImages.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImages.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        if (!IsTracked) return;

        foreach (var trackedImage in eventArgs.added)
        {
            CreateOrUpdateObject(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            if (trackedImage.trackingState == TrackingState.Tracking && !ARObjects.ContainsKey(trackedImage.referenceImage.name))
            {
                CreateOrUpdateObject(trackedImage);
            }
        }
    }

    private void CreateOrUpdateObject(ARTrackedImage trackedImage)
    {
        foreach (var arPrefab in ArPrefabs)
        {
            if (trackedImage.referenceImage.name == arPrefab.name)
            {
                GameObject newPrefab = Instantiate(arPrefab, trackedImage.transform);
                ARObjects[trackedImage.referenceImage.name] = newPrefab;
                IsTracked = false;
                CloseButton.SetActive(true);
                return;
            }
        }
    }

    public void DestroyObject()
    {
        CloseButton.SetActive(false);
        foreach (var obj in ARObjects.Values)
        {
            Destroy(obj);
        }
        ARObjects.Clear();
        IsTracked = true;
    }
}
