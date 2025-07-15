using UnityEngine;

public class UI : MonoBehaviour
{
    protected GameObject panel;

    protected virtual void Awake()
    {
        panel = transform.gameObject;
    }

    public void SetCanvas(int order)
    {
        GetComponent<Canvas>().sortingOrder = order;
    }

    public virtual void Show()
    {
        panel.SetActive(true);
    }

    public virtual void Hide()
    {
        panel.SetActive(false);
    }

    public virtual void Close()
    {

    }

    public virtual void Clear()
    {

    }
}