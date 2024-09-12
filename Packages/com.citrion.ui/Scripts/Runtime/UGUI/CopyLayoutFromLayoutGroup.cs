using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CitrioN.UI
{
  // TODO Check for remaining overrides
  // TODO Test overrides for them being required?
  [RequireComponent(typeof(RectTransform))]
  public class CopyLayoutFromLayoutGroup : UIBehaviour, ILayoutElement
  {
    [SerializeField]
    protected RectTransform rectTransform;
    [SerializeField]
    protected LayoutGroup layoutGroup;

    public float minWidth
      => layoutGroup != null ? ((ILayoutElement)layoutGroup).minWidth : 0;

    public float preferredWidth
      => layoutGroup != null ? ((ILayoutElement)layoutGroup).preferredWidth : 0;

    public float flexibleWidth
      => layoutGroup != null ? ((ILayoutElement)layoutGroup).flexibleWidth : -1;

    public float minHeight
      => layoutGroup != null ? ((ILayoutElement)layoutGroup).minHeight : 0;

    public float preferredHeight
      => layoutGroup != null ? ((ILayoutElement)layoutGroup).preferredHeight : 0;

    public float flexibleHeight
      => layoutGroup != null ? ((ILayoutElement)layoutGroup).flexibleHeight : -1;

    public int layoutPriority
      => layoutGroup != null ? ((ILayoutElement)layoutGroup).layoutPriority : 1;

#if UNITY_EDITOR
    protected override void Reset()
    {
      base.Reset();
      CacheComponents();
    }
#else
    protected void Reset()
    {
      CacheComponents();
    }
#endif

    protected override void Awake()
    {
      base.Awake();
      CacheComponents();
    }

    protected override void OnEnable()
    {
      base.OnEnable();
      RebuildLayout();
    }

    protected override void OnDisable()
    {
      base.OnDisable();
      RebuildLayout();
    }

    private void CacheComponents()
    {
      if (rectTransform == null)
      {
        rectTransform = GetComponent<RectTransform>();
      }
      if (layoutGroup == null)
      {
        layoutGroup = GetComponentInChildren<LayoutGroup>();
      }
    }

    protected void RebuildLayout()
    {
      if (!IsActive()) { return; }
      LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
    }

    protected override void OnTransformParentChanged()
    {
      base.OnTransformParentChanged();
      RebuildLayout();
    }

    protected override void OnDidApplyAnimationProperties()
    {
      base.OnDidApplyAnimationProperties();
      RebuildLayout();
    }

    protected override void OnBeforeTransformParentChanged()
    {
      base.OnBeforeTransformParentChanged();
      RebuildLayout();
    }

    public void CalculateLayoutInputHorizontal()
    {
      if (layoutGroup == null) { return; }
      ((ILayoutElement)layoutGroup).CalculateLayoutInputHorizontal();
    }

    public void CalculateLayoutInputVertical()
    {
      if (layoutGroup == null) { return; }
      ((ILayoutElement)layoutGroup).CalculateLayoutInputVertical();
    }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
      base.OnValidate();
      if (!Application.isPlaying)
      {
        RebuildLayout();
      }
    }
#endif
  }
}