namespace Web.Features.SwaggerSchema.SaveWithChildValidator;

public class SaveWithChildValidator : Endpoint<Request>
{
    public override void Configure()
    {
        Put("SwaggerSchema/SaveWithChildValidator");
        AllowAnonymous();
        Summary(summary => summary.Description = $"This is testing '{nameof(SaveWithChildValidator)}'");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        await SendOkAsync(ct);
    }
}

public class RequestChild
{
    public Guid? NullableIdentifier { get; set; }
    public string? Message { get; set; }
}

public class Request
{
    public Guid Identifier { get; set; }
    public RequestChild? RequestChild { get; set; }
}

public class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.Identifier).NotEmpty();
        RuleFor(x => x.RequestChild).NotNull()
            .SetValidator(new RequestChildValidator());
    }
}

public class RequestChildValidator : Validator<RequestChild>
{
    public RequestChildValidator()
    {
        RuleFor(x => x.NullableIdentifier).NotEmpty();
        RuleFor(x => x.Message).Length(3, 100);
    }
}