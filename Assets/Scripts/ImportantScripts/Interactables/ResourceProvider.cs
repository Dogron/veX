using UnityEngine;

namespace ImportantScripts.Interactables
{
    public abstract class ResourceProvider : MonoBehaviour
    {
        public int Amount;
        public abstract Resource Consume();
    }
}