using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using ProdControlWPF.Models;

namespace ProdControlWPF.Data
{
    public sealed class WorkOrderRepository : IWorkOrderRepository
    {
        private const string RowsSql =
            "SELECT [IDнаряда], [Заказ], [ДатаНаряда], [№наряда], [КодНаряда], [№машины], " +
            "[Рост], [РостNEW], [Размер], [РазмерNEW], [ПланКол-во], [ФактКол-во], " +
            "[ФактВес], [ДатаНарядаФакт], [Вязальщица1], [Вяз1%], [Вязальщица2], [Вяз2%], " +
            "[Вязальщица3], [Вяз3%], [ВТО1], [ДатаВТОФакт], [Стирка1], [ДатаСтиркиФакт] " +
            "FROM [НарядыЗапрос] ORDER BY [IDнаряда] DESC";

        private const string OrdersSql =
            "SELECT [Заказ], [Заказчики.Заказчик], [Артикул], [Цвет], [Оборудование], [ДатаЗаказа] " +
            "FROM [ЗаказыЗапрос]";

        private const string OrderPlansSql =
            "SELECT [Заказ], SUM([ЗаказКол-во]) AS [PlannedQuantity] " +
            "FROM [ЗаказыИнфо] WHERE [Испорчен] = False GROUP BY [Заказ]";

        private const string KnittersSql =
            "SELECT [КодВязальщицы], [Вязальщица] FROM [Вязальщицы] ORDER BY [Вязальщица]";

        private const string FormationWorkersSql =
            "SELECT [КодФормировщица], [Формировщица] FROM [Формировщицы] ORDER BY [Формировщица]";

        private const string UpdateSql =
            "UPDATE [Наряды] SET [ФактКол-во] = ?, [ФактВес] = ?, [ДатаНарядаФакт] = ?, " +
            "[РостNEW] = ?, [РазмерNEW] = ?, [Вязальщица1] = ?, [Вяз1%] = ?, " +
            "[Вязальщица2] = ?, [Вяз2%] = ?, [Вязальщица3] = ?, [Вяз3%] = ?, " +
            "[ВТО1] = ?, [ДатаВТОФакт] = ?, [Стирка1] = ?, [ДатаСтиркиФакт] = ? " +
            "WHERE [IDнаряда] = ?";

        private readonly IDbConnectionFactory _connectionFactory;

