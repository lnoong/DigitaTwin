using CitrioN.Common;
using CitrioN.Common.Editor;
using UnityEngine;
using UnityEngine.UIElements;

namespace CitrioN.SettingsMenuCreator.Editor
{
  [CreateAssetMenu(fileName = "WelcomeController_",
                   menuName = "CitrioN/Common/ScriptableObjects/VisualTreeAsset/Controller/Welcome")]
  public class WelcomeController : ScriptableVisualTreeAssetController
  {
    public StringToStringRelationProfile texts;

    private const string OVERVIEW_LABEL_CLASS = "label__overview";
    private const string GENERAL_OVERVIEW_LABEL_CLASS = "label__overview-general";

    public override void Setup(VisualElement root)
    {
      var overviewLabel = root.Q<Label>(className: OVERVIEW_LABEL_CLASS);

      string packageVersion;
      bool hasProVersion;
      bool hasInputRebind;
      bool hasPostProcess;
      bool hasSrp;

      if (overviewLabel != null)
      {
        hasProVersion = PackageUtilities.IsPackageInstalled("com.citrion.settings-menu-creator.pro", out packageVersion);
        hasInputRebind = PackageUtilities.IsPackageInstalled("com.citrion.settings-menu-creator.input", out packageVersion);
        hasPostProcess = PackageUtilities.IsPackageInstalled("com.citrion.settings-menu-creator.post-processing", out packageVersion);
        hasSrp = PackageUtilities.IsPackageInstalled("com.citrion.settings-menu-creator.srp", out packageVersion);

        bool hasPaidVersion = hasProVersion || hasInputRebind || hasPostProcess || hasSrp;

        string overviewText = texts != null ? texts.GetValue(hasPaidVersion ? "overview-paid" : "overview-free") : string.Empty;

        overviewLabel.SetText(overviewText);
      }

      var generalOverviewLabel = root.Q<Label>(className: GENERAL_OVERVIEW_LABEL_CLASS);

      if (generalOverviewLabel != null)
      {
        string generalOverviewText = texts != null ? texts.GetValue("overview-general") : string.Empty;

        generalOverviewLabel.SetText(generalOverviewText);
      }
    }
  }
}