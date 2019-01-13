

namespace ImportantScripts.Interactables
{
    public class Resource
    {
        private readonly ResourceType _type;
        private readonly int _amount;

        public ResourceType Type
        {
            get { return _type; }
        }

        public int Amount
        {
            get { return _amount; }
        }

        public Resource(int amount, ResourceType type)
        {
            _type = type;
            _amount = amount;
        }
    }
}