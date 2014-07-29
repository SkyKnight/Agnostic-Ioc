namespace Cardinal.IoC.Registration
{
    public interface IRegistrationDefinition<TRegisteredAs, TResolvedTo>  where TRegisteredAs : class where TResolvedTo : TRegisteredAs
    {
        LifetimeScope Scope { get; set; }
    }
}
