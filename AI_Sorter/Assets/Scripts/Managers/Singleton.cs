using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T instance = null;

	public static T Instance => instance;

	private void Awake() => Init();
	protected virtual void Init()
	{
		if (instance)
		{
			Destroy(this);
			return;
		}

		instance = this as T;
	}

	private void Start() => PostInit();
	protected virtual void PostInit() { }
}
