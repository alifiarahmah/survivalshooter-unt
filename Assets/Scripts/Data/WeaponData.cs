using System;

[Serializable]
public class WeaponData
{
    public string name;
    public int price;
    public string filename;
    public int id;

    public WeaponData(string name, int price, string filename, int id)
    {
        this.name = name;
        this.price = price;
        this.filename = filename;
        this.id = id;
    }

    public override string ToString()
    {
        return $"{name} have reached price {price}";
    }
}


