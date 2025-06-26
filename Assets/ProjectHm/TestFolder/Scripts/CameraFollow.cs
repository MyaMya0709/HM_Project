using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // ���� 2D ������Ʈ
    public float smoothSpeed = 0.125f; // �ε巯�� �̵� �ӵ�
    public Vector3 offset; // ī�޶�� ��ǥ ������ �Ÿ� offset

    void Update()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        //transform.position = smoothedPosition;
        transform.position = desiredPosition;
    }
}
