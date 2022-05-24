namespace Web.Features.SwaggerSchema.SaveWithChildRules;

public class SaveWithChildRules : Endpoint<Request>
{
    public override void Configure()
    {
        Put("SwaggerSchema/SaveWithChildRules");
        AllowAnonymous();
        Summary(summary => summary.Description = $"This is testing rules for child collections '{nameof(SaveWithChildRules)}'");
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
    public List<RequestChild> RequestChildren { get; set; }
}

public class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.Identifier).NotEmpty();
        RuleFor(x => x.RequestChildren)
            .NotNull()
            .NotEmpty()
            .ChildRules(children => { children.RuleFor(x => x.Count).GreaterThan(2); }).ForEach(child => { child.NotEmpty(); });
        RuleForEach(x => x.RequestChildren)
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