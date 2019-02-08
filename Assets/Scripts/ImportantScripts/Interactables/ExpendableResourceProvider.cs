namespace ImportantScripts.Interactables
{
    public class ExpendableResourceProvider : ResourceProvider
    {
        public ResourceType Type = ResourceType.Health;
        
        public override Resource Consume()
        {
            gameObject.SetActive(false);
            return new Resource(Amount, Type);
        }
    }
}