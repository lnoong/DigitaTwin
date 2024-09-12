using CitrioN.Common;
using UnityEngine;

namespace CitrioN.SettingsMenuCreator
{
  [CreateAssetMenu(fileName = "InputElementProviderCollection_UGUI_",
                   menuName = "CitrioN/Settings Menu Creator/Provider Collection/UGUI/Default",
                   order = 5)]
  public class InputElementProviderCollection_UGUI : StringToGenericDataRelationProfile<ScriptableInputElementProvider_UGUI>
  {

  }
}