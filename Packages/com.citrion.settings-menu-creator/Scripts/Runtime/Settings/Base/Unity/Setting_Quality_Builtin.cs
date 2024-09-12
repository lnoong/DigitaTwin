using CitrioN.Common;

namespace CitrioN.SettingsMenuCreator
{
  [ExcludeFromMenuSelection]
  public abstract class Setting_Quality_Builtin<T> : Setting_Quality<T>
  {
    public override string EditorName => base.EditorName + " (Builtin)";
  }
}