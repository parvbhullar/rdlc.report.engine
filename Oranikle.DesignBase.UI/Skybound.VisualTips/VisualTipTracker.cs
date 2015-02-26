using System;
using System.Collections;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;

namespace Skybound.VisualTips
{

    internal class VisualTipTracker : System.Windows.Forms.IMessageFilter
    {

        private static System.Windows.Forms.Control _LastMouseEventControl;
        private static System.IntPtr _LastMouseEventHwnd;
        private static Skybound.VisualTips.VisualTipProvider _TrackingProvider;
        private static System.Collections.Hashtable ElapsedHandlers;
        private static Skybound.VisualTips.VisualTipTracker Instance;
        private static System.Timers.Timer TimeoutHandler;
        private static System.EventHandler TimeoutHandlerCallback;
        private static System.Collections.Hashtable Timers;
        private static System.Windows.Forms.Control TimerSynchronizingObject;

        public static System.Windows.Forms.Control LastMouseEventControl
        {
            get
            {
                if (Skybound.VisualTips.VisualTipTracker._LastMouseEventControl != null)
                    return Skybound.VisualTips.VisualTipTracker._LastMouseEventControl;
                Skybound.VisualTips.VisualTipTracker._LastMouseEventControl = System.Windows.Forms.Control.FromHandle(Skybound.VisualTips.VisualTipTracker._LastMouseEventHwnd);
                return System.Windows.Forms.Control.FromHandle(Skybound.VisualTips.VisualTipTracker._LastMouseEventHwnd);
            }
        }

        public static Skybound.VisualTips.VisualTipProvider TrackingProvider
        {
            get
            {
                return Skybound.VisualTips.VisualTipTracker._TrackingProvider;
            }
            set
            {
                Skybound.VisualTips.VisualTipTracker._TrackingProvider = value;
            }
        }

        private VisualTipTracker()
        {
        }

        static VisualTipTracker()
        {
            Skybound.VisualTips.VisualTipTracker.ElapsedHandlers = new System.Collections.Hashtable();
            Skybound.VisualTips.VisualTipTracker.Timers = new System.Collections.Hashtable();
        }

        bool System.Windows.Forms.IMessageFilter.PreFilterMessage(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 512)
            {
                Skybound.VisualTips.VisualTipTracker.ResetTimers();
                Skybound.VisualTips.VisualTipTracker._LastMouseEventHwnd = m.HWnd;
                Skybound.VisualTips.VisualTipTracker._LastMouseEventControl = null;
            }
            else if (((m.Msg != 675) && (m.Msg != 513)) || Skybound.VisualTips.VisualTipTracker._LastMouseEventHwnd == m.HWnd)
            {
                Skybound.VisualTips.VisualTipTracker._LastMouseEventHwnd = System.IntPtr.Zero;
                Skybound.VisualTips.VisualTipTracker._LastMouseEventControl = null;
            }
            if (Skybound.VisualTips.VisualTipTracker.TrackingProvider == null)
                goto label_1;
            switch (m.Msg)
            {
                case 512:
                    System.IntPtr intPtr1 = m.LParam;
                    System.Drawing.Point point = new System.Drawing.Point(intPtr1.ToInt32());
                    Skybound.VisualTips.VisualTipTracker.TrackingProvider.TrackMouseMove(new System.Windows.Forms.MouseEventArgs(System.Windows.Forms.Control.MouseButtons, 0, point.X, point.Y, 0));
                    break;

                case 513:
                case 514:
                    Skybound.VisualTips.VisualTipTracker.TrackingProvider.TrackMouseDownUp();
                    break;

                case 675:
                    Skybound.VisualTips.VisualTipTracker.TrackingProvider.TrackMouseLeave(System.EventArgs.Empty);
                    break;

                case 256:
                    System.IntPtr intPtr2 = m.WParam;
                    System.Windows.Forms.KeyEventArgs keyEventArgs = new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys)intPtr2.ToInt32());
                    Skybound.VisualTips.VisualTipTracker.TrackingProvider.TrackKeyDown(keyEventArgs);
                    return keyEventArgs.Handled;
            
