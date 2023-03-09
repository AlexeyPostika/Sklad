using POS.Report;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace POS.FlexControls.FlexProgressBar
{
    public partial class FlexProgressBar
    {
        FlexProgressBarData progressBarData;
        Thread progressBarThread;
        Delegate @delegate;

        public FlexProgressBar(FlexProgressBarData _progressBarData)
        {
            progressBarData = _progressBarData;
            @delegate = _progressBarData.Delegate;

            progressBarData.PbStatusStart = true;
            progressBarData.PbStatusBreak = false;
            progressBarData.PbStatusAbort = false;

            progressBarThread = new Thread(new ThreadStart(StartThread));
            progressBarThread.SetApartmentState(ApartmentState.STA);
            progressBarThread.IsBackground = true;
            progressBarThread.Start();

            switch (_progressBarData.PbType)
            {
                case ProgressBarType.Payment:
                    FlexPaymentUi flexPaymentUi = new FlexPaymentUi(progressBarData);
                    flexPaymentUi.ShowDialog();
                    break;
                case ProgressBarType.TaxFree:
                    FlexTaxFreeUi flexTaxFreeUi = new FlexTaxFreeUi(progressBarData);
                    flexTaxFreeUi.abortOperation += new Action(() =>
                    {
                        System.Net.HttpWebRequest httpRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create("http://localhost:8001/");
                        httpRequest.Method = System.Net.WebRequestMethods.Http.Post;
                        httpRequest.ContentType = "application/json";
                        FlexControl.FlexMessageBox.FlexMessageBox mb = new FlexControl.FlexMessageBox.FlexMessageBox();
                        if(mb.Show(Properties.Resources.TaxFreeCancelingQuestion, Properties.Resources.TaxFreeCanceling, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            String str = "";
                            using (var streamWriter = new System.IO.StreamWriter(httpRequest.GetRequestStream()))
                            {
                                streamWriter.Write("{ \"originator\": \"TaxFreeHelper\", \"success\": false, \"actionResponse\": { \"errorCode\": \"TFH-004\" } }");
                                streamWriter.Flush();
                                streamWriter.Close();

                                System.Net.HttpWebResponse httpResponse;
                                try
                                {
                                    httpResponse = (System.Net.HttpWebResponse)httpRequest.GetResponse();
                                }
                                catch
                                {
                                    POS.FlexControl.FlexMessageBox.FlexMessageBox mb1 = new POS.FlexControl.FlexMessageBox.FlexMessageBox();
                                    mb1.Show(Properties.Resources.Error, Properties.Resources.Error, MessageBoxButton.OK);
                                }
                            }
                        }
                    });
                    flexTaxFreeUi.ShowDialog();
                    break;
                case ProgressBarType.Print:
                    FlexProgressBarUi progressbar = new FlexProgressBarUi(progressBarData);
                    progressbar.ShowDialog();
                    if (_progressBarData.XpsDoc != null && progressBarData.PbStatusAbort != true)
                    {
                        try
                        {
                            FlexDocumentWindow printpreview = new FlexDocumentWindow(_progressBarData.XpsDoc);
                            printpreview.Title = Properties.Resources.DocumentPreview;
                            printpreview.Owner = MainWindow.AppWindow;
                            printpreview.Show();
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    break;
                case ProgressBarType.PrintReport:
                    FlexProgressBarUi progressbarUi = new FlexProgressBarUi(progressBarData);
                    progressbarUi.ShowDialog();
                    if (!String.IsNullOrEmpty(_progressBarData.XpsDocPath) && progressBarData.PbStatusAbort != true)
                    {
                        try
                        {
                            FlexDocumentWindow printpreview = new FlexDocumentWindow(null, progressBarData.XpsDocPath);
                            printpreview.Title = Properties.Resources.DocumentPreview;
                            printpreview.Owner = MainWindow.AppWindow;
                            printpreview.Show();
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    break;
                default:
                    FlexProgressBarUi progressbar1 = new FlexProgressBarUi(progressBarData);
                    progressbar1.Owner = MainWindow.AppWindow;
                    progressbar1.ShowDialog();
                    MainWindow.AppWindow.Focus();
                    break;
            }
        }

        void StartThread()
        {
            progressBarData.PropertyChanged += (propertyName) =>
            {
                if (propertyName == "PbStatusAbort")
                {
                    if (progressBarData.PbStatusAbort == true)
                    {
                        progressBarData.PbStatusAbort = false;
                        if (progressBarThread.IsAlive)
                        {
                            progressBarThread.Interrupt();
                        }
                    }
                }
            };

            try
            {
                @delegate.DynamicInvoke();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.InnerException.Message);
            }
            progressBarData.PbStatusStart = false;
            progressBarData.PbStatusBreak = true;
        }
    }
}