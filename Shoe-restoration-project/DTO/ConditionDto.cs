using Shoe_restoration_project.Models;

namespace Shoe_restoration_project.DTO
{
    public class ConditionDto : IDtoObject<Condition>
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public Condition GetModel()
        {
            return new Condition()
            {
                Id = this.Id,
                Name = this.Name
            };
        }

        public static IDtoObject<Condition> FromModel(Condition condition)
        {
            return new ConditionDto()
            {
                Id = condition.Id,
                Name = condition.Name
            };
        }
    }
}
