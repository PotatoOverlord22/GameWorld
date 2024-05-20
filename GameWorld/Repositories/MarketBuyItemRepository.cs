﻿using Microsoft.Data.SqlClient;
using GameWorld.Entities;
using GameWorld.Utils;

namespace GameWorld.Repositories
{
    public class MarketBuyItemRepository : IMarketBuyItemRepository
    {
        private readonly string connectionString = DatabaseHelper.GetDatabaseFilePath();

        public async Task<List<MarketBuyItem>> GetAllMarketBuyItemsAsync()
        {
            List<MarketBuyItem> marketBuyItems = new List<MarketBuyItem>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SELECT * FROM MarketBuyItems", connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            marketBuyItems.Add(new MarketBuyItem(
                                id: (Guid)reader["Id"],
                                itemId: (Guid)reader["ItemId"],
                                buyPrice: (int)reader["BuyPrice"]));
                        }
                    }
                }
            }
            return marketBuyItems;
        }

        public async Task<MarketBuyItem> GetMarketBuyItemByItemIdAsync(Guid itemId)
        {
            MarketBuyItem? marketBuyItem = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SELECT * FROM MarketBuyItems WHERE ItemId = @ItemId", connection))
                {
                    command.Parameters.AddWithValue("@ItemId", itemId);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            marketBuyItem = new MarketBuyItem(
                                id: (Guid)reader["Id"],
                                itemId: (Guid)reader["ItemId"],
                                buyPrice: (int)reader["BuyPrice"]);
                        }
                    }
                }
            }
            return marketBuyItem;
        }

        public async Task AddMarketBuyItemAsync(MarketBuyItem marketBuyItem)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO MarketBuyItems (Id, ItemId, BuyPrice) VALUES (@Id, @ItemId, @BuyPrice)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", marketBuyItem.Id);
                    command.Parameters.AddWithValue("@ItemId", marketBuyItem.ItemId);
                    command.Parameters.AddWithValue("@BuyPrice", marketBuyItem.BuyPrice);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateMarketBuyItemAsync(MarketBuyItem marketBuyItem)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE MarketBuyItems SET ItemId = @ItemId, BuyPrice = @BuyPrice WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", marketBuyItem.Id);
                    command.Parameters.AddWithValue("@ItemId", marketBuyItem.ItemId);
                    command.Parameters.AddWithValue("@BuyPrice", marketBuyItem.BuyPrice);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteMarketBuyItemAsync(Guid marketBuyItemId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM MarketBuyItems WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", marketBuyItemId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
