namespace TeleportationRunes.src.Dkosher.Beacon
{
    public class BeaconState
    {
        public static BeaconState BIND_RUNE = new BeaconState("BindRune", "bindrune");
        public static BeaconState TELEPORT = new BeaconState("Teleport", "teleport");

        private BeaconState(string name, string code)
        {
            _name = name;
            _code = code;
        }

        private readonly string _name;
        private readonly string _code;

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string Code
        {
            get
            {
                return _code;
            }
        }
    }
}