        public WorkOrderRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public IReadOnlyList<WorkOrderRow> GetRows()
        {
            var result = new List<WorkOrderRow>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = RowsSql;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new WorkOrderRow
                        {
                            Id = ReadInt32(reader, 0),
                            OrderNumber = ReadString(reader, 1),
                            OrderDate = ReadNullableDateTime(reader, 2),
                            RowNumber = ReadNullableInt32(reader, 3),
                            Code = ReadString(reader, 4),
                            Machine = ReadString(reader, 5),
                            Height = ReadString(reader, 6),
                            HeightNew = ReadString(reader, 7),
                            Size = ReadString(reader, 8),
                            SizeNew = ReadString(reader, 9),
                            PlanQuantity = ReadInt32(reader, 10),
                            FactQuantity = ReadInt32(reader, 11),
                            FactWeight = ReadNullableDecimal(reader, 12),
                            FactDate = ReadNullableDateTime(reader, 13),
                            Knitter1Id = ReadNullableInt32(reader, 14),
                            Knitter1Percent = ReadNullableDecimal(reader, 15),
                            Knitter2Id = ReadNullableInt32(reader, 16),
                            Knitter2Percent = ReadNullableDecimal(reader, 17),
                            Knitter3Id = ReadNullableInt32(reader, 18),
                            Knitter3Percent = ReadNullableDecimal(reader, 19),
                            VtoWorkerId = ReadNullableInt32(reader, 20),
                            VtoDate = ReadNullableDateTime(reader, 21),
                            WashWorkerId = ReadNullableInt32(reader, 22),
                            WashDate = ReadNullableDateTime(reader, 23)
                        });
                    }
                }
            }

            return result;
        }

        public IReadOnlyList<WorkOrderOrderInfo> GetOrders()
        {
            var orders = new List<WorkOrderOrderInfo>();
            var ordersByNumber = new Dictionary<string, WorkOrderOrderInfo>(StringComparer.Ordinal);

            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = OrdersSql;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var orderNumber = ReadString(reader, 0).Trim();

                            if (orderNumber.Length == 0 || ordersByNumber.ContainsKey(orderNumber))
                            {
                                continue;
                            }

                            var order = new WorkOrderOrderInfo
                            {
                                OrderNumber = orderNumber,
                                CustomerName = ReadString(reader, 1),
                                ArticleName = ReadString(reader, 2),
                                ColorName = ReadString(reader, 3),
                                EquipmentName = ReadString(reader, 4),
                                OrderDate = ReadNullableDateTime(reader, 5)
                            };

                            orders.Add(order);
                            ordersByNumber[orderNumber] = order;
                        }
                    }
                }

                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = OrderPlansSql;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var orderNumber = ReadString(reader, 0).Trim();

                            if (orderNumber.Length == 0
                                || !ordersByNumber.TryGetValue(orderNumber, out var order))
                            {
                                continue;
                            }

                            var planned = Convert.ToDecimal(
                                reader.GetValue(1),
                                CultureInfo.InvariantCulture);

                            order.PlannedQuantity = Convert.ToInt32(
                                Math.Round(planned, MidpointRounding.AwayFromZero));
                        }
                    }
                }
            }

            return orders;
        }

        public IReadOnlyList<WorkOrderLookup> GetKnitters()
        {
            return ReadLookups(KnittersSql);
        }

        public IReadOnlyList<WorkOrderLookup> GetFormationWorkers()
        {
            return ReadLookups(FormationWorkersSql);
        }

        public void Update(WorkOrderUpdate update)
        {
            if (update == null)
            {
                throw new ArgumentNullException(nameof(update));
            }

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = UpdateSql;

                command.Parameters.Add("@factQuantity", OleDbType.Integer).Value = update.FactQuantity;
                command.Parameters.Add("@factWeight", OleDbType.Single).Value = ToSingleDbValue(update.FactWeight);
                command.Parameters.Add("@factDate", OleDbType.Date).Value = ToDbValue(update.FactDate);
                command.Parameters.Add("@heightNew", OleDbType.VarWChar, 50).Value = ToDbValue(update.HeightNew);
                command.Parameters.Add("@sizeNew", OleDbType.VarWChar, 50).Value = ToDbValue(update.SizeNew);
                command.Parameters.Add("@knitter1", OleDbType.Integer).Value = ToDbValue(update.Knitter1Id);
                command.Parameters.Add("@knitter1Percent", OleDbType.Single).Value =
                    ToSingleDbValue(update.Knitter1Percent);
                command.Parameters.Add("@knitter2", OleDbType.Integer).Value = ToDbValue(update.Knitter2Id);
                command.Parameters.Add("@knitter2Percent", OleDbType.Single).Value =
                    ToSingleDbValue(update.Knitter2Percent);
                command.Parameters.Add("@knitter3", OleDbType.Integer).Value = ToDbValue(update.Knitter3Id);
                command.Parameters.Add("@knitter3Percent", OleDbType.Single).Value =
                    ToSingleDbValue(update.Knitter3Percent);
                command.Parameters.Add("@vtoWorker", OleDbType.Integer).Value = ToDbValue(update.VtoWorkerId);
                command.Parameters.Add("@vtoDate", OleDbType.Date).Value = ToDbValue(update.VtoDate);
                command.Parameters.Add("@washWorker", OleDbType.Integer).Value = ToDbValue(update.WashWorkerId);
                command.Parameters.Add("@washDate", OleDbType.Date).Value = ToDbValue(update.WashDate);
                command.Parameters.Add("@id", OleDbType.Integer).Value = update.Id;

                command.ExecuteNonQuery();
            }
        }

        private IReadOnlyList<WorkOrderLookup> ReadLookups(string sql)
        {
            var result = new List<WorkOrderLookup>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = sql;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = ReadString(reader, 1).Trim();

                        if (name.Length == 0)
                        {
                            continue;
                        }

                        result.Add(new WorkOrderLookup(ReadInt32(reader, 0), name));
                    }
                }
            }

            return result;
        }

        private static object ToDbValue(int? value)
        {
            return value.HasValue && value.Value > 0
                ? (object)value.Value
                : DBNull.Value;
        }

        private static object ToDbValue(DateTime? value)
        {
            return value.HasValue ? (object)value.Value.Date : DBNull.Value;
        }

        private static object ToDbValue(string value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? (object)DBNull.Value
                : value.Trim();
        }

        private static object ToSingleDbValue(decimal? value)
        {
            return value.HasValue ? (object)(float)value.Value : DBNull.Value;
        }

        private static string ReadString(OleDbDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal)
                ? string.Empty
                : Convert.ToString(reader.GetValue(ordinal), CultureInfo.CurrentCulture) ?? string.Empty;
        }

        private static int ReadInt32(OleDbDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal)
                ? 0
                : Convert.ToInt32(reader.GetValue(ordinal), CultureInfo.InvariantCulture);
        }

        private static int? ReadNullableInt32(OleDbDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal)
                ? (int?)null
                : Convert.ToInt32(reader.GetValue(ordinal), CultureInfo.InvariantCulture);
        }

        private static decimal? ReadNullableDecimal(OleDbDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal)
                ? (decimal?)null
                : Convert.ToDecimal(reader.GetValue(ordinal), CultureInfo.InvariantCulture);
        }

        private static DateTime? ReadNullableDateTime(OleDbDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal)
                ? (DateTime?)null
                : Convert.ToDateTime(reader.GetValue(ordinal), CultureInfo.CurrentCulture);
        }
    }
}
