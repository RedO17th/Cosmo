using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joistick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image joistickBG;
    private Image joistick;
    private Vector2 inputVector;

    private bool canSh = true;
    void Start()
    {
        joistickBG = GetComponent<Image>();
        joistick = transform.GetChild(0).GetComponent<Image>();
    }
    public virtual void OnPointerDown(PointerEventData data)
    {
        OnDrag(data);
        canSh = !canSh;
    }
    public void OnPointerUp(PointerEventData data)
    {
        inputVector = Vector2.zero;
        joistick.rectTransform.anchoredPosition = Vector2.zero;
        canSh = !canSh;
    }
    public void OnDrag(PointerEventData data)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joistickBG.rectTransform, data.position, data.pressEventCamera, out pos))
        {
            pos.x = (pos.x / joistickBG.rectTransform.sizeDelta.x);
            pos.y = (pos.y / joistickBG.rectTransform.sizeDelta.x);

            inputVector = new Vector2(pos.x * 2f, pos.y * 2f);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            joistick.rectTransform.anchoredPosition = new Vector2(inputVector.x * (joistickBG.rectTransform.sizeDelta.x / 4), inputVector.y * (joistickBG.rectTransform.sizeDelta.y / 4));
        }
    }
    public float Horizontal()
    {
        if (inputVector.x != 0) return inputVector.x;
        else return Input.GetAxis("Horizontal");
    }
    public float Vertical()
    {
        if (inputVector.y != 0) return inputVector.y;
        else return Input.GetAxis("Vertical");
    }
    public bool CanShoot()
    {
        return canSh;
    }
}
