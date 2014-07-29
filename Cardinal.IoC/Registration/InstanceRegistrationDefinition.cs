namespace Cardinal.IoC.Registration
{
    public class InstanceRegistrationDefinition<TRegisteredAs, TResolvedTo> : 
        RegistrationDefinition<TRegisteredAs, TResolvedTo>, 
        IInstanceRegistrationDefinition<TResolvedTo> where TResolvedTo : TRegisteredAs where TRegisteredAs : class
    {
        public InstanceRegistrationDefinition(TResolvedTo instance)
        {
            Instance = instance;
        }

        public TResolvedTo Instance { get; private set; }
    }
}
