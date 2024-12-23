using Children.Db2;

namespace Children.Tests
{
    public class MappingTests
    {
        private readonly ChildMapper _mapper = new();

        [Fact]
        public Task Map_X5T78_To_Girl()
        {
            var child = _mapper.ToDto(FakeX5T78.CreateGirl());

            return Verify(child);
        }

        [Fact]
        public Task Map_X5T78_To_Child_For_A_Boy()
        {
            var child = _mapper.ToDto(FakeX5T78.CreateBoy());

            return Verify(child);
        }
    }
}
