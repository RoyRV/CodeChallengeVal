using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ValantDemoApi.Models.UploadMaze;

public sealed record UploadMazeRequest(string FileName, List<string> MazeFile);

public class UploadMazeRequestValidator : AbstractValidator<UploadMazeRequest>
{
  const string ALLOWED_CHARS = "SOXE";
  public UploadMazeRequestValidator()
  {
    RuleFor(request => request.FileName)
      .NotNull()
      .NotEmpty()
        .WithMessage("FileName is required.")
      .Must(fileName => fileName is not null && fileName.EndsWith(".txt", StringComparison.InvariantCultureIgnoreCase))
        .WithMessage("FileName must end with '.txt'.");
    RuleFor(request => request.MazeFile)
      .NotNull()
      .NotEmpty()
        .WithMessage("MazeFile is required.")
       .Must(mazeFile => mazeFile.Count > 1)
        .WithMessage("MazeFile must have at least 2 elements.")
       .Must(mazeFile => mazeFile.Count < 26)
        .WithMessage("MazeFile must contain at most 25 elements.");


    RuleForEach(request => request.MazeFile)
           .Must(mazeLine => mazeLine.All(character => ALLOWED_CHARS.Contains(character, StringComparison.InvariantCultureIgnoreCase)))
           .WithMessage("Each string in MazeFile must contain only the characters 'S', 'O', 'X', and 'E'.");
  }
}
