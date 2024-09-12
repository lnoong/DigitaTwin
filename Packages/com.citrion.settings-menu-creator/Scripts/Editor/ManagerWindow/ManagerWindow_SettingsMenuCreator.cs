using CitrioN.Common.Editor;
using UnityEditor;

namespace CitrioN.SettingsMenuCreator.Editor
{
  public class ManagerWindow_SettingsMenuCreator : ManagerWindow
  {
    protected const string TITLE = "Settings Menu Creator";

    [MenuItem("Tools/CitrioN/Settings Menu Creator/Manager", priority = -10)]
    public static ManagerWindow ShowWindow_SettingsMenuCreator()
    {
      return ShowWindow<ManagerWindow_SettingsMenuCreator>(TITLE);
    }

    //[MenuItem("Tools/CitrioN/Settings Menu Creator/Welcome")]
    //public static ManagerWindow ShowManagerTab_Welcome()
    //{
    //  return ShowWindow<ManagerWindow_SettingsMenuCreator>(TITLE, "Welcome");
    //}

    [MenuItem("Tools/CitrioN/Settings Menu Creator/Resources Generator", priority = -8)]
    public static ManagerWindow ShowManagerTab_ResourcesGenerator()
    {
      return ShowWindow<ManagerWindow_SettingsMenuCreator>(TITLE, "Resources Generator");
    }

    [MenuItem("Tools/CitrioN/Settings Menu Creator/Samples", priority = -9)]
    public static ManagerWindow ShowManagerTab_Samples()
    {
      return ShowWindow<ManagerWindow_SettingsMenuCreator>(TITLE, "Samples");
    }

    [MenuItem("Tools/CitrioN/Settings Menu Creator/Documentation", priority = -7)]
    public static ManagerWindow ShowManagerTab_Documentation()
    {
      return ShowWindow<ManagerWindow_SettingsMenuCreator>(TITLE, "Documentation");
    }

    [MenuItem("Tools/CitrioN/Settings Menu Creator/Support", priority = -6)]
    public static ManagerWindow ShowManagerTab_Support()
    {
      return ShowWindow<ManagerWindow_SettingsMenuCreator>(TITLE, "Support");
    }
  }
}