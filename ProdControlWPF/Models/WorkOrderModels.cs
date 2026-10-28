using System;
using System.Globalization;
using ProdControlWPF.Domain;
using ProdControlWPF.Infrastructure;

namespace ProdControlWPF.Models
{
    public enum WorkOrderMode
    {
        Active = 0,
        Completed = 1
    }

    public sealed class WorkOrderLookup
    {
        public WorkOrderLookup(int id, string name)
        {
            Id = id;
            Name = name ?? string.Empty;
        }

        public int Id { get; }

        public string Name { get; }

        public bool IsEmpty
        {
            get { return Id <= 0; }
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public sealed class WorkOrderOrderInfo
    {
        public string OrderNumber { get; set; } = string.Empty;

        public string CustomerName { get; set; } = string.Empty;

        public string ArticleName { get; set; } = string.Empty;

        public string ColorName { get; set; } = string.Empty;

        public string EquipmentName { get; set; } = string.Empty;

        public DateTime? OrderDate { get; set; }

        public int? PlannedQuantity { get; set; }
    }

    public sealed class WorkOrderRow
    {
        public int Id { get; set; }

        public string OrderNumber { get; set; } = string.Empty;

        public DateTime? OrderDate { get; set; }

        public int? RowNumber { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Machine { get; set; } = string.Empty;

        public string Height { get; set; } = string.Empty;

        public string HeightNew { get; set; } = string.Empty;

        public string Size { get; set; } = string.Empty;

        public string SizeNew { get; set; } = string.Empty;

        public int PlanQuantity { get; set; }

        public int FactQuantity { get; set; }

        public decimal? FactWeight { get; set; }

        public DateTime? FactDate { get; set; }

        public int? Knitter1Id { get; set; }

        public decimal? Knitter1Percent { get; set; }

        public int? Knitter2Id { get; set; }

        public decimal? Knitter2Percent { get; set; }

        public int? Knitter3Id { get; set; }

        public decimal? Knitter3Percent { get; set; }

        public int? VtoWorkerId { get; set; }

        public DateTime? VtoDate { get; set; }

        public int? WashWorkerId { get; set; }

        public DateTime? WashDate { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public string ArticleName { get; set; } = string.Empty;

        public string ColorName { get; set; } = string.Empty;

        public string EquipmentName { get; set; } = string.Empty;

        public int? OrderPlannedQuantity { get; set; }

        public string Knitter1Name { get; set; } = string.Empty;

        public string Knitter2Name { get; set; } = string.Empty;

        public string Knitter3Name { get; set; } = string.Empty;

        public string VtoWorkerName { get; set; } = string.Empty;

        public string WashWorkerName { get; set; } = string.Empty;

        public bool IsCompleted
        {
            get { return WorkOrderRules.IsCompleted(PlanQuantity, FactQuantity); }
        }

        public decimal ProgressPercent
        {
            get { return WorkOrderRules.CalculateProgress(PlanQuantity, FactQuantity); }
        }

        public string ProgressText
        {
            get { return WorkOrderRules.FormatPercent(ProgressPercent); }
        }

        public int RemainingQuantity
        {
            get { return WorkOrderRules.CalculateRemaining(PlanQuantity, FactQuantity); }
        }

        public string StatusText
        {
            get { return WorkOrderRules.GetStatusText(PlanQuantity, FactQuantity); }
        }

        public string HeightDisplay
        {
            get { return string.IsNullOrWhiteSpace(HeightNew) ? Height : HeightNew; }
        }

        public string SizeDisplay
        {
            get { return string.IsNullOrWhiteSpace(SizeNew) ? Size : SizeNew; }
        }

        public string FactWeightText
        {
            get { return WorkOrderRules.FormatNumber(FactWeight); }
        }
    }

    public sealed class WorkOrderOrderSummary
    {
        public string OrderNumber { get; set; } = string.Empty;

        public int Ordered { get; set; }

        public int Issued { get; set; }

        public int Fact { get; set; }

        public int RemainingToIssue
        {
            get { return Ordered - Issued; }
        }

        public int RemainingToKnit
        {
            get { return Math.Max(0, Ordered - Fact); }
        }

        public int InShop
        {
            get { return Math.Max(0, Issued - Fact); }
        }

        public int Overproduced
        {
            get { return Math.Max(0, Fact - Issued); }
        }

        public string OverproducedText
        {
            get { return Overproduced > 0 ? "+" + Overproduced.ToString(CultureInfo.CurrentCulture) : "0"; }
        }

        public decimal ProgressPercent { get; set; }

        public string ProgressText
        {
            get { return WorkOrderRules.FormatPercent(ProgressPercent); }
        }
    }

    public sealed class WorkOrderUpdate
    {
        public int Id { get; set; }

        public int FactQuantity { get; set; }

        public decimal? FactWeight { get; set; }

        public DateTime? FactDate { get; set; }

        public string HeightNew { get; set; }

        public string SizeNew { get; set; }

        public int? Knitter1Id { get; set; }

        public decimal? Knitter1Percent { get; set; }

        public int? Knitter2Id { get; set; }

        public decimal? Knitter2Percent { get; set; }

        public int? Knitter3Id { get; set; }

        public decimal? Knitter3Percent { get; set; }

        public int? VtoWorkerId { get; set; }

        public DateTime? VtoDate { get; set; }

        public int? WashWorkerId { get; set; }

        public DateTime? WashDate { get; set; }
    }

    public sealed class WorkOrderEditor : ObservableObject
    {
        private string _heightNew = string.Empty;
        private string _sizeNew = string.Empty;
        private string _factQuantityText = "0";
        private string _factWeightText = string.Empty;
        private DateTime? _factDate;
        private WorkOrderLookup _knitter1;
        private string _knitter1PercentText = string.Empty;
        private WorkOrderLookup _knitter2;
        private string _knitter2PercentText = string.Empty;
        private WorkOrderLookup _knitter3;
        private string _knitter3PercentText = string.Empty;
        private WorkOrderLookup _vtoWorker;
        private DateTime? _vtoDate;
        private WorkOrderLookup _washWorker;
        private DateTime? _washDate;

        public int Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public string OrderNumber { get; set; } = string.Empty;

        public DateTime? OrderDate { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public string ArticleName { get; set; } = string.Empty;

        public string ColorName { get; set; } = string.Empty;

        public string EquipmentName { get; set; } = string.Empty;

        public string Machine { get; set; } = string.Empty;

        public string Height { get; set; } = string.Empty;

        public string Size { get; set; } = string.Empty;

        public int PlanQuantity { get; set; }

        public int FactQuantity { get; set; }

        public decimal? FactWeight { get; set; }

        public string StatusText { get; set; } = string.Empty;

        public decimal ProgressPercent { get; set; }

        public string ProgressText
        {
            get { return WorkOrderRules.FormatPercent(ProgressPercent); }
        }

        public int RemainingQuantity { get; set; }

        public WorkOrderOrderSummary Summary { get; set; }

        public string HeightNew
        {
            get { return _heightNew; }
            set { SetProperty(ref _heightNew, value ?? string.Empty); }
        }

        public string SizeNew
        {
            get { return _sizeNew; }
            set { SetProperty(ref _sizeNew, value ?? string.Empty); }
        }

        public string FactQuantityText
        {
            get { return _factQuantityText; }
            set { SetProperty(ref _factQuantityText, value ?? string.Empty); }
        }

        public string FactWeightText
        {
            get { return _factWeightText; }
            set { SetProperty(ref _factWeightText, value ?? string.Empty); }
        }

        public DateTime? FactDate
        {
            get { return _factDate; }
            set { SetProperty(ref _factDate, value); }
        }

        public WorkOrderLookup Knitter1
        {
            get { return _knitter1; }
            set { SetProperty(ref _knitter1, value); }
        }

        public string Knitter1PercentText
        {
            get { return _knitter1PercentText; }
            set { SetProperty(ref _knitter1PercentText, value ?? string.Empty); }
        }

        public WorkOrderLookup Knitter2
        {
            get { return _knitter2; }
            set { SetProperty(ref _knitter2, value); }
        }

        public string Knitter2PercentText
        {
            get { return _knitter2PercentText; }
            set { SetProperty(ref _knitter2PercentText, value ?? string.Empty); }
        }

        public WorkOrderLookup Knitter3
        {
            get { return _knitter3; }
            set { SetProperty(ref _knitter3, value); }
        }

        public string Knitter3PercentText
        {
            get { return _knitter3PercentText; }
            set { SetProperty(ref _knitter3PercentText, value ?? string.Empty); }
        }

        public WorkOrderLookup VtoWorker
        {
            get { return _vtoWorker; }
            set { SetProperty(ref _vtoWorker, value); }
        }

        public DateTime? VtoDate
        {
            get { return _vtoDate; }
            set { SetProperty(ref _vtoDate, value); }
        }

        public WorkOrderLookup WashWorker
        {
            get { return _washWorker; }
            set { SetProperty(ref _washWorker, value); }
        }

        public DateTime? WashDate
        {
            get { return _washDate; }
            set { SetProperty(ref _washDate, value); }
        }

        public string FactWeightDisplay
        {
            get { return WorkOrderRules.FormatNumber(FactWeight); }
        }
    }
}
