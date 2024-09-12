using CitrioN.Common;

namespace CitrioN.SettingsMenuCreator
{
  [MenuPath("Types")]
  [ExcludeFromMenuSelection]
  public abstract class Setting_Generic_Type<T> : Setting_Generic<T>
  {
    public override string EditorNamePrefix => "[Type]";
  }
}