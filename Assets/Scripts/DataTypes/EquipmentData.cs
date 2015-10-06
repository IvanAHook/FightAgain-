[System.Serializable]
public struct EquipmentData {

	public enum Type { Head, Body, Feet };

	public Type type;
	public string name;
	public int armor;
	public int speed;
	// sprite reference?

	public EquipmentData( Type type, string name, int damage, int attackSpeed )
	{
		this.type = type;
		this.name = name;
		this.armor = damage;
		this.speed = attackSpeed;
	}
}
