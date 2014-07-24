namespace Cardinal.IoC.UnitTests.Helpers
{
    public class LateDependantClass : ILateDependantClass
    {
        public string Name { get; set; }
    }

    public interface ILateDependantClass
    {
        string Name { get; set; }
    }
}
