using FluentValidation;

namespace ValantDemoApi.Models.GetMazes;

public sealed record GetMazesRequest(int StartIndex, int Size);

public class GetMazeRequestValidator : AbstractValidator<GetMazesRequest>
{
  public GetMazeRequestValidator()
  {
    RuleFor(request => request.StartIndex)
      .GreaterThanOrEqualTo(0);

    RuleFor(request => request.Size)
      .GreaterThanOrEqualTo(1)
      .LessThanOrEqualTo(100);
  }
}
