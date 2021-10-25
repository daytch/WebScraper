using WebScraper.DataAccess.Model;
using System.Threading.Tasks;

namespace WebScraper.DataAccess.Interface
{
    public interface IConfig
    {
        Task<string> GetValue(Config config);
    }
}
