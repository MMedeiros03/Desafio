namespace Infrastructure.DTO;

public class ErrorDTO
{
    public ErrorDTO(int statusCode, string message)
    {
        TraceId = Guid.NewGuid().ToString();
        StatusCode = statusCode;
        Message = message;
    }

    public string? TraceId { get; set; } = Guid.NewGuid().ToString();
    public int? StatusCode { get; set; }
    public string? Message { get; set; }
}
