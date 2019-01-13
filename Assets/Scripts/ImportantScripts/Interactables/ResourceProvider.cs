using UnityEngine;

namespace ImportantScripts.Interactables
{
    public abstract class ResourceProvider : MonoBehaviour
    {
        public abstract Resource Consume();
    }
}