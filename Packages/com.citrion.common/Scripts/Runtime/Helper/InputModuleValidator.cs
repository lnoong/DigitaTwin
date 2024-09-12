using UnityEngine;
using UnityEngine.EventSystems;

namespace CitrioN.Common
{
  public class InputModuleValidator : MonoBehaviour
  {
    void Start()
    {
      Validate();
    }

    private void Validate()
    {
      var eventSystem = EventSystem.current;
#if ENABLE_LEGACY_INPUT_MANAGER
      eventSystem.gameObject.AddOrGetComponent<StandaloneInputModule>();
#elif ENABLE_INPUT_SYSTEM
      var legacyModule = eventSystem.GetComponent<StandaloneInputModule>();
      if (legacyModule != null)
      {
        Destroy(legacyModule);
      }
      eventSystem.gameObject.AddOrGetComponent<UnityEngine.InputSystem.UI.InputSystemUIInputModule>();
#endif
    }
  }
}