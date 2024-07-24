using System.Collections.Generic;

namespace Character.Buff
{
    public interface IBuffable
    {
        void AddBuff(IBuff buff);
        void RemoveBuff(IBuff buff);
    }
}