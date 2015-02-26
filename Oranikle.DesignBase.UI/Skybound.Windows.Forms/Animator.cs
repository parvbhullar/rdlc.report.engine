using System;
using System.ComponentModel;
using System.Threading;
using System.Timers;

namespace Skybound.Windows.Forms
{

    internal class Animator : System.IDisposable
    {

        private int _Duration;
        private Skybound.Windows.Forms.Motion _Motion;
        private System.ComponentModel.ISynchronizeInvoke _SynchronizingObject;
        private double _Value;
        private System.Timers.Timer FrameTimer;
        private double LastFrameMs;

        public event System.EventHandler NextFrame;

        public int Duration
        {
            get
            {
                return _Duration;
            }
        }

        public bool IsComplete
        {
            get
            {
                return FrameTimer == null;
            }
        }

        public Skybound.Windows.Forms.Motion Motion
        {
            get
            {
                return _Motion;
            }
        }

        public System.ComponentModel.ISynchronizeInvoke SynchronizingObject
        {
            get
            {
                return _SynchronizingObject;
            }
            set
            {
                _SynchronizingObject = value;
                if (FrameTimer != null)
                    FrameTimer.SynchronizingObject = value;
            }
        }

        public double Value
        {
            get
            {
                return _Value;
            }
        }

        public Animator(Skybound.Windows.Forms.Motion motion, int duration)
        {
            _Motion = motion;
            _Duration = duration;
        }

        public void Dispose()
        {
            if (FrameTimer != null)
            {
                FrameTimer.Dispose();
                FrameTimer = null;
            }
        }

        private void OnFrameTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (System.Threading.Monitor.TryEnter(this))
            {
                try
                {
                    if (FrameTimer == null)
                        return;
                    System.DateTime dateTime1 = System.DateTime.Now;
                    System.TimeSpan timeSpan1 = dateTime1.TimeOfDay;
                    double d1 = timeSpan1.TotalMilliseconds - LastFrameMs;
                    double d2 = d1 / (double)Duration;
                    if (Motion != Skybound.Windows.Forms.Motion.Constant)
                    {
                        double d3 = 0.0;
                        if (Motion == Skybound.Windows.Forms.Motion.InOut)
                            d3 = (Value - 0.5) * 1.8;
                        else if (Motion == Skybound.Windows.Forms.Motion.Decelerate)
                            d3 = Value * 0.8;
                        else if (Motion == Skybound.Windows.Forms.Motion.Accelerate)
                            d3 = -0.8 + (Value * 0.8);
                        double d4 = System.Math.Cos(d3 * 3.14159265358979);
                        d2 += d2 * d4;
                    }
                    if (d2 < 0.01)
                        d2 = 0.01;
                    _Value += d2;
                    if (Value >= 1.0)
                    {
                        Stop();
                    }
                    else
                    {
                        System.DateTime dateTime2 = System.DateTime.Now;
                        System.TimeSpan timeSpan2 = dateTime2.TimeOfDay;
                        LastFrameMs = timeSpan2.TotalMilliseconds;
                    }
                    OnNextFrame(e);
                }
                finally
                {
                    System.Threading.Monitor.Exit(this);
                }
            }
        }

        public void Reset()
        {
            _Value = 1.0 - Value;
        }

        public void Start()
        {
            if (FrameTimer != null)
                throw new System.InvalidOperationException("INTERNAL ERROR: The animation timer is already running.");
            _Value = 0.08;
            FrameTimer = new System.Timers.Timer();
            FrameTimer.SynchronizingObject = SynchronizingObject;
            FrameTimer.Interval = 1.0;
            FrameTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnFrameTimerElapsed);
            FrameTimer.Start();
            System.DateTime dateTime = System.DateTime.Now;
            System.TimeSpan timeSpan = dateTime.TimeOfDay;
            LastFrameMs = timeSpan.TotalMilliseconds;
            OnFrameTimerElapsed(FrameTimer, null);
        }

        public void Stop()
        {
            _Value = 1.0;
            if (FrameTimer != null)
            {
                FrameTimer.Stop();
                FrameTimer.Dispose();
                FrameTimer = null;
            }
        }

        protected virtual void OnNextFrame(System.EventArgs e)
        {
            if (NextFrame != null)
                NextFrame(this, e);
        }

    } // class Animator

}

