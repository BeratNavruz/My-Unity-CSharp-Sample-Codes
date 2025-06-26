using UnityEngine;
using DG.Tweening;

public class PathDOTweenScript : MonoBehaviour
{
    private Transform pathParent;
    private Vector3[] pathArray;
    public bool IsSetLookAt;
    public float Time;

    private void Start()
    {
        pathParent = transform;
        pathArray = new Vector3[pathParent.childCount];
        for (int i = 0; i < pathArray.Length; i++)
        {
            pathArray[i] = pathParent.GetChild(i).position;
        }
    }

    public void PathFunc(Transform T_Object)
    {
        if (IsSetLookAt)
        {
            T_Object.DOPath(pathArray, Time, PathType.CatmullRom).SetLookAt(0.001f);
        }
        else
        {
            T_Object.DOPath(pathArray, Time, PathType.CatmullRom);
        }
    }
}
