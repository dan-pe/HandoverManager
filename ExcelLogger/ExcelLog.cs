#region Usings

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;

using HandoverAlgorithmBase.NovelAlgorithm;
using Profiler;

#endregion

namespace ExcelLogger
{
    public class ExcelLog
    {
        #region Private Fields
        
        private readonly Application _excelApp;

        private readonly Workbook workbook;

        #endregion

        #region Constructors
        
        public ExcelLog(string fileName, List<NovelNetworkModel> novelNetworks, UserProfile profileName)
        {
            this._excelApp = new Application();
            this.workbook = this._excelApp.Workbooks.Add();
            object nullValue = System.Reflection.Missing.Value;


            this.GenerateContent(novelNetworks, profileName);

            this.workbook.SaveAs(fileName);
            this.workbook.Close(true, nullValue, nullValue);
            this._excelApp.Quit();
            Marshal.ReleaseComObject(this.workbook);
            Marshal.ReleaseComObject(this._excelApp);
        }

        #endregion

        private void GenerateContent(List<NovelNetworkModel> novelNetworks, UserProfile profileName)
        {
            var workSheet = (Worksheet)this._excelApp.Worksheets.Item[1];
            // Title
            workSheet.Cells[3,3] = "Evaluated Networks";

            const int anchorRowId = 5;
            const int anchorColumnId = 2;

            // ColumnsNames
            workSheet.Cells[anchorRowId, anchorColumnId] = "Network Name";
            workSheet.Cells[anchorRowId, anchorColumnId + 1] = "Network Type";
            workSheet.Cells[anchorRowId, anchorColumnId + 2] = "Throughoutput";
            workSheet.Cells[anchorRowId, anchorColumnId + 3] = "Packet Loss";
            workSheet.Cells[anchorRowId, anchorColumnId + 4] = "Response";
            workSheet.Cells[anchorRowId, anchorColumnId + 5] = "Security";

            int i = anchorRowId + 1;
            int j = anchorColumnId;

            foreach (var networkModel in novelNetworks)
            {
                workSheet.Cells[i, j] = networkModel.RadioNetworkModel.NetworkName;
                workSheet.Cells[i, j + 1] = networkModel.RadioNetworkModel.NetworkType;
                workSheet.Cells[i, j + 2] = networkModel.RadioNetworkModel.Parameters.ThroughputInMbps;
                workSheet.Cells[i, j + 3] = networkModel.RadioNetworkModel.Parameters.PacketLossPercentage;
                workSheet.Cells[i, j + 4] = networkModel.RadioNetworkModel.Parameters.ResponseTimeInMsec;
                workSheet.Cells[i, j + 5] = networkModel.RadioNetworkModel.Parameters.SecurityLevel;
                i++;
            }

            // Evaluation result
            int moveOffset = anchorRowId + novelNetworks.Count + 5;
            workSheet.Cells[moveOffset, anchorColumnId] = "Evaluation result";
            workSheet.Cells[moveOffset+1, anchorColumnId] = "Network Name";
            workSheet.Cells[moveOffset+1, anchorColumnId + 1] = "GRC Factor";
            i = moveOffset + 2;
            foreach (var novelNetwork in novelNetworks)
            {
                workSheet.Cells[i, anchorColumnId] = novelNetwork.RadioNetworkModel.NetworkName;
                workSheet.Cells[i, anchorColumnId + 1] = novelNetwork.GrcFactor;
                i++;
            }

            // Generate Chart

            var charts = (ChartObjects) workSheet.ChartObjects(Type.Missing);
            var chartObject = charts.Add(200, 200, 200, 200);
            var chart = chartObject.Chart;


            var chartRange = workSheet.Range[
                workSheet.Cells[moveOffset ,anchorColumnId],
                workSheet.Cells[i-1, anchorColumnId + 1]];
            chart.SetSourceData(chartRange, System.Reflection.Missing.Value);
            chart.ChartType = XlChartType.xlColumnClustered;
            chart.ChartTitle.Caption = "Networks GRC Factor";
            }

       
    }
}
