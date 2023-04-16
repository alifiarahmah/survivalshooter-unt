using System;

[Serializable]
public class PetData
{
    public string name;
    public string buffType;
    public float buffAmount;
    public int price;
    public string filename;
    public int id;

    public PetData(string name, string buffType, float buffAmount, int price, string filename, int id)
    {
        this.name = name;
        this.buffType = buffType;
        this.buffAmount = buffAmount;
        this.price = price;
        this.filename = filename;
        this.id = id;
    }

    public override string ToString()
    {
        return $"{name} has {buffType} with {buffAmount} buffAmount. They have reached price {price}";
    }
}


