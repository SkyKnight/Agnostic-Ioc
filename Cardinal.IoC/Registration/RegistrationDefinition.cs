namespace Cardinal.IoC.Registration
{
    public class RegistrationDefinition<TRegisteredAs, TResolvedTo> : IRegistrationDefinition<TRegisteredAs, TResolvedTo>
        where TRegisteredAs : class
        where TResolvedTo : TRegisteredAs
    {
        public LifetimeScope Scope { get; set; }
    }
}
