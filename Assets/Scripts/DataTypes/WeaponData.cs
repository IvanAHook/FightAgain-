[System.Serializable]
public struct WeaponData {

	enum Upgrades { ball, boot, dick }

	public enum Type { Melee, Ranged };

	public Type type;
	public string name;
	public float damage;
	// sprite reference?

	public WeaponData( Type type, string name, float damage )
	{
		this.type = type;
		this.name = name;
		this.damage = damage;
	}

}
