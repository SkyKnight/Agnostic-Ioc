namespace Cardinal.IoC.Registration
{
    public interface IInstanceRegistrationDefinition<out TResolvedTo>
    {
        TResolvedTo Instance { get; }
    }
}
