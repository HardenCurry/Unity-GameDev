using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class JoyStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Player_CT player;
    [SerializeField] RectTransform lever;
    RectTransform rectTransform;

    [SerializeField, Range(10, 150)]
    private float leverRange;

    private Vector3 inputDirection;
    public bool isInput;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        DragFunc(eventData);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        DragFunc(eventData);

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
        isInput = false;
        player.JoystickMove(Vector3.zero);
    }
    void DragFunc(PointerEventData eventData)
    {
        var inputPos = eventData.position - rectTransform.anchoredPosition;
        var inputVector = lever.anchoredPosition = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;
        lever.anchoredPosition = inputVector;
        inputDirection = inputVector / leverRange;
        isInput = true;
    }
    private void Update()
    {
        if (isInput && player.Isalive)
        {
            player.JoystickMove(inputDirection);
        }
    }
}
