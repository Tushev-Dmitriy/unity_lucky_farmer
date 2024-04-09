using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private RectTransform joystickBackground;
    private RectTransform joystick;
    private Image buttonImage;

    public GameObject target;
    public Sprite pressedSprite;
    public Sprite normalSprite;

    private Vector2 moveDirection = Vector2.zero;

    void Start()
    {
        joystickBackground = GetComponent<RectTransform>();
        joystick = transform.GetChild(0).GetComponent<RectTransform>();
        buttonImage = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = Vector2.zero;
        buttonImage.sprite = pressedSprite;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground, eventData.position, eventData.pressEventCamera, out position))
        {
            // Нормализуем координаты в диапазоне [-1, 1]
            position.x = Mathf.Clamp(position.x / (joystickBackground.sizeDelta.x / 2), -1f, 1f);
            position.y = Mathf.Clamp(position.y / (joystickBackground.sizeDelta.y / 2), -1f, 1f);

            // Вычисляем позицию джойстика относительно фона джойстика
            Vector2 inputVector = new Vector2(position.x, position.y);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            // Устанавливаем позицию джойстика
            joystick.anchoredPosition = new Vector2(inputVector.x * (joystickBackground.sizeDelta.x / 3), inputVector.y * (joystickBackground.sizeDelta.y / 3));

            // Устанавливаем направление перемещения
            moveDirection = inputVector;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        buttonImage.sprite = normalSprite;
        joystick.anchoredPosition = Vector2.zero;
        moveDirection = Vector2.zero;
    }

    void Update()
    {
        if (moveDirection != Vector2.zero)
        {
            MoveTarget(moveDirection);
        }
    }

    private void MoveTarget(Vector2 direction)
    {
        target.transform.Translate(new Vector3(direction.x, 0, direction.y) * Time.deltaTime, Space.World);
    }
}
