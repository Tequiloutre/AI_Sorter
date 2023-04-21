using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour
{
	[SerializeField] private Image imageItem = null;
	[SerializeField] private GameObject imageCount = null;
	[SerializeField] private TMP_Text textCount = null;

	public void ResetContent()
	{
		imageItem.gameObject.SetActive(false);
		imageCount.SetActive(false);
	}

	public void SetContent(ItemSlot _slot)
	{
		imageItem.gameObject.SetActive(true);
		imageItem.sprite = _slot.Item.GetSprite;
		if (_slot.Count <= 1) return;
		imageCount.SetActive(true);
		textCount.text = _slot.Count.ToString();
	}
}
