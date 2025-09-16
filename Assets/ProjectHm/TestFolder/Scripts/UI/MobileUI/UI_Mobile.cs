using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Mobile : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Button rightBtn;
    [SerializeField] private Button leftBtn;
    [SerializeField] private Button upBtn;
    [SerializeField] private Button downBtn;
    [SerializeField] private Button attackBtn;
    [SerializeField] private Button skillBtn;

    private void OnRightClick() => player.RightMove();
    private void OnLeftClick() => player.LeftMove();
    private void OnUpClick() => player.DoJump();
    private void OnDownClick() { /*player.OnDownAttack();*/ }
    private void OnAttackClick() { /*player.OnAttack();*/ }
    // private void OnSkillClick()   => 스킬 사용 함수;

    private void OnEnable()
    {
        rightBtn.onClick.AddListener(OnRightClick);
        leftBtn.onClick.AddListener(OnLeftClick);
        upBtn.onClick.AddListener(OnUpClick);
        downBtn.onClick.AddListener(OnDownClick);
        attackBtn.onClick.AddListener(OnAttackClick);
        // skillBtn.onClick.AddListener(OnSkillClick);
    }

    private void OnDisable()
    {
        rightBtn.onClick.RemoveListener(OnRightClick);
        leftBtn.onClick.RemoveListener(OnLeftClick);
        upBtn.onClick.RemoveListener(OnUpClick);
        downBtn.onClick.RemoveListener(OnDownClick);
        attackBtn.onClick.RemoveListener(OnAttackClick);
        // skillBtn.onClick.RemoveListener(OnSkillClick);
    }


}
