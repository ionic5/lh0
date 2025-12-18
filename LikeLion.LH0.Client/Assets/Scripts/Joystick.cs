using Platformer;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public SceneType sceneType;

    private AdventurerAttack attack;
    private IMovement movement;

    [SerializeField] private GameObject backgroundUI;
    [SerializeField] private GameObject handleUI;

    [SerializeField] private Button atkButton;
    [SerializeField] private Button jumpButton;

    private Vector2 startPos, currentPos;

    void Awake()
    {
        if (sceneType == SceneType.Town)
        {
            movement = FindFirstObjectByType<TownMovement>() as IMovement;
        }
        else if (sceneType == SceneType.HuntingGround)
        {
            movement = FindFirstObjectByType<AdventurerMovement>() as IMovement;
            attack = FindFirstObjectByType<AdventurerAttack>();

            atkButton.onClick.AddListener(attack.Attack);
            jumpButton.onClick.AddListener(movement.Jump);
        }
    }

    void Start()
    {
        backgroundUI.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        backgroundUI.SetActive(true);
        backgroundUI.transform.position = eventData.position;

        startPos = eventData.position; // 클릭한 처음 위치 저장
    }

    public void OnDrag(PointerEventData eventData)
    {
        currentPos = eventData.position;
        Vector2 dragDir = currentPos - startPos;

        float maxDistance = Mathf.Min(dragDir.magnitude, 75f);

        Vector2 normalDir = dragDir.normalized;

        movement.InputJoystick(normalDir.x, normalDir.y);

        handleUI.transform.position = startPos + normalDir * maxDistance;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        movement.InputJoystick(0, 0);

        handleUI.transform.localPosition = Vector2.zero;
        backgroundUI.SetActive(false);
    }
}