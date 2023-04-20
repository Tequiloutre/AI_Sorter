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
		Texture2D _texture = AssetPreview.GetAssetPreview(_slot.Item.GetItem.gameObject);
		imageItem.sprite = Sprite.Create(_texture, new Rect(0, 0, _texture.width, _texture.height), new Vector2(0.5f, 0.5f));
		if (_slot.Count <= 1) return;
		imageCount.SetActive(true);
		textCount.text = _slot.Count.ToString();
	}
}
