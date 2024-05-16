using ScrollShooter.Entity;

namespace ScrollShooter 
{
    public interface IBuff
    {
        EntityStats ApplyStats(EntityStats baseStats);
    }
}