namespace SharedKernel.Result
{
    public interface IResult
    {
        ResultStatus Status { get; }
        
        List<ValidationError> ValidationErrors { get; }
        
        Type ValueType { get; }
        
        object GetValue();
    }
}