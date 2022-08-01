using Services;

public interface IMapLoader : IService
{
    Map Load(string path);
}
