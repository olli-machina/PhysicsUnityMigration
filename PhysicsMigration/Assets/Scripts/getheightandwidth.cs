using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getheightandwidth : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
	public  Rect Get_Rect(GameObject gameObject)
	{
		if (gameObject != null)
		{
			RectTransform rectTransform = gameObject.GetComponent<RectTransform>();

			if (rectTransform != null)
			{
				return rectTransform.rect;
			}
		}
		else
		{
			Debug.Log("Game object is null.");
		}

		return new Rect();
	}
	public  float Get_Width(Component component)
	{
		if (component != null)
		{
			return Get_Width(component.gameObject);
		}

		return 0;
	}
	public  float Get_Width(GameObject gameObject)
	{
		if (gameObject != null)
		{
			var rect = Get_Rect(gameObject);
			if (rect != null)
			{
				return rect.width;
			}
		}

		return 0;
	}

public  float Get_Height(Component component)
{
	if (component != null)
	{
		return Get_Height(component.gameObject);
	}

	return 0;
}
public  float Get_Height(GameObject gameObject)
{
	if (gameObject != null)
	{
		var rect = Get_Rect(gameObject);
		if (rect != null)
		{
			return rect.height;
		}
	}

	return 0;
}

	// Update is called once per frame
	void Update()
    {
        
    }
}
