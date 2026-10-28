using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using ProdControlWPF.Models;

namespace ProdControlWPF.Data
{
    public sealed class KnittingRoutingRepository : IKnittingRoutingRepository
    {
        private const string ArticulsSql = "SELECT ArticulID, ArticulName FROM Articul";

        private const string PartsSql = "SELECT id, part_name FROM ClothingParts";

        private const string MachinesSql =
            "SELECT id, machine_make + ' ' + machine_model AS machine_displayname FROM KnittingMachines";

        private const string CoefficientsSql =
            "SELECT coefficient_id, coefficient_value FROM Coefficients";

        private const string RoutingSql =
            "SELECT cp.id AS part_id, cp.part_name, km.id AS machine_id, km.machine_make, km.machine_model, " +
            "kr.product_parts_amount, kr.machine_parts_amount, kr.k1, kr.k2, kr.avg_rate, " +
            "kr.coefficient_rate, kr.kit_rate " +
            "FROM (KnittingRouting AS kr " +
            "INNER JOIN ClothingParts AS cp ON kr.clothing_part_id = cp.id) " +
            "INNER JOIN KnittingMachines AS km ON kr.machine_id = km.id " +
            "WHERE kr.articul_id = ?";

        private const string DeleteSql = "DELETE FROM [KnittingRouting] WHERE [articul_id] = ?";

        private const string InsertSql =
            "INSERT INTO [KnittingRouting] " +
            "([articul_id], [clothing_part_id], [machine_id], [product_parts_amount], " +
            "[machine_parts_amount], [k1], [k2], [avg_rate], [coefficient_rate], [kit_rate]) " +
            "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

        private readonly IDbConnectionFactory _connectionFactory;

        public KnittingRoutingRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public IReadOnlyList<ArticulLookup> GetArticuls()
        {
            var result = new List<ArticulLookup>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = ArticulsSql;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new ArticulLookup(
                            Convert.ToInt32(reader["ArticulID"]),
                            reader["ArticulName"] is DBNull
                                ? string.Empty
                                : Convert.ToString(reader["ArticulName"])));
                    }
                }
            }

            return result;
        }

        public IReadOnlyList<ClothingPartLookup> GetParts()
        {
            var result = new List<ClothingPartLookup>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = PartsSql;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new ClothingPartLookup(
                            Convert.ToInt32(reader["id"]),
                            reader["part_name"] is DBNull
                                ? string.Empty
                                : Convert.ToString(reader["part_name"])));
                    }
                }
            }

            return result;
        }

        public IReadOnlyList<KnittingMachineLookup> GetMachines()
        {
            var result = new List<KnittingMachineLookup>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = MachinesSql;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new KnittingMachineLookup(
                            Convert.ToInt32(reader["id"]),
                            reader["machine_displayname"] is DBNull
                                ? string.Empty
                                : Convert.ToString(reader["machine_displayname"])));
                    }
                }
            }

            return result;
        }

        public IReadOnlyList<CoefficientLookup> GetCoefficients()
        {
            var result = new List<CoefficientLookup>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = CoefficientsSql;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new CoefficientLookup(
                            Convert.ToInt32(reader["coefficient_id"]),
                            ReadDouble(reader, "coefficient_value")));
                    }
                }
            }

            return result;
        }

        public IReadOnlyList<KnittingRoutingEntry> GetRouting(int articulId)
        {
            var result = new List<KnittingRoutingEntry>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = RoutingSql;
                command.Parameters.Add("@articulId", OleDbType.Integer).Value = articulId;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var make = reader["machine_make"] is DBNull
                            ? string.Empty
                            : Convert.ToString(reader["machine_make"]);
                        var model = reader["machine_model"] is DBNull
                            ? string.Empty
                            : Convert.ToString(reader["machine_model"]);

                        result.Add(new KnittingRoutingEntry
                        {
                            PartId = Convert.ToInt32(reader["part_id"]),
                            PartName = reader["part_name"] is DBNull
                                ? string.Empty
                                : Convert.ToString(reader["part_name"]),
                            MachineId = Convert.ToInt32(reader["machine_id"]),
                            MachineName = make + " " + model,
                            ProductPartsAmount = ReadInt(reader, "product_parts_amount"),
                            MachinePartsAmount = ReadInt(reader, "machine_parts_amount"),
                            K1 = ReadDouble(reader, "k1"),
                            K2 = ReadDouble(reader, "k2"),
                            AvgRate = ReadDouble(reader, "avg_rate"),
                            CoefficientRate = ReadDouble(reader, "coefficient_rate"),
                            KitRate = ReadDouble(reader, "kit_rate")
                        });
                    }
                }
            }

            return result;
        }

        public void Save(int articulId, IReadOnlyList<KnittingRoutingEntry> entries)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var deleteCommand = connection.CreateCommand())
                        {
                            deleteCommand.Transaction = transaction;
                            deleteCommand.CommandType = CommandType.Text;
                            deleteCommand.CommandText = DeleteSql;
                            deleteCommand.Parameters.Add("@articulId", OleDbType.Integer).Value = articulId;
                            deleteCommand.ExecuteNonQuery();
                        }

                        if (entries != null)
                        {
                            foreach (var entry in entries)
                            {
                                using (var insertCommand = connection.CreateCommand())
                                {
                                    insertCommand.Transaction = transaction;
                                    insertCommand.CommandType = CommandType.Text;
                                    insertCommand.CommandText = InsertSql;

                                    insertCommand.Parameters.Add("@articulId", OleDbType.Integer).Value = articulId;
                                    insertCommand.Parameters.Add("@partId", OleDbType.Integer).Value = entry.PartId;
                                    insertCommand.Parameters.Add("@machineId", OleDbType.Integer).Value = entry.MachineId;
                                    insertCommand.Parameters.Add("@productPartsAmount", OleDbType.Integer).Value =
                                        entry.ProductPartsAmount;
                                    insertCommand.Parameters.Add("@machinePartsAmount", OleDbType.Integer).Value =
                                        entry.MachinePartsAmount;
                                    insertCommand.Parameters.Add("@k1", OleDbType.Double).Value = entry.K1;
                                    insertCommand.Parameters.Add("@k2", OleDbType.Double).Value = entry.K2;
                                    insertCommand.Parameters.Add("@avgRate", OleDbType.Double).Value = entry.AvgRate;
                                    insertCommand.Parameters.Add("@coefficientRate", OleDbType.Double).Value =
                                        entry.CoefficientRate;
                                    insertCommand.Parameters.Add("@kitRate", OleDbType.Double).Value = entry.KitRate;

                                    insertCommand.ExecuteNonQuery();
                                }
                            }
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception)
                        {
                        }

                        throw;
                    }
                }
            }
        }

        private static int ReadInt(OleDbDataReader reader, string columnName)
        {
            var value = reader[columnName];

            return value is DBNull
                ? 0
                : Convert.ToInt32(value, CultureInfo.InvariantCulture);
        }

        private static double ReadDouble(OleDbDataReader reader, string columnName)
        {
            var value = reader[columnName];

            return value is DBNull
                ? 0
                : Convert.ToDouble(value, CultureInfo.InvariantCulture);
        }
    }
}
