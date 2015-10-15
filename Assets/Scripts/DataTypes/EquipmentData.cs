[System.Serializable]
public struct EquipmentData {

	public enum Type { Head, Body, Feet };

	public Type type;
	public string name;
	public int armor;
	public float speed;
	// sprite reference?

	public EquipmentData( Type type, string name, int armor, float speed )
	{
		this.type = type;
		this.name = name;
		this.armor = armor;
		this.speed = speed;
	}
}
