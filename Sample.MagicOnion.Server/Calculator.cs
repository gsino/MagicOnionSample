using MagicOnion;
using MagicOnion.Server;
using Sample.MagicOnion.Definitions;
using System;

namespace Sample.MagicOnion.Server
{
    public class Calculator : ServiceBase<Definitions.ICalculator>, Definitions.ICalculator
    {
        public async UnaryResult<int> Sum(int x, int y)
        {
            return x + y;
        }
    }
}
