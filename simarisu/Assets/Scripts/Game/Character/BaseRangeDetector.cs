using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseRangeDetector : GameMonoBehaviour
{
	private System.Action<BaseCharacter> onTriggerEnter;

	public void Show(float rangeSize, System.Action<BaseCharacter> onTriggerEnter)
	{
		this.onTriggerEnter = onTriggerEnter;
		transform.ScaleTo(rangeSize);

		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}

	protected void CharacterHit(BaseCharacter character)
	{
		if (onTriggerEnter == null) {return;}
		onTriggerEnter(character);
	}

	protected virtual void OnTriggerEnter2D(Collider2D collider)
	{
		BaseCharacter character = collider.gameObject.GetComponent<BaseCharacter>();
		if (character != null)
		{
			CharacterHit(character);
		}
	}
}
