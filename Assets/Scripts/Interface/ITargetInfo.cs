using UnityEngine;

public interface ITargetInfo
{
    Sprite Icon { get; }
    string DisplayName { get; }
    int MaxHealth { get; }
    int CurrentHealth { get; }
}
