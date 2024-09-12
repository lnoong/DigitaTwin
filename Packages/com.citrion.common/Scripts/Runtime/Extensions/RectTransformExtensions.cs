using UnityEngine;

namespace CitrioN.Common
{
  public enum RectMatchOperation { Width, Height, WidthAndHeight }

  public static class RectTransformExtensions
  {
    /// <summary>
    /// Matches certain RectTransform values of RectTransform this method is used on
    /// to another one that is provided in the parameters.
    /// The match operation defines what values should be matched.
    /// </summary>
    /// <param name="other">The other RectTransform the values should be taken from</param>
    /// <param name="matchOperation">The operation that defines what values should be matched</param>
    public static void MatchValuesToRect(this RectTransform rect, RectTransform other, RectMatchOperation matchOperation)
    {
      Vector2 newSizeDelta = new Vector2(rect.rect.width, rect.rect.height);

      switch (matchOperation)
      {
        case RectMatchOperation.Width:
          newSizeDelta.x = other.rect.x;
          break;
        case RectMatchOperation.Height:
          newSizeDelta.y = other.rect.y;
          break;
        case RectMatchOperation.WidthAndHeight:
          newSizeDelta.x = other.rect.x;
          newSizeDelta.y = other.rect.y;
          break;
        default:
          Debug.LogWarning("Invalid match operation!");
          break;
      }

      rect.sizeDelta = newSizeDelta;
    }

    public static Rect ToWorldSpaceRect(this RectTransform rectTransform)
    {
      Rect rect = rectTransform.rect;
      rect.center = rectTransform.TransformPoint(rect.center);
      rect.size = rectTransform.TransformVector(rect.size);
      return rect;
    }

    /// <summary>
    /// Checks if the <see cref="RectTransform"/> is fully visible on the screen
    /// aka if all 4 corners are on the screen.
    /// </summary>
    /// <returns>True if the <see cref="RectTransform"/> is fully visible on the screen</returns>
    public static bool IsVisibleOnScreen(this RectTransform rectTransform)
    {
      // Get the screen rect
      var screenRect = new Rect(0f, 0f, Screen.width, Screen.height);
      // Get the world space rect from the RectTransform
      var rect = rectTransform.ToWorldSpaceRect();
      // Check if both the top left and the bottom right corners of the 
      // rect are inside the screenRect
      return screenRect.Contains(new Vector2(rect.xMin, rect.yMax)) &&
             screenRect.Contains(new Vector2(rect.xMax, rect.yMin));
    }

    /// <summary>
    /// Checks if the specified corner is inside the screen <see cref="Rect"/>.
    /// </summary>
    /// <param name="leftCorner">Is the corner on the left edge of the rect?</param>
    /// <param name="topCorner">Is the corner on the top edge of the rect?</param>
    /// <returns>True if the corner is inside the screen rect</returns>
    public static bool IsCornerOnScreen(this RectTransform rectTransform, bool leftCorner, bool topCorner)
    {
      // Get the screen rect
      var screenRect = new Rect(0f, 0f, Screen.width, Screen.height);
      // Get the world space rect from the RectTransform
      var rect = rectTransform.ToWorldSpaceRect();
      // Get the corner position based on the provided parameters
      Vector2 corner = new Vector2(leftCorner ? rect.xMin : rect.xMax,
                                         topCorner ? rect.yMax : rect.yMin);
      // Check if the screen rect contains the corner position
      return screenRect.Contains(corner);
    }
  }
}