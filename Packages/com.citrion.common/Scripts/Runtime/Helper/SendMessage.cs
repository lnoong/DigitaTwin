using UnityEngine;

namespace CitrioN.Common
{
  [SkipObfuscationRename]
  public class SendMessage : MonoBehaviour
  {
    [SerializeField]
    [Tooltip("The method name to send/broadcast")]
    protected string methodName = string.Empty;

    [SerializeField]
    [Tooltip("Should the message be broadcasted?")]
    protected bool broadcast = false;

    [Button]
    [ContextMenu("Send Message")]
    public void Send()
    {
      Send(methodName, broadcast);
    }

    [Button]
    public void Send(string methodName)
    {
      Send(methodName, broadcast);
    }

    [Button]
    [ContextMenu("Broadcast Message")]
    public void Broadcast()
    {
      Send(methodName, true);
    }

    [Button]
    public void Broadcast(string methodName)
    {
      Send(methodName, true);
    }

    [Button]
    public void Send(string methodName, bool broadcast)
    {
      if (string.IsNullOrEmpty(methodName)) { return; }
      if (broadcast)
      {
        BroadcastMessage(methodName, SendMessageOptions.DontRequireReceiver);
      }
      else
      {
        SendMessage(methodName, SendMessageOptions.DontRequireReceiver);
      }
    }
  }
}