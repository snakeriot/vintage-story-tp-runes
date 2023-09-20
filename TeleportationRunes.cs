using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

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