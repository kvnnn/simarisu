using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class StageCell : GameMonoBehaviour, IPointerEnterHandler
{
	[SerializeField]
	private bool isAvailable = true;
	private Vector2? position = null;

	private Color beforeColor;
	private bool wasArrowShown;

	private SpriteRenderer spriteRenderer
	{
		get {return gameObject.GetComponent<SpriteRenderer>();}
	}

	private Transform arrowTransform
	{
		get {return transform.Find("arrow").transform;}
	}

	public System.Action<StageCell> onPointerEnter;

	private static readonly Color SELECTED_COLOR = new Color(255f/255f, 150f/255f, 150f/255f, 1f);
	private static readonly Color RANGE_COLOR = new Color(50f/255f, 255f/255f, 50/255f, 1f);
	private static readonly Vector3[][] ARROW_ROTATE_LIST = new Vector3[][]
	{
		new Vector3[]{new Vector3(0f,0f,0f), new Vector3(0f,0f,45f), new Vector3(0f,0f,90f)},
		new Vector3[]{new Vector3(0f,0f,315f), new Vector3(0f,0f,0f), new Vector3(0f,0f,135f)},
		new Vector3[]{new Vector3(0f,0f,270f), new Vector3(0f,0f,225f), new Vector3(0f,0f,180f)}
	};

	public Vector2 PositionInWorld()
	{
		return transform.position;
	}

	public Vector2 Position()
	{
		if (position == null)
		{
			position = CustomVector.GetFromString(gameObject.name);
		}
		return (Vector2)position;
	}

	public bool IsAvailable()
	{
		return isAvailable;
	}

	public void SetArrow(Vector2 direction)
	{
		direction += Vector2.one;
		arrowTransform.gameObject.SetActive(true);
		arrowTransform.localEulerAngles = ARROW_ROTATE_LIST[(int)direction.x][(int)direction.y];
	}

	public void SetSelectColor()
	{
		spriteRenderer.color = SELECTED_COLOR;
	}

	public void UnsetSelectColor()
	{
		spriteRenderer.color = Color.white;
		arrowTransform.gameObject.SetActive(false);
	}

	public void SetRangeColor()
	{
		beforeColor = spriteRenderer.color;
		wasArrowShown = arrowTransform.gameObject.activeInHierarchy;

		spriteRenderer.color = RANGE_COLOR;
		arrowTransform.gameObject.SetActive(false);
	}

	public void UnsetRangeColor()
	{
		if (spriteRenderer.color != RANGE_COLOR) {return;}

		spriteRenderer.color = beforeColor;
		arrowTransform.gameObject.SetActive(wasArrowShown);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		onPointerEnter(this);
	}
}
