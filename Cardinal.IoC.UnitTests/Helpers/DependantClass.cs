namespace Cardinal.IoC.UnitTests.Helpers
{
    public class DependantClass2 : IDependantClass
    {
        public DependantClass2()
        {
            Name = TestConstants.DependantClass2Name;
        }

        public string Name { get; set; }
    }

    public class DependantClass : IDependantClass
    {
        public DependantClass()
        {
            Name = TestConstants.DependantClassName;
        }

        public string Name { get; set; }
    }

    public interface IDependantClass
    {
        string Name { get; set; }
    }
}
