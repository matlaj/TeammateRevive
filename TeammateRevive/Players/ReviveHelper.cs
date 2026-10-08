using RoR2;

namespace TeammateRevive.Players;

public static class ReviveHelper
{
    /// <summary>
    /// Respawns the player at their death position the same way Dio's Best Friend does.
    /// Since game version 1.5.0 CharacterMaster.RespawnExtraLife no longer consumes Dio's Best Friend
    /// (that moved to CharacterMaster.TryReviveOnBodyDeath), so it can be called directly
    /// without touching the player's inventory.
    /// </summary>
    public static void RespawnExtraLife(CharacterMaster master)
    {
        master.RespawnExtraLife();
    }
}
