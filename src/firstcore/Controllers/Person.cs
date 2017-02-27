public class Person
{
    public Person(string name, int age)
    {
        this.Age = age;
        this.Name = name;
    }
    public int Age { get; set; }
    public string Name { get; set; }
}