using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float distance = 6.3f;
    public float height = 3.5f;

    public float height_Damping = 3.25f;
    public float Rotation_Damping = 0.27f;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void LateUpdate()
    {
        FollowPlayer();
    }
    void FollowPlayer()
{
    float wanted_Rotationangles = target.eulerAngles.y;
    float wanted_Height = target.position.y + height;

    float current_Rotation_Angle = transform.eulerAngles.y;
    float current_height = transform.position.y;

    current_Rotation_Angle = Mathf.LerpAngle(current_Rotation_Angle, wanted_Rotationangles, Rotation_Damping * Time.deltaTime);
    current_height = Mathf.Lerp(current_height, wanted_Height, height_Damping * Time.deltaTime);

    Quaternion Current_Rotation = Quaternion.Euler(0f, current_Rotation_Angle, 0f);

    Vector3 targetPosition = target.position - Current_Rotation * Vector3.forward * distance;
    targetPosition.y = current_height;

    transform.position = targetPosition;
}

}
