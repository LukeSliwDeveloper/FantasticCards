using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Encounter : SerializedScriptableObject
{
    [field: SerializeField] public Material CharacterMaterial { get; private set; }
    [field: SerializeField] public string Dialogue { get; private set; }
    [field: SerializeField] public Dictionary<FlowerType[], Encounter> FollowingEncounters { get; private set; }
}
