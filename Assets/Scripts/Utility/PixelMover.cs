using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PixelMover : MonoBehaviour {

	public static Vector2 delta = new Vector2(1,1) * .5f;

	public static void Move(Transform obj, float x, float y)
    {
		obj.Translate (Vector3.right * delta.x * x + Vector3.up * delta.y * y);
	}

    public static void Move(Transform obj, float x, float y, float speed)
    {
        obj.Translate(speed * (Vector3.right * delta.x * x + Vector3.up * delta.y * y));
    }
}
