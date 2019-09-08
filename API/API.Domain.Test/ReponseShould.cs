using System;
using API.Domain.Common;
using Xunit;

namespace API.Domain.Test
{
    public class ResponseShould
    {
        private sealed class TestDto
        {
            public string Uuid { get; set; }
        }

        private Response<TestDto> _response;

        public ResponseShould()
        {
            _response = new Response<TestDto>();
        }

        [Fact]
        public void BeValidByDefault()
        {
            Assert.True(_response.IsValid);
        }

        [Fact]
        public void HaveAlreadyCreatedErrorList()
        {
            Assert.NotNull(_response.Errors);
        }

        [Fact]
        public void BeValidByDefaultForDataCtor()
        {
            // arrange
            _response = new Response<TestDto>(new TestDto());

            // assert
            Assert.True(_response.IsValid);
        }

        [Fact]
        public void HaveAlreadyCreatedErrorListForDataCtor()
        {
            // arrange
            _response = new Response<TestDto>(new TestDto());

            // assert
            Assert.NotNull(_response.Errors);
        }

        [Fact]
        public void HaveDataInitialized()
        {
            // arrange
            var guid = Guid.NewGuid().ToString();
            var testData = new TestDto { Uuid = guid };

            // act
            _response = new Response<TestDto>(testData);

            // assert
            Assert.Equal(guid, _response.Data.Uuid);
        }
    }
}
