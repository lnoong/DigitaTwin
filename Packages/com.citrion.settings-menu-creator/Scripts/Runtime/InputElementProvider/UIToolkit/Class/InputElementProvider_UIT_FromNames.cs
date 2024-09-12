using CitrioN.Common;
using System.ComponentModel;
using UnityEngine;

namespace CitrioN.SettingsMenuCreator
{
  [MenuOrder(1)]
  [MenuPath("From Name")]
  [DisplayName("Custom (From Name)")]
  public class InputElementProvider_UIT_FromName_Custom : InputElementProvider_UIT_FromName
  {
    [SerializeField]
    protected string providerName = string.Empty;

    protected override string ProviderName => providerName;

    public override string Name => "From Name";
  }

  [MenuOrder(11)]
  [MenuPath("From Name")]
  [DisplayName("Slider Float (From Name)")]
  public class InputElementProvider_UIT_FromName_SliderFloat : InputElementProvider_UIT_FromName
  {
    protected override string ProviderName => "Slider Float";
  }

  [MenuOrder(10)]
  [MenuPath("From Name")]
  [DisplayName("Slider Integer (From Name)")]
  public class InputElementProvider_UIT_FromName_SliderInteger : InputElementProvider_UIT_FromName
  {
    protected override string ProviderName => "Slider Integer";
  }

  [MenuOrder(99)]
  [MenuPath("From Name")]
  [DisplayName("Dropdown (From Name)")]
  public class InputElementProvider_UIT_FromName_Dropdown : InputElementProvider_UIT_FromName
  {
    protected override string ProviderName => "Dropdown";
  }

  [MenuOrder(90)]
  [MenuPath("From Name")]
  [DisplayName("Previous/Next Selector (From Name)")]
  public class InputElementProvider_UIT_FromName_PreviousNextSelector : InputElementProvider_UIT_FromName
  {
    protected override string ProviderName => "Previous Next Selector";
  }

  [MenuOrder(89)]
  [MenuPath("From Name")]
  [DisplayName("Previous/Next Selector No Cycle (From Name)")]
  public class InputElementProvider_UIT_FromName_PreviousNextSelector_NoCycle : InputElementProvider_UIT_FromName
  {
    protected override string ProviderName => "Previous Next Selector No Cycle";
  }

  [MenuOrder(9)]
  [MenuPath("From Name")]
  [DisplayName("Toggle (From Name)")]
  public class InputElementProvider_UIT_FromName_Toggle : InputElementProvider_UIT_FromName
  {
    protected override string ProviderName => "Toggle";
  }

  [MenuOrder(8)]
  [MenuPath("From Name")]
  [DisplayName("Button (From Name)")]
  public class InputElementProvider_UIT_FromName_Button : InputElementProvider_UIT_FromName
  {
    protected override string ProviderName => "Button";
  }

  [MenuOrder(7)]
  [MenuPath("From Name")]
  [DisplayName("Integer Field (From Name)")]
  public class InputElementProvider_UIT_FromName_IntegerField : InputElementProvider_UIT_FromName
  {
    protected override string ProviderName => "Integer Field";
  }

  [MenuOrder(6)]
  [MenuPath("From Name")]
  [DisplayName("Float Field (From Name)")]
  public class InputElementProvider_UIT_FromName_FloatField : InputElementProvider_UIT_FromName
  {
    protected override string ProviderName => "Float Field";
  }

  [MenuOrder(2)]
  [MenuPath("From Name")]
  [DisplayName("Headline (From Name)")]
  public class InputElementProvider_UIT_FromName_Headline : InputElementProvider_UIT_FromName
  {
    protected override string ProviderName => "Headline";
  }

  [MenuOrder(1)]
  [MenuPath("From Name")]
  [DisplayName("Title (From Name)")]
  public class InputElementProvider_UIT_FromName_Title : InputElementProvider_UIT_FromName
  {
    protected override string ProviderName => "Title";
  }
}
