namespace Web.Features.SwaggerSchema.SaveWithoutInheritance;

public class SaveWithoutInheritance : Endpoint<Request>
{
    public override void Configure()
    {
        Put("SwaggerSchema/SaveWithoutInheritance");
        AllowAnonymous();
        Summary(summary => summary.Description = $"This is testing '{nameof(SaveWithoutInheritance)}'");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        await SendOkAsync(ct);
    }
}

public class Request
{
    public Guid Identifier { get; set; } = Guid.NewGuid();
    public Guid? NullableIdentifier { get; set; }
    public string? Message { get; set; }
}


public class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.Identifier).NotEmpty();
        RuleFor(x => x.NullableIdentifier).NotEmpty();
        RuleFor(x => x.Message).Length(3, 100);
    }
}