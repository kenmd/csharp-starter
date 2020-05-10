using Dapper;


namespace MyFunction
{
    public class Person
    {
        [Key]
        public int ID { get; set; }
        public string NAME { get; set; }
    }
}
