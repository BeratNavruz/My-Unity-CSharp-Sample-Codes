using Meta.XR.MRUtilityKit;
using UnityEngine;
using TMPro;

public class DisplayLabel : MonoBehaviour
{
    public Transform rayStartPoint;
    public float rayLength = 5;
    public MRUKAnchor.SceneLabels labelFilter;
    public TextMeshPro debugText;

    [System.Obsolete]
    void Update()
    {
        Ray ray = new Ray(rayStartPoint.position, rayStartPoint.forward);

        MRUKRoom room = MRUK.Instance.GetCurrentRoom();

        bool hasHit = room.Raycast(ray, rayLength, LabelFilter.FromEnum(labelFilter), out RaycastHit hit, out MRUKAnchor anchor);

        if (hasHit)
        {
            Vector3 hitPoint = hit.point;
            Vector3 hitNormal = hit.normal;

            string label = anchor.AnchorLabels[0];

            if (label == "WALL_FACE")
            {
                debugText.color = Color.red;
                label = "Duvar";
            }
            else if (label == "CEILING")
            {
                debugText.color = Color.blue;
                label = "Tavan";
            }
            else if (label == "FLOOR")
            {
                debugText.color = Color.green;
                label = "Zemin";
            }
            else
            {
                debugText.color = Color.yellow;
                label = "Diğer Objeler";
            }

            debugText.transform.position = hitPoint;
            debugText.transform.rotation = Quaternion.LookRotation(-hitNormal);

            debugText.text = "Anchor: " + label;
        }
    }
}
