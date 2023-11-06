using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private float _jumpPower = 10.0f;
    [SerializeField] private float _gravity = -9.8f;
    [SerializeField] private float _groundedOffset = -1f;

    private CharacterController _controller;
    private float _verticalMoving = 0;

    private void Start()
    {
        InputController.Instance.moveAction += Move;
        InputController.Instance.jumpAction += Jump;

        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!IsGround())
        {
            _verticalMoving += 2 * _gravity * Time.deltaTime;
        }

        if (_verticalMoving < 0.0f)
        {
            _verticalMoving = _gravity / 3;
        }

        _controller.Move(transform.up * _verticalMoving * Time.deltaTime);
    }

    private void Move(Vector3 value)
    {
        _controller.Move(transform.TransformDirection(value) * _speed * Time.deltaTime);
    }

    private void Jump()
    {
        if (IsGround())
        {
            _verticalMoving = Mathf.Sqrt(_jumpPower * -2f * _gravity);
        }
    }

    private bool IsGround()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit, 1.6f))
        {
            return true;
        }

        return false;
    }

    private void OnDestroy()
    {
        InputController.Instance.moveAction -= Move;
        InputController.Instance.jumpAction -= Jump;
    }
}
