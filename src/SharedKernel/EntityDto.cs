namespace SharedKernel
{
    /// <summary>
    /// Dto of an entity.
    /// Can be inherited for more complex dtos.
    /// </summary>
    public class EntityDto
    {
        public int Id { get; set; }

        public EntityDto()
        {
        }

        public EntityDto(int id)
        {
            Id = id;
        }

        public EntityDto(IEntity entity)
        {
            Id = entity?.Id ?? 0;
        }
    }
}