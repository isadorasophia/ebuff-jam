using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PixelMover : MonoBehaviour {

	public static Vector2 pixel = new Vector2(1,1);

	public static void Move(Transform obj, float x, float y)
    {
		Vector2 direction = new Vector2 (x, y);
		direction = direction.normalized;
		obj.Translate (Vector3.right * pixel.x * direction.x + Vector3.up * pixel.y * direction.y);
	}

    public static void Move(Transform obj, float x, float y, float speed)
    {
		Vector2 direction = new Vector2 (x, y);
		direction = direction.normalized;
        obj.Translate(speed * (Vector3.right * pixel.x * direction.x + Vector3.up * pixel.y * direction.y));
    }
}
