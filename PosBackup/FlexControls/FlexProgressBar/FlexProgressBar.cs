using System;
using System.Threading;
using System.Windows;

namespace PosBackup.FlexControls.FlexProgressBar
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

            FlexProgressBarUi progressbar1 = new FlexProgressBarUi(progressBarData);
            progressbar1.ShowDialog();
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