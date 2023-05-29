using System;

namespace Domain.Result
{
    public interface IResult
    {
        Error Error { get; }
        bool IsSuccessful => this.Error == null;
        bool IsFailed => !this.IsSuccessful;
        bool IsUnprocessable => this.IsFailed && this.Error.Type == ErrorType.Unprocessable;
        bool IsForbidden => this.IsFailed && this.Error.Type == ErrorType.Forbidden;
        bool IsBadRequest => this.IsFailed && this.Error.Type == ErrorType.BadRequest;
        bool IsNotFound => this.IsFailed && this.Error.Type == ErrorType.NotFound;
        bool IsConflict => this.IsFailed && this.Error.Type == ErrorType.Conflict;
    }

    public interface IResult<TData> : IResult
    {
        TData Data { get; }
        TData Unwrap(TData defaultData = default)
            => this.IsSuccessful ? this.Data : defaultData;

        IResult<TData> Map(Func<TData, TData> mapper)
            => this.IsFailed
                ? this
                : Result.Success<TData>(mapper(this.Data));
        IResult<TData> MapWhen(bool predicate, Func<TData, TData> mapper)
            => !predicate
                ? this
                : this.Map(mapper);
    }
}