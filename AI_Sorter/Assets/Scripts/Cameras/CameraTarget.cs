using UnityEngine;

public class CameraTarget : MonoBehaviour
{
	[SerializeField] private float moveSpeed = 8.0f;

	private float inputVertical = 0.0f,
		inputHorizontal = 0.0f;

	private void OnInputVertical(float _input) => inputVertical = _input;
	private void OnInputHorizontal(float _input) => inputHorizontal = _input;

	private void Start()
	{
		InputManager.Instance.OnInputVertical += OnInputVertical;
		InputManager.Instance.OnInputHorizontal += OnInputHorizontal;
	}

	private void Update()
	{
		Transform _transform = transform;
		Vector3 _inputDirection = _transform.forward * inputVertical + _transform.right * inputHorizontal;
		_inputDirection = (_inputDirection.magnitude <= 1.0f ? _inputDirection : _inputDirection.normalized) * (moveSpeed * Time.deltaTime);
		Move(_inputDirection);
	}

	private void Move(Vector3 _direction)
	{
		transform.position += _direction;
	}
}
