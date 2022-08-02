namespace Services.MapGenerator
{
    public interface IMapLoader : IService
    {
        Map Load(string path);
    }
}
