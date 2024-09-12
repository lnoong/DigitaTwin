using TMPro;
using UnityEngine;

namespace CitrioN.StyleProfileSystem
{
  [AddComponentMenu("CitrioN/Style Profile/Style Listener/Style Listener (TMP Text - Color)")]
  public class StyleListener_Color_Text : StyleListener_Color
  {
    [Header("Text Color")]

    [SerializeField]
    [Tooltip("Should the alpha value be kept?")]
    protected bool keepAlpha = true;

    [SerializeField]
    [Tooltip("Reference to the text component to manage.")]
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

    protected override void ApplyChange(Color color)
    {
      base.ApplyChange(color);
      if (textElement != null)
      {
        if (keepAlpha)
        {
          color.a = textElement.color.a;
        }
        textElement.color = color;
      }
    }
  }
}