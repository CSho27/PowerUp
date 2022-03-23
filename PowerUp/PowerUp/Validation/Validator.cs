using System;

namespace PowerUp.Validation
{
  public abstract class Validator<TParameters>
  {
    public abstract void Validate(TParameters parameters);

    protected void ThrowIfNull(object? value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof(value));
    }

    protected void ThrowIfNullOrEmpty(string? value)
    {
      if(string.IsNullOrEmpty(value))
        throw new ArgumentOutOfRangeException(nameof(value));
    }

    protected void ThrowIfLongerThanMaxLength(string? value, int maxLength)
    {
      if (value != null && value.Length > maxLength)
        throw new ArgumentOutOfRangeException(nameof(value));
    }

    protected void ThrowIfNotBetween(int? value, int min, int max)
    {
      if(value < min || value > max)
        throw new ArgumentOutOfRangeException(nameof(value));
    }
  }
}
