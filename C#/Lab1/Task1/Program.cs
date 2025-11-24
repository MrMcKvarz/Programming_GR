class Address
{
    private int index;
    private string country;
    private string city;
    private string street;
    private int house;
    private int apartment;

    public int Index { get => index; set => index = value; }
    public string Country { get => country; set => country = value; }
    public string City { get => city; set => city = value; }
    public int House { get => house; set => house = value; }
    public int Apartment { get => apartment; set => apartment = value; }
    public string Street { get => street; set => street = value; }
    public void print()
    {
        Console.WriteLine("Adress: ");
        Console.WriteLine(index);
        Console.WriteLine(country);
        Console.WriteLine(city);
        Console.WriteLine(street);
        Console.WriteLine(house);
        Console.WriteLine(apartment);
    }


    static void Main(string[] args)
    {
        Address adress = new Address();
        adress.Index = 65102;
        adress.Country = "Ukraine";
        adress.city = "Kyiv";
        adress.street = "Heroiv Dnipra";
        adress.house = 94;
        adress.Apartment = 32;

        adress.print();
    }
}

