namespace Sponsorship.Application.Common.Results
{
    public interface IResult
    {
        List<string> Messages { get; set; }

        bool Succeeded { get; set; }
        int StatusCode { get; set; }
    }

    public interface IResult<out T> : IResult
    {
        T? Data { get; }
    }
}
