using TMPro;
using UnityEngine;

public class MatchSetup : GameState
{
    [SerializeField] private Animator _animator;
    [SerializeField] private TMP_InputField _tournamentField;
    [SerializeField] private TMP_InputField _playerField;
    [SerializeField] private TMP_InputField[] _rivalsFields;

    private int _animatorHash = Animator.StringToHash("Visible");

    string[] _firstNames = new string[] {
    "ELDOR", "MIST", "FROST", "SHADOW", "IRON", "SILVER", "GREEN", "DARK", "SUN", "STORM",
    "EMBER", "GOLDEN", "DRAGON", "NIGHT", "CRYSTAL", "THORN", "REGAL", "WILD", "HOLLOW", "BRIGHT",
    "CINDER", "STAR", "MOON", "RAVEN", "WHISPER", "FLINT", "ASHEN", "GLIMMER", "VERDANT", "STONE",
    "VALE", "SKY", "DUSK", "GALE", "DREAM", "WRAITH", "SEA", "VENGEFUL", "TEMPEST", "SHADOWED",
    "ARCANE", "ANCIENT", "MAPLE", "BLOOD", "THUNDERS", "PHOENIX", "GLOOM", "CELESTIAL", "OBSIDIAN",
    "FERAL", "SABLE", "MEADOW", "ETERNAL", "BROKEN", "FROSTED", "FEY", "TIDAL", "LUMINOUS", "VORTEX",
    "SPIRITED", "LOST", "HOWLING", "MIRTHFUL", "COBALT", "SAPPHIRE", "DRIFTING", "MOONSTONE", "RAVENOUS",
    "DREAD", "WONDROUS", "SHIMMERING", "ELEMENTAL", "FROSTBITE", "HOLLOWED", "WANDERING", "SPIRE", "FORGOTTEN",
    "TWILIGHT", "WHISPERING", "CRIMSON", "SEARING", "AGILE", "THUNDEROUS", "GLACIAL", "CHAOS", "GRASSY",
    "TAINTED", "PATCHWORK", "STARLIT", "TWISTED", "CURSIVE", "PERILOUS", "TIMELESS", "BITTER"
    };

    string[] _secondNames = new string[] {
    "VALE", "KINGDOM", "KEEP", "REALM", "FORTRESS", "CITADEL", "HAVEN", "NEST", "LAND", "DOMINION",
    "EMPIRE", "SPIRE", "GROVE", "SHORE", "GLADE", "HARBOR", "CONCLAVE", "COAST", "SANCTUARY", "TIDES",
    "HOLLOW", "FALLS", "PEAKS", "WILDS", "DUNES", "EXPANSE", "PLAINS", "FIELDS", "HOLD", "BASTION",
    "RUINS", "WASTES", "DEPTHS", "OUTPOST", "MEADOWS", "HEART", "CRADLE", "TRENCH", "HEARTH", "SHIRE",
    "ARCHIPELAGO", "SWAMPS", "BASIN", "THOROUGHFARE", "ABYSS", "WAY", "BLIGHT", "SPIRES", "SUNDOWN", "HEIGHTS",
    "MIRAGE", "ROYCE", "BOG", "SHRINE", "ROUTE", "CHASM", "ISLES", "STRETCH", "END", "PATH",
    "LEGACY", "FRACTURE", "RETREAT", "WATCH", "OASIS", "FIELD", "TAR", "REMNANT", "SUMMITS",
    "SUMMIT", "CATHEDRAL", "ISLE", "SOURCE", "HILL", "SKIES", "PASSAGE", "CROFT", "CROSSROAD", "OUTCROP",
    "REACH"
    };

    private void Awake()
    {
        _tournamentField.onValueChanged.AddListener((t) => _tournamentField.text = t.ToUpper());
        _playerField.onValueChanged.AddListener((t) => _playerField.text = t.ToUpper());
        foreach (var field in _rivalsFields)
            field.onValueChanged.AddListener((t) => field.text = t.ToUpper());
    }

    public void GoToCardsDraft()
    {
        _animator.SetBool(_animatorHash, false);
    }

    public override void Activate()
    {
        base.Activate();
        _animator.SetBool(_animatorHash, true);
        _tournamentField.text = $"{_firstNames[Random.Range(0, _firstNames.Length)]} {_secondNames[Random.Range(0, _secondNames.Length)]}";
    }

    private void DraftACard()
    {

    }
}
