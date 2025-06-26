using System;
using UnityEngine;

public class CardFace3 : MonoBehaviour
{
    float rotAngle;
    float animationTime;
    float elapsedTime;
    float originalAngle;
    bool rotating;
    Action cardRotated;

    public void Rotate(float angle, float animationTime, Action rotated)
    {
        cardRotated = rotated;
        originalAngle = transform.rotation.eulerAngles.y;
        rotAngle = angle;
        this.animationTime = animationTime;
        rotating = true;
        elapsedTime = 0;
    }

    private void Update()
    {
        if (rotating)
        {
            elapsedTime += Time.deltaTime;
            float timePercentage = elapsedTime / animationTime;
            if (timePercentage >= 1)
                timePercentage = 1;
            float targetAngle = rotAngle * timePercentage + originalAngle;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
            if (timePercentage == 1)
            {
                rotating = false;
                cardRotated.Invoke();
            }
        }
    }
}
