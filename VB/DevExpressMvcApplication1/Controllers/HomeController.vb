﻿Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraScheduler
Imports DevExpressMvcApplication1.Reports

Namespace DevExpressMvcApplication1.Views
    Public Class HomeController
        Inherits Controller

        #Region "Scheduler"
        Public Function Index() As ActionResult
            Return View(SchedulerDataHelper.DataObject)
        End Function

        Public Function SchedulerPartial() As ActionResult
            Return PartialView("SchedulerPartial", SchedulerDataHelper.DataObject)
        End Function
        #End Region


        #Region "Report"
        Public Function ReportView() As ActionResult
            Return View("ReportView")
        End Function

        Public Function ReportViewerPartial(ByVal interval As TimeInterval) As ActionResult
            ViewData("Report") = CreateReport()
            Return PartialView("ReportViewerPartial")
        End Function

        Public Function ExportReportViewerPartial() As ActionResult
            Return DevExpress.Web.Mvc.ReportViewerExtension.ExportTo(CreateReport())
        End Function

        Private ReadOnly Property Start() As Date
            Get
                Return ParseDate(Request.Params("StartDate"))
            End Get
        End Property
        Private ReadOnly Property [End]() As Date
            Get
                Return ParseDate(Request.Params("EndDate"))
            End Get
        End Property

        Private Function ParseDate(ByVal utcDateString As String) As Date
            Dim utcDate As Date
            utcDate = ((New Date(1970, 1, 1)).AddMilliseconds(Convert.ToDouble(utcDateString)))
            Return TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(utcDate, DateTimeKind.Utc), TimeZoneInfo.Local)
        End Function

        Private Function CreateReport() As XtraReport
            Dim report As New SchedulerDayReport()
            report.SchedulerAdapter = SchedulerDataHelper.CreateSchedulerStoragePrintAdapter(New TimeInterval(Start, [End]))
            Return TryCast(report, XtraReport)
        End Function
        #End Region


        #Region "Popup"
        Public Function ReportPopupPartial() As ActionResult
            Return PartialView("ReportPopupPartial")
        End Function
        #End Region
    End Class
End Namespace
