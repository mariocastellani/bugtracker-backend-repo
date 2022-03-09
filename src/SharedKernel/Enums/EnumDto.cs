namespace SharedKernel
{
    public class EnumDto
    {
        public int Value { get; set; }

        public string NameOrDescription { get; set; }

        public EnumDto()
        {
        }

        public EnumDto(int value, string nameOrDescription)
        {
            Value = value;
            NameOrDescription = nameOrDescription;
        }

        public EnumDto(Enum enumValue)
        {
            Value = Convert.ToInt32(enumValue);
            NameOrDescription = enumValue.GetNameOrDescription();
        }
    }
}
