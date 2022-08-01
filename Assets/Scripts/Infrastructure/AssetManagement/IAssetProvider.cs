using Services;
using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path, Vector3 at);
        T Instantiate<T>(T obj, Vector3 at) where T : Object;
        GameObject Instantiate(string path);
    }
}
