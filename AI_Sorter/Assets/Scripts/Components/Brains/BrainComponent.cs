using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BrainComponent : Component
{
	[SerializeField] protected string activeStateID = "";
	[SerializeField] protected float minIdleTime = 1.0f,
		maxIdleTime = 5.0f;

	protected Dictionary<NPCState, State> states = new Dictionary<NPCState, State>();
	protected State activeState = null;
	protected NavMeshPath path = null;
	protected int pathIndex = 0;

	public Character GetCharacter => (Character) entity;
	public float GetMinIdleTime => minIdleTime;
	public float GetMaxIdleTime => maxIdleTime;
	public State GetState(NPCState _stateID) => states[_stateID];
	public bool GetNextPathPoint(Vector3 _position, out Vector3 _point)
	{
		while (pathIndex < path.corners.Length)
		{
			_point = path.corners[pathIndex];
			if (Vector3.Distance(_position.ResetY(), _point.ResetY()) > 0.1f) return true;
			++pathIndex;
		}
		_point = Vector3.zero;
		return false;
	}

	public void ComputePath(Vector3 _position, Vector3 _targetPosition)
	{
		if (!NavMesh.SamplePosition(_targetPosition, out NavMeshHit _hit, Mathf.Infinity, NavMesh.AllAreas)) return;
		Vector3 _nearestPoint = _hit.position;
		NavMesh.CalculatePath(_position, _nearestPoint, NavMesh.AllAreas, path);
	}
	public void IncrementPathIndex() => ++pathIndex;
	public void ResetPathIndex() => pathIndex = 0;

	protected override void Init()
	{
		base.Init();

		foreach (State _state in states.Values)
		{
			_state.OnStateChanged += OnStateChanged;
			_state.Init(this);
		}

		path = new NavMeshPath();
	}

	public void SetActiveState(NPCState _stateID)
	{
		if (!states.ContainsKey(_stateID)) return;
		if (activeState != null)
		{
			activeState.Exit();
			activeState.SetActive(false);
		}
		activeState = states[_stateID];
		activeStateID = activeState.GetID.ToString();
		activeState.SetActive(true);
		activeState.Enter();
	}

	private void Update()
	{
		activeState?.Update();
	}

	private void OnStateChanged(State _state)
	{
		if (debug) Debug.Log($"[StateMachine] Enter state : {_state.GetID}");
	}

	protected override void DrawDebug()
	{
		base.DrawDebug();
		if (path == null) return;
		Vector3[] _points = path.corners;
		int _count = _points.Length;
		if (_count == 0) return;
		for (int i = 0; i < _count - 1; ++i)
		{
			Gizmos.color = Color.cyan;
			Gizmos.DrawLine(_points[i], _points[i + 1]);
			Gizmos.color = Color.yellow;
			Gizmos.DrawSphere(_points[i], 0.1f);
		}
		Gizmos.DrawSphere(_points[_count - 1], 0.1f);
	}
}
