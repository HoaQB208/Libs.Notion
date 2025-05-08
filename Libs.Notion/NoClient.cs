using Notion.Client;

namespace Libs.Notion
{
    public class NoClient
    {
        /// <summary>
        /// Get Notion Client
        /// </summary>
        /// <param name="token">https://www.notion.so/my-integrations</param>
        /// Chia sẻ page với Client API này: Dấu 3 chấm (trên, phải) >> Connections >> Chọn Client API đã tạo
        /// <returns></returns>
        public static NotionClient Get(string token)
        {
            return NotionClientFactory.Create(new ClientOptions
            {
                AuthToken = token
            });
        }
    }
}