using SpaceShootuh.Configurations;
using System.Collections.Generic;

namespace SpaceShootuh.Battle.Environment
{
    public interface ILevel
    {
        List<WaveConfig> WaveConfigs { get; }
    }
}