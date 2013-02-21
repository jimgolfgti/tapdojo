using System.Threading.Tasks;

namespace tapdojo.tests.TAP
{
    public static class Addition
    {
        public static async Task<int> AddAsync(int augend, int addend)
        {
            await Task.Delay(500);

            return 3;
        }
    }
}