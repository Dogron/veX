namespace ImportantScripts.Interactables
{
    public class ExpendableResourceProvider : ResourceProvider
    {
        public ResourceType Type = ResourceType.Health;
        public int Amount;

        public override Resource Consume()
        {
            Destroy(gameObject);
            return new Resource(Amount, Type);
        }
    }
}