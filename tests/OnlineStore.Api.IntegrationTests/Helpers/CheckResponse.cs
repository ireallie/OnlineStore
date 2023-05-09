using OnlineStore.BusinessLogic.Contracts.Dtos;
using FluentAssertions;

namespace OnlineStore.Api.IntegrationTests.Helpers
{
    public static class CheckResponse
    {
        public static void Succeeded<T>(ApiResult<T> result)
        {
            result.IsSucceeded.Should().BeTrue();
            result.Errors.Should().BeEmpty();
            result.Data.Should().NotBe(default);
        }

        public static void Failure<T>(ApiResult<T> result)
        {
            result.IsSucceeded.Should().BeFalse();
            result.Errors.Should().HaveCountGreaterThan(0);
            result.Data.Should().Be(default);
        }
    }
}
