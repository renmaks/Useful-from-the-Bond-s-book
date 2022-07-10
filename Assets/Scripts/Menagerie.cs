using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ePetType
{
    none,
    dog,
    cat,
    bird,
    fish,
    other
}

public enum eLifeStage
{
    baby,
    teen,
    adult,
    senior,
    deceased
}

public interface IAnimal
{
    ePetType pType { get; set; }
    eLifeStage age { get; set; }
    string name { get; set; }

    void Move();
    string Speak();
}

public class Fish : IAnimal
{
    private ePetType _pType = ePetType.fish;
    public ePetType pType
    {
        get { return (_pType); }
        set { _pType = value; }
    }

    public eLifeStage age { get; set; }
    public string name { get; set; }

    public void Move()
    {
        Debug.Log("The fish swims around.");
    }

    public string Speak()
    {
        return ("...!");
    }
}

public class Mammal
{
    protected eLifeStage _age;
    public eLifeStage age
    {
        get { return (_age); }
        set { _age = value; }
    }

    public string name { get; set; }
}

public class Dog: Mammal, IAnimal
{
    private ePetType _pType = ePetType.dog;

    public ePetType pType
    {
        get { return (_pType); }
        set { _pType = value; }
    }

    public void Move()
    {
        Debug.Log("The dog walks around.");
    }

    public string Speak()
    {
        return ("Bark!");
    }
}

public class Cat:Mammal, IAnimal
{
    private ePetType _pType = ePetType.cat;

    public ePetType pType
    {
        get { return (_pType); }
        set { _pType = value; }
    }

    public void Move()
    {
        Debug.Log("The cat stalks around.");
    }

    public string Speak()
    {
        return ("Meow!");
    }
}

public class Menagerie : MonoBehaviour
{
    public List<IAnimal> animals;

    void Awake()
    {
        animals = new List<IAnimal>();

        Dog d = new Dog();
        d.age = eLifeStage.adult;
        animals.Add(d);
        animals.Add(new Cat());
        animals.Add(new Fish());

        animals[0].name = "Wendy";
        animals[1].name = "Caramel";
        animals[2].name = "Nemo";

        string[] types = new string[] { "none", "dog", "cat", "bird", "fish", "other" };
        string[] ages = new string[] { "baby", "teen", "adult", "senior", "deceased" };
        string aName;
        IAnimal animal;
        for (int i = 0; i < animals.Count; i++)
        {
            animal = animals[i];
            aName = animal.name;
            print("Animal #" + i + " is a " + types[(int)animal.pType] + " named " + aName + ".");
            animal.Move();
            print(aName + " says: " + animal.Speak());

            switch (animal.age)
            {
                case eLifeStage.baby:
                case eLifeStage.teen:
                case eLifeStage.senior:
                    print(aName + " is a " + ages[(int)animal.age] + ".");
                    break;
                case eLifeStage.adult:
                    print(aName + " is an adult.");
                    break;
                case eLifeStage.deceased:
                    print(aName + " is deceased.");
                    break;
            }
        }
    }
}
