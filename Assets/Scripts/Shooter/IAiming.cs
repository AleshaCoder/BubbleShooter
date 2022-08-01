using System;
using Services;

public interface IAiming : IService
{
    event Action<AttackData> OnAimingEnd;
}
