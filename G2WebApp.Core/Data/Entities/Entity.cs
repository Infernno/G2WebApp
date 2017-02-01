using LinqToDB.Mapping;

namespace G2WebApp.Core.Data.Entities
{
    public class Entity
    {
        [Column(Name = "Id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
    }
}
