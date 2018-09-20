using UnityEngine;
using System.Collections;

[System.Serializable]
public class Size {
	public float width, height;

	public Size() {}

	public Size(float width, float height) {
		this.width = width;
		this.height = height;
	}
}
