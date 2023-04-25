using UnityEngine;

public class MovementComponent : Component
{
	[SerializeField] protected Transform head = null;
	[SerializeField] protected CapsuleCollider capsule = null;
	[SerializeField] protected Animator animator = null;
	
	[SerializeField] protected bool canMove = true;
	[SerializeField] protected bool canRotate = true;
	[SerializeField] protected float moveSpeed = 4.0f;
	[SerializeField] protected float rotateSpeed = 5.0f;
	[SerializeField] protected LayerMask terrainLayer = 0;

	private Vector3 lastPosition = Vector3.zero;

	public float GetMoveSpeed => moveSpeed;

	private void Start()
	{
		lastPosition = transform.position;
	}

	private void Update()
	{
		// MoveTowards(Vector3.down * (9.81f * Time.deltaTime), true);
	}

	public bool CheckSafeMove(Vector3 _position, Vector3 _targetPosition)
	{
		Vector3 _direction = _targetPosition - _position;
		
		float _height = capsule.height - 0.02f;
		float _radius = capsule.radius;
		Vector3 _p1 = _position + capsule.center - Vector3.up * (_height / 2.0f - _radius - 0.01f);
		Vector3 _p2 = _p1 + Vector3.up * (_height - _radius * 2.0f - 0.01f);

		return !Physics.CapsuleCast(_p1, _p2, _radius, _direction.normalized, _direction.magnitude + 0.02f, terrainLayer);
	}

	public void Move(Vector3 _direction)
	{
		if (!canMove) _direction = Vector3.zero;
		lastPosition = transform.position;
		transform.position += _direction;
		if (_direction.magnitude <= float.Epsilon)
		{
			animator.SetBool("IsMoving", false);
			return;
		}
		animator.SetBool("IsMoving", true);
		if (_direction != Vector3.zero)
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(_direction), rotateSpeed * Time.deltaTime);
	}

	public void MoveTowards(Vector3 _direction, bool _slide = true)
	{
		if (!SafeMove(_direction, out Vector3 _normal) && _slide)
			SlideAlongSurface(_direction, _normal);
	}
	
	private bool SafeMove(Vector3 _direction, out Vector3 _normal)
	{
		float _height = capsule.height - 0.02f;
		float _radius = capsule.radius;
		Vector3 _p1 = lastPosition + capsule.center - Vector3.up * (_height / 2.0f - _radius - 0.01f);
		Vector3 _p2 = _p1 + Vector3.up * (_height - _radius * 2.0f - 0.01f);
		_normal = Vector3.zero;

		RaycastHit[] _hitResults = Physics.CapsuleCastAll(_p1, _p2, _radius, _direction.normalized, _direction.magnitude + 0.02f, terrainLayer);
		int _count = _hitResults.Length;

		switch (_count)
		{
			case 0:
				Move(_direction);
				return true;
			case > 1:
				foreach (RaycastHit _hitResult in _hitResults)
					_normal += _hitResult.normal;
				_normal += _direction;
				_normal.Normalize();
				return true;
			default:
				_normal = _hitResults[0].normal;
				return false;
		}
	}
	private void SlideAlongSurface(Vector3 _direction, Vector3 _normal)
	{
		SafeMove((_direction + Vector3.Reflect(_direction, _normal)) / 2.0f, out Vector3 _newNormal);
	}
}
