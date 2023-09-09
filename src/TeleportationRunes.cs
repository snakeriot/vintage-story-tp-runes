using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

[assembly: ModInfo("TeleportationRunes",
    Description = "Mod that simplifies traveling in the world using gothic-like teleportation stones.",
    Website = "https://github.com/snakeriot/vintage-story-tp-runes",
    Version = "1.0.9",
    Authors = new[] { "dKosher" })]
namespace TeleporatationRunes
{
    public class TeleporatationRunesMod : ModSystem
    {
        public override void Start(ICoreAPI api)
        {
            api.RegisterBlockEntityClass("BEBeacon", typeof(BEBeacon));
            api.RegisterItemClass("ItemRune", typeof(ItemRune));
        }

        public override void StartClientSide(ICoreClientAPI api)
        {

        }

        public override void StartServerSide(ICoreServerAPI api)
        {

        }
    }
}