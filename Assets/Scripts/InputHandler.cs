using UnityEngine;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour
{
    [Header("InputEvents")]
    public UnityEvent<Vector2> OnMove = new UnityEvent<Vector2>();
    public UnityEvent OnSpacePressed = new UnityEvent();
    public UnityEvent OnLeftClick = new UnityEvent();

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = Vector2.zero;
        if(Input.GetKey(KeyCode.W))
        {
            moveInput += Vector2.up;
        }
        if(Input.GetKey(KeyCode.S))
        {
            moveInput -= Vector2.up;
        }
        if(Input.GetKey(KeyCode.A))
        {
            moveInput += Vector2.left;
        }
        if(Input.GetKey(KeyCode.D))
        {
            moveInput -= Vector2.left;
        }

        OnMove.Invoke(moveInput.normalized);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpacePressed.Invoke();
        }
        if(Input.GetMouseButtonDown(0))
        {
            OnLeftClick.Invoke();
        }

    }
}
