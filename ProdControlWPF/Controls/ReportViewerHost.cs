using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Microsoft.Reporting.WinForms;
using ProdControlWPF.Models;

namespace ProdControlWPF.Controls
{
    public sealed class ReportViewerHost : WindowsFormsHost
    {
        private const string ReportResourceName = "ProdControlWPF.Reports.ReportEmployee.rdlc";
        private readonly ReportViewer _viewer;

        public ReportViewerHost()
        {
            _viewer = new ReportViewer
            {
                Dock = DockStyle.Fill,
                ProcessingMode = ProcessingMode.Local
            };

            _viewer.LocalReport.ReportEmbeddedResource = ReportResourceName;
            Child = _viewer;
        }

        public void LoadReport(IReadOnlyList<EmployeeReportRow> rows)
        {
            if (rows == null)
            {
                throw new ArgumentNullException(nameof(rows));
            }

            _viewer.LocalReport.DataSources.Clear();
            _viewer.LocalReport.DataSources.Add(new ReportDataSource("ReportEmployee", rows));
            _viewer.RefreshReport();
        }

        public void DisposeReport()
        {
            _viewer.LocalReport.DataSources.Clear();
            _viewer.Dispose();
        }
    }
}
