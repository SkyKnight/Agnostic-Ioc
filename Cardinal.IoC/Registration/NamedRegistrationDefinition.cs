namespace Cardinal.IoC.Registration
{
    public class NamedRegistrationDefinition<TRegisteredAs, TResolvedTo> : RegistrationDefinition<TRegisteredAs, TResolvedTo>, INamedRegistrationDefinition
        where TRegisteredAs : class
        where TResolvedTo : TRegisteredAs
    {
        public NamedRegistrationDefinition(string name)
        {
            Name = name;
        }

        public string Name { get; protected set; }
    }
}
