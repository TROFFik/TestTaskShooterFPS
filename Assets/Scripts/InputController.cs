using System;
using UnityEngine;

public partial class InputController : MonoBehaviour
{
    public static InputController Instance { get; private set; }

    public Action<Vector2> rotateAction;
    public Action<Vector3> moveAction;

    public Action clickLeftButtonAction;
    public Action clickRightButtonAction;
    public Action jumpAction;

    private Vector2 _rotationVector = new Vector2();
    private Vector3 _moveVector = new Vector2();

    private void Awake()
    {
        CreateSingleton();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        InputMouse();
        InputKeyboard();
    }

    private void CreateSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InputMouse()
    {
        _rotationVector.x -= Input.GetAxis("Mouse Y") * Time.deltaTime;
        _rotationVector.x = Math.Clamp(_rotationVector.x, -0.3f, 0.3f);
        _rotationVector.y = Input.GetAxis("Mouse X") * Time.deltaTime;

        if (_rotationVector != Vector2.zero)
        {
            rotateAction?.Invoke(_rotationVector);
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            clickLeftButtonAction?.Invoke();
        }

        if (Input.GetMouseButtonDown(1))
        {
            clickRightButtonAction?.Invoke();
        }
    }

    private void InputKeyboard()
    {
        if (Input.anyKey)
        {
            _moveVector = new Vector3(Input.GetAxis("Horizontal"), 0 ,Input.GetAxis("Vertical"));

            moveAction?.Invoke(_moveVector);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpAction?.Invoke();
            }
        }
    }
}
