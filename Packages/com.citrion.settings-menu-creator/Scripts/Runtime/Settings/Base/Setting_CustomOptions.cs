using CitrioN.Common;
using System.Collections.Generic;
using UnityEngine;

namespace CitrioN.SettingsMenuCreator
{
  [ExcludeFromMenuSelection]
  public abstract class Setting_CustomOptions : Setting
  {
    [SerializeField]
    [Tooltip("Click to show/hide available input element options")]
    protected List<StringToStringRelation> options = new List<StringToStringRelation>();

    public Setting_CustomOptions() { }

    public override List<StringToStringRelation> Options
    {
      get { return options; }
    }
  }
}
