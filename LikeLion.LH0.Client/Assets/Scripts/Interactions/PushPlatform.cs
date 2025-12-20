using System.Collections;
using Platformer;
using UnityEngine;

public class PushPlatform : MonoBehaviour, IInteractable
{
    private AdventurerMovement movement;
    [SerializeField]
    private Animator anim;

    private Coroutine pushRoutine;

    [SerializeField] private float pushPower = 50f;

    public bool IsInteracting { get; }

    public void Interact(Transform interactor = null)
    {
        PushRoutine(interactor);
    }

    void PushRoutine(Transform interactor)
    {
        Rigidbody2D rb = interactor.GetComponent<Rigidbody2D>();

        movement = interactor.GetComponent<AdventurerMovement>();
        movement.SetGroundAnimation(true);

        if (rb != null)
        {
            rb.AddForceY(pushPower, ForceMode2D.Impulse); rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * pushPower, ForceMode2D.Impulse);
        }

        anim.SetTrigger("Push");
    }

    public void UnInteract()
    {
        movement.SetGroundAnimation(false);
    }
}