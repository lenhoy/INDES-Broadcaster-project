namespace OBSUWP.Inferfaces
{
    public interface ISource
    {
        string GetOutput();
        object Output { get; }
        string Type { get; }
    }
}
