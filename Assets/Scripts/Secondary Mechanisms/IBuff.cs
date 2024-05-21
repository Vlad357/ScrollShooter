using ScrollShooter.EntityScripts;

namespace ScrollShooter.SecondaryMechanisms 
{
    public interface IBuff
    {
        EntityStats ApplyStats(EntityStats baseStats);
    }
}