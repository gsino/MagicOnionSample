using MagicOnion;
using System;

namespace Sample.MagicOnion.Definitions
{
    public interface ICalculator : IService<ICalculator>
    {
        UnaryResult<int> Sum(int x, int y);
    }
}
