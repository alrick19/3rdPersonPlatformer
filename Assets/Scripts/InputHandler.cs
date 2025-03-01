using UnityEngine;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour
{
    [Header("InputEvents")]
    public UnityEvent<Vector2> OnMove = new UnityEvent<Vector2>();
    public UnityEvent OnSpacePressed = new UnityEvent();
    public UnityEvent OnDash = new UnityEvent();

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector2 moveInput = new Vector2(moveX, moveY);

        OnMove.Invoke(moveInput);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpacePressed.Invoke();
        }
        if(Input.GetMouseButtonDown(0))
        {
            OnDash.Invoke();
        }

    }
}