                    break;
            }
        label_1:
            if (m.Msg == 512)
            {
                Skybound.VisualTips.VisualTipTracker.ResetTimers();
            }
            else if ((m.Msg == 256) && (Skybound.VisualTips.VisualTipProvider.WindowStack.Count > 0))
            {
                System.IntPtr intPtr3 = m.WParam;
                return Skybound.VisualTips.VisualTipProvider.WindowStack.ProcessKeyDown((System.Windows.Forms.Keys)intPtr3.ToInt32());
            }
            return false;
        }

        public static void AddHandler(int duration, System.EventHandler elapsed)
        {
            if (Skybound.VisualTips.VisualTipTracker.TimerSynchronizingObject == null)
            {
                Skybound.VisualTips.VisualTipTracker.TimerSynchronizingObject = new System.Windows.Forms.Control();
                Skybound.VisualTips.VisualTipTracker.TimerSynchronizingObject.CreateControl();
            }
            System.EventHandler eventHandler = (System.EventHandler)Skybound.VisualTips.VisualTipTracker.ElapsedHandlers[duration];
            if (eventHandler == null)
            {
                System.Timers.Timer timer = new System.Timers.Timer();
                timer.SynchronizingObject = Skybound.VisualTips.VisualTipTracker.TimerSynchronizingObject;
                timer.Interval = (double)duration;
                timer.Elapsed += new System.Timers.ElapsedEventHandler(Skybound.VisualTips.VisualTipTracker.timer_Elapsed);
                timer.Start();
                Skybound.VisualTips.VisualTipTracker.Timers[duration] = timer;
                Skybound.VisualTips.VisualTipTracker.ElapsedHandlers[duration] = elapsed;
                return;
            }
            Skybound.VisualTips.VisualTipTracker.ElapsedHandlers[duration] = (System.EventHandler)System.Delegate.Combine(eventHandler, elapsed);
        }

        public static void Disable()
        {
            if (Skybound.VisualTips.VisualTipTracker.Instance != null)
            {
                System.Windows.Forms.Application.RemoveMessageFilter(Skybound.VisualTips.VisualTipTracker.Instance);
                Skybound.VisualTips.VisualTipTracker.Instance = null;
            }
        }

        public static void Enable()
        {
            if (Skybound.VisualTips.VisualTipTracker.Instance == null)
            {
                Skybound.VisualTips.VisualTipTracker.Instance = new Skybound.VisualTips.VisualTipTracker();
                System.Windows.Forms.Application.AddMessageFilter(new Skybound.VisualTips.VisualTipTracker());
            }
        }

        public static void RemoveHandler(int duration, System.EventHandler elapsed)
        {
            System.EventHandler eventHandler = (System.EventHandler)Skybound.VisualTips.VisualTipTracker.ElapsedHandlers[duration];
            if (eventHandler != null)
                Skybound.VisualTips.VisualTipTracker.ElapsedHandlers[duration] = (System.EventHandler)System.Delegate.Remove(eventHandler, elapsed);
            if (Skybound.VisualTips.VisualTipTracker.ElapsedHandlers[duration] == null)
            {
                System.Timers.Timer timer = Skybound.VisualTips.VisualTipTracker.Timers[duration] as System.Timers.Timer;
                if (timer != null)
                {
                    Skybound.VisualTips.VisualTipTracker.Timers[duration] = null;
                    timer.Dispose();
                }
            }
        }

        private static void ResetTimers()
        {
            if ((Skybound.VisualTips.VisualTipTracker.Timers != null) && (Skybound.VisualTips.VisualTipTracker.Timers.Values != null))
            {
                foreach (System.Timers.Timer timer in Skybound.VisualTips.VisualTipTracker.Timers.Values)
                {
                    if (timer != null)
                    {
                        timer.Stop();
                        timer.Start();
                    }
                }
            }
        }

        public static void SetTimeoutHandler(int duration, System.EventHandler callback)
        {
            lock (typeof(Skybound.VisualTips.VisualTipTracker))
            {
                Skybound.VisualTips.VisualTipTracker.TimeoutHandlerCallback = callback;
                if (Skybound.VisualTips.VisualTipTracker.TimeoutHandler == null)
                {
                    Skybound.VisualTips.VisualTipTracker.TimeoutHandler = new System.Timers.Timer();
                    Skybound.VisualTips.VisualTipTracker.TimeoutHandler.SynchronizingObject = Skybound.VisualTips.VisualTipTracker.TimerSynchronizingObject;
                }
                else
                {
                    Skybound.VisualTips.VisualTipTracker.TimeoutHandler.Stop();
                }
                Skybound.VisualTips.VisualTipTracker.TimeoutHandler.Interval = (double)duration;
                Skybound.VisualTips.VisualTipTracker.TimeoutHandler.Elapsed += new System.Timers.ElapsedEventHandler(Skybound.VisualTips.VisualTipTracker.TimeoutHandler_Elapsed);
                Skybound.VisualTips.VisualTipTracker.TimeoutHandler.Start();
            }
        }

        private static void TimeoutHandler_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Skybound.VisualTips.VisualTipTracker.TimeoutHandler.Enabled)
            {
                Skybound.VisualTips.VisualTipTracker.TimeoutHandler.Stop();
                Skybound.VisualTips.VisualTipTracker.TimeoutHandler.Elapsed -= new System.Timers.ElapsedEventHandler(Skybound.VisualTips.VisualTipTracker.TimeoutHandler_Elapsed);
                if (Skybound.VisualTips.VisualTipTracker.TimeoutHandlerCallback != null)
                    Skybound.VisualTips.VisualTipTracker.TimeoutHandlerCallback(null, e);
            }
        }

        private static void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            System.EventHandler eventHandler = (System.EventHandler)Skybound.VisualTips.VisualTipTracker.ElapsedHandlers[(int)(sender as System.Timers.Timer).Interval];
            if (eventHandler != null)
                eventHandler(null, e);
        }

    } // class VisualTipTracker

}

