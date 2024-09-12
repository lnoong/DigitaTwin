using CitrioN.Common;
using System.ComponentModel;
using UnityEngine;

namespace CitrioN.SettingsMenuCreator
{
  [MenuOrder(1)]
  [MenuPath("From Name")]
  [DisplayName("Custom (From Name)")]
  public class InputElementProvider_UGUI_FromName_Custom : InputElementProvider_UGUI_FromName
  {
    [SerializeField]
    protected string providerName = string.Empty;

    protected override string ProviderName => providerName;

    public override string Name => "From Name";
  }

  [MenuOrder(11)]
  [MenuPath("From Name")]
  [DisplayName("Slider Float (From Name)")]
  public class InputElementProvider_UGUI_FromName_SliderFloat : InputElementProvider_UGUI_FromName
  {
    protected override string ProviderName => "Slider Float";

    public override string Name => "Slider Float (From Name)";
  }

  [MenuOrder(10)]
  [MenuPath("From Name")]
  [DisplayName("Slider Integer (From Name)")]
  public class InputElementProvider_UGUI_FromName_SliderInteger : InputElementProvider_UGUI_FromName
  {
    protected override string ProviderName => "Slider Integer";

    public override string Name => "Slider Integer (From Name)";
  }

  [MenuOrder(99)]
  [MenuPath("From Name")]
  [DisplayName("Dropdown (From Name)")]
  public class InputElementProvider_UGUI_FromName_Dropdown : InputElementProvider_UGUI_FromName
  {
    protected override string ProviderName => "Dropdown";

    public override string Name => "Dropdown (From Name)";
  }

  [MenuOrder(90)]
  [MenuPath("From Name")]
  [DisplayName("Previous Next Selector (From Name)")]
  public class InputElementProvider_UGUI_FromName_PreviousNextSelector : InputElementProvider_UGUI_FromName
  {
    protected override string ProviderName => "Previous Next Selector";

    public override string Name => "Previous Next Selector (From Name)";
  }

  [MenuOrder(9)]
  [MenuPath("From Name")]
  [DisplayName("Toggle (From Name)")]
  public class InputElementProvider_UGUI_FromName_Toggle : InputElementProvider_UGUI_FromName
  {
    protected override string ProviderName => "Toggle";

    public override string Name => "Toggle (From Name)";
  }

  [MenuOrder(8)]
  [MenuPath("From Name")]
  [DisplayName("Button (From Name)")]
  public class InputElementProvider_UGUI_FromName_Button : InputElementProvider_UGUI_FromName
  {
    protected override string ProviderName => "Button";

    public override string Name => "Button (From Name)";
  }

  [MenuOrder(7)]
  [MenuPath("From Name")]
  [DisplayName("Integer Field (From Name)")]
  public class InputElementProvider_UGUI_FromName_IntegerField : InputElementProvider_UGUI_FromName
  {
    protected override string ProviderName => "Integer Field";

    public override string Name => "Field Integer (From Name)";
  }

  [MenuOrder(6)]
  [MenuPath("From Name")]
  [DisplayName("Float Field (From Name)")]
  public class InputElementProvider_UGUI_FromName_FloatField : InputElementProvider_UGUI_FromName
  {
    protected override string ProviderName => "Float Field";

    public override string Name => "Field Float (From Name)";
  }

  [MenuOrder(2)]
  [MenuPath("From Name")]
  [DisplayName("Headline (From Name)")]
  public class InputElementProvider_UGUI_FromName_Headline : InputElementProvider_UGUI_FromName
  {
    protected override string ProviderName => "Headline";

    public override string Name => "Headline (From Name)";
  }

  [MenuOrder(1)]
  [MenuPath("From Name")]
  [DisplayName("Title (From Name)")]
  public class InputElementProvider_UGUI_FromName_Title : InputElementProvider_UGUI_FromName
  {
    protected override string ProviderName => "Title";

    public override string Name => "Title (From Name)";
  }

  [MenuOrder(1)]
  [MenuPath("From Name")]
  [DisplayName("Container (From Name)")]
  public class InputElementProvider_UGUI_FromName_Container: InputElementProvider_UGUI_FromName
  {
    protected override string ProviderName => "Container";

    public override string Name => "Container (From Name)";
  }
}
