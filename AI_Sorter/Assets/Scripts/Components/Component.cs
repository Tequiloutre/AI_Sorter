using UnityEngine;

public class Component : MonoBehaviour
{
	[SerializeField] protected bool debug = false;
	[SerializeField] protected Entity entity = null;

	public Entity GetEntity => entity;

	private void Awake() => Init();
	protected virtual void Init() { }

	private void Start() => PostInit();
	protected virtual void PostInit() { }

	private void OnDrawGizmos()
	{
		if (!debug) return;
		DrawDebug();
	}
	protected virtual void DrawDebug() { }
}
