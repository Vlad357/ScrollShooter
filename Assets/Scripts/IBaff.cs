using ScrollShooter.Entity;

namespace ScrollShooter 
{
    public interface IBaff
    {
        EntityStats ApplyBuff(EntityStats baseStats);
    }
}