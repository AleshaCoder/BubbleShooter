using System;
using Services;

namespace Shooter
{
    public interface IAiming : IService
    {
        event Action<AttackData> OnAimingEnd;
    }
}
