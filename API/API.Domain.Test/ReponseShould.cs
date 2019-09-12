using System;
using System.Collections.Generic;
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

        private readonly TestDto _defaultDto;       

        private Response<TestDto> _response;

        public ResponseShould()
        {
            _defaultDto = new TestDto();

            _response = Response<TestDto>.Valid(_defaultDto);
        }

        [Fact]
        public void ReturnValidReponseWhenCallingValid()
        {
            Assert.True(_response.IsValid);
        }

        [Fact]
        public void HaveEmptyErrorListWhenCallingValid()
        {
            Assert.Empty(_response.Errors);
        }

        [Fact]
        public void SetDataWhenCallingValid()
        {
            // arrange
            var guid = Guid.NewGuid().ToString();
            _defaultDto.Uuid = guid;

            // act
            _response = Response<TestDto>.Valid(_defaultDto);

            // assert
            Assert.Equal(guid, _response.Data.Uuid);
        }

        [Fact]
        public void CreateInvalidResponseWhenCallingInvalid()
        {
            // act
            _response = Response<TestDto>.Invalid(new List<string> { "error" });

            // assert
            Assert.False(_response.IsValid);
        }

        [Fact]
        public void HaveItemsInErrorCollectionWhenCallingInvalid()
        {
            // act
            _response = Response<TestDto>.Invalid(new List<string> { "error" });

            // assert
            Assert.NotEmpty(_response.Errors);
        }
    }
}
