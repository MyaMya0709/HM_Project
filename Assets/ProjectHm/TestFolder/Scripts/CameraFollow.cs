using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 따라갈 2D 오브젝트
    public float smoothSpeed = 0.125f; // 부드러운 이동 속도
    public Vector3 offset; // 카메라와 목표 사이의 거리 offset

    void Update()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        //transform.position = smoothedPosition;
        transform.position = desiredPosition;
    }
}
