[System.Serializable]
public struct WeaponData {
	public string name;
	public int damage;
	public int attackSpeed;
	// sprite reference?

	public WeaponData( string name, int damage, int attackSpeed )
	{
		this.name = name;
		this.damage = damage;
		this.attackSpeed = attackSpeed;
	}
}
