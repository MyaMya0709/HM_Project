using TMPro;
using UnityEngine;

public class DamageLabel : MonoBehaviour
{
    [Header("DamageLabel")]
    public TMP_Text damageText;
    public float fontSize = 10f;

    [Header("Anim easing")]
    public AnimationCurve easeCurve;
    private float _displayDuration;

    [Header("Bezier curve settings")]
    public Vector2 highPointOffset = new Vector2(-350, 300);
    public Vector2 lowPointOffset = new Vector2(-100, -500);
    public float heightVariationMax = 150;
    public float heightVariationMin = 50;

    private Vector3 _highPointOffsetBasedOnDir = Vector3.zero;
    private Vector3 _dropPointOffsetBasedOnDir = Vector3.zero;
    private bool _dir = true;

    [Header("Visualize")]
    public bool disPlayGizmos;
    public int gizmoResolution = 20;
    private Vector3 startingPositionForVisualization = Vector3.zero;

    
}
