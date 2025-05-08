using Notion.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libs.Notion
{
    public class NoDatabase
    {
        /// <summary>
        /// Database functs
        /// </summary>
        /// <param name="client"></param>
        /// <param name="databaseId">Chuỗi 32 ký tự sau tên miền và trước tham số '?v='
        /// và cần chia sẻ database với Client API đã tạo: Dấu 3 chấm (trên, phải) >> Connections >> Chọn Client API đã tạo</param>
        /// <returns></returns>
        public static async Task<List<Page>> Get(NotionClient client, string databaseId)
        {
            var rows = new List<Page>();

            try
            {
                var queryParams = new DatabasesQueryParameters();
                var queryResponse = await client.Databases.QueryAsync(databaseId, queryParams);
                rows.AddRange(queryResponse.Results);
                while (queryResponse.HasMore)
                {
                    queryParams.StartCursor = queryResponse.NextCursor;
                    queryResponse = await client.Databases.QueryAsync(databaseId, queryParams);
                    rows.AddRange(queryResponse.Results);
                }
            }
            catch (NotionApiException apiEx)
            {
                Console.WriteLine($"API Error: {apiEx.StatusCode} - {apiEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
            }
            return rows;
        }

        public static async Task Update(NotionClient client, string databaseId, string rowId, string newContent)
        {
            // Tạo bản cập nhật cho cột "Content"
            var updatedProperties = new Dictionary<string, PropertyValue>
            {
                {
                    "Content", new RichTextPropertyValue
                    {
                        RichText = new List<RichTextBase>
                        {
                            new RichTextText
                            {
                                Text = new Text
                                {
                                    Content = newContent
                                }
                            }
                        }
                    }
                }
            };

            // Cập nhật hàng đầu tiên
            await client.Pages.UpdateAsync(rowId, new PagesUpdateParameters
            {
                Properties = updatedProperties
            });
        }
    }
}