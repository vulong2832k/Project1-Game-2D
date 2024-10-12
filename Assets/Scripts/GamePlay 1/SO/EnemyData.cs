using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Default Properties:")]
    public float dropChangeBuffSpeedItem = 0.05f;
    public float dropChangeBoxAmmoItem = 0.1f;

    public int defaultHealth;
    public float damage;
    public float speed;
    
    public float damageBonus = 0f;
    public float healthBonus = 0f;

}
