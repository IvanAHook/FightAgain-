[System.Serializable]
public struct WeaponData {

	public enum Type { Melee, Ranged };

	public Type type;
	public string name;
	public int damage;
	// sprite reference?

	public WeaponData( Type type, string name, int damage )
	{
		this.type = type;
		this.name = name;
		this.damage = damage;
	}
}
