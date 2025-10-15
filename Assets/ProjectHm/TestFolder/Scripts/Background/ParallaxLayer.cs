using UnityEngine;

[ExecuteInEditMode]
public class ParallaxLayer : MonoBehaviour
{
    // 이동거리에 따른 비례
    public float parallaxFactor;

    // 플레이어 이동 시, 비례에 따른 거리로 이동
    public void Move(float delta)
    {
        Vector3 newPos = transform.localPosition;
        newPos.x -= delta * parallaxFactor;

        transform.localPosition = newPos;
    }
}
