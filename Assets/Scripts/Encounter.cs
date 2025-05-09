using UnityEngine;

[CreateAssetMenu]
public class Encounter : ScriptableObject
{
    [field: SerializeField] public FlowerType[] RequiredFlowers { get; private set; }
    [field: SerializeField] public Sprite CharacterSprite { get; private set; }
    [field: SerializeField] public string Dialogue { get; private set; }
    [field: SerializeField] public Encounter[] FollowingEncounters { get; private set; }
}
