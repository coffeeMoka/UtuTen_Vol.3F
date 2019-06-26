using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour {
	private static T m_instance;
	public static T instance {
		get {
			if(m_instance == null) {
				System.Type type = typeof(T);
				m_instance = (T)FindObjectOfType(type);
				if(m_instance == null)
					m_instance = new GameObject(type.Name).AddComponent<T>();
			}
			return m_instance;
		}
	}

	virtual protected void Awake() {
		if(this != instance) {
			Destroy(this.gameObject);
			return;
		}
		DontDestroyOnLoad(this.gameObject);
	}
}
