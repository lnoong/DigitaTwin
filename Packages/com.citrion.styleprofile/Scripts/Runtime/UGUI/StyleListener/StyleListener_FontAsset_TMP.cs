using TMPro;
using UnityEngine;

namespace CitrioN.StyleProfileSystem
{
  [AddComponentMenu("CitrioN/Style Profile/Style Listener/Style Listener (TMP Font Asset)")]
  public class StyleListener_FontAsset_TMP : GenericStyleListener<TMP_FontAsset>
  {
    [Header("Font")]

    [SerializeField]
    [Tooltip("The text component to manage the font for.")]
    protected TextMeshProUGUI textElement;

    private void Reset()
    {
      CacheTextElement();
    }

    protected override void Awake()
    {
      base.Awake();
      CacheTextElement();
    }

    private void CacheTextElement()
    {
      if (textElement == null)
      {
        textElement = GetComponent<TextMeshProUGUI>();
      }
    }

    protected override void ApplyChange(TMP_FontAsset fontAsset)
    {
      base.ApplyChange(fontAsset);
      if (fontAsset != null && textElement != null)
      {
        textElement.font = fontAsset;
      }
    }
  }
}