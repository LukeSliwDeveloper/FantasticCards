using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MatchSetup : GameState
{
    [SerializeField] private Animator _animator;
    [SerializeField] private TMP_InputField _tournamentField;
    [SerializeField] private TMP_InputField _playerField;
    [SerializeField] private TMP_InputField[] _rivalsFields;
    [SerializeField] private Card _testCardPrefab;

    private Vector2 _cardSpawnPosition = Vector2.left * 4.2f;
    private Vector2 _targetCardPosition = Vector2.left * 1.4f;
    private float _cardShowTime = .6f;
    private float _cardShowDelay = .3f;
    private int _animatorDraftHash = Animator.StringToHash("Draft");
    private int _animatorRearrangeHash = Animator.StringToHash("Rearrange");

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

    private List<Card> _draftedCards= new();
    private BoardPosition _draftedBoardPosition;

    private void Awake()
    {
        _tournamentField.onValueChanged.AddListener((t) => _tournamentField.text = t.ToUpper());
        _playerField.onValueChanged.AddListener((t) => _playerField.text = t.ToUpper());
        foreach (var field in _rivalsFields)
            field.onValueChanged.AddListener((t) => field.text = t.ToUpper());
    }

    public void GoToNextStage()
    {
        if (_draftedBoardPosition > BoardPosition.DownBack)
        {
            _animator.SetBool(_animatorRearrangeHash, false);
        }
        else
            _animator.SetBool(_animatorDraftHash, false);
    }

    public override void Activate()
    {
        base.Activate();
        _animator.SetBool(_animatorDraftHash, true);
        _tournamentField.text = $"{_firstNames[Random.Range(0, _firstNames.Length)]} {_secondNames[Random.Range(0, _secondNames.Length)]}";
    }

    private void DraftACard()
    {
        foreach (var card in _draftedCards)
            card.HideFromDraft(card.transform.position.x < 0f ? _cardSpawnPosition : -_cardSpawnPosition);
        _draftedCards.Clear();
        if (_draftedBoardPosition > BoardPosition.DownBack)
        {
            _animator.SetBool(_animatorRearrangeHash, true);
            foreach (var card in GameManager.Instance.GetCardsOfPlayer(0))
                card.ActivatePhase(CardPhase.Arrange);
        }
        else
        {
            _draftedCards.Add(Instantiate(_testCardPrefab, _cardSpawnPosition, Quaternion.identity));
            _draftedCards.Add(Instantiate(_testCardPrefab, -_cardSpawnPosition, Quaternion.identity));
            for (int i = 0; i < _draftedCards.Count; i++)
            {
                var card = _draftedCards[i];
                card.Initialize(_draftedBoardPosition);
                card.OnClicked += DraftACard;
                card.transform.DOMove(_targetCardPosition * (i == 0 ? 1 : -1), _cardShowTime).SetDelay(_cardShowDelay)
                    .OnComplete(() => card.ActivatePhase(CardPhase.Draft));
            }
        }
        _draftedBoardPosition++;
    }

    private void GoToNextGameState()
    {
        OnChangeState?.Invoke();
        gameObject.SetActive(false);
    }
}
