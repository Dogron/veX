namespace ImportantScripts.Interactables
{
    public class NotExpendableResourceProvider : ResourceProvider
    {
        public ResourceType Type = ResourceType.Health;
        
        public override Resource Consume()
        {
           return new Resource(Amount, Type);
        }
    }
}