using Cysharp.Threading.Tasks;

namespace Character.Buff
{
    public interface IBuff
    {
        CharacterStats AddBuff(CharacterStats baseBuff);
    }
}