using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.ComponentModel;
using System.Drawing;
using System.Timers;
using System.Windows;

namespace WpfDemoNew.Control
{
    /// <summary>
    /// Label走马灯自定义控件
    /// </summary>
    [ToolboxBitmap(typeof(Label))] //设置工具箱中显示的图标
    public class ScrollingTextControl : Label
    {
        /// <summary>
        /// 定时器
        /// </summary>
        Timer MarqueeTimer = new Timer();
        /// <summary>
        /// 滚动文字源
        /// </summary>
        String _TextSource = "滚动文字源";
        /// <summary>
        /// 输出文本
        /// </summary>
        String _OutText = string.Empty;
        /// <summary>
        /// 过度文本存储
        /// </summary>
        string _TempString = string.Empty;
        /// <summary>
        /// 文字的滚动速度
        /// </summary>
        double _RunSpeed = 1000;

        DateTime _SignTime;
        bool _IfFirst = true;

        /// <summary>
        /// 滚动一循环字幕停留的秒数,单位为毫秒,默认值停留3秒
        /// </summary>
        int _StopSecond = 3000;

        /// <summary>
        /// 滚动一循环字幕停留的秒数,单位为毫秒,默认值停留3秒
        /// </summary>
        public int StopSecond
        {
            get { return _StopSecond; }
            set
            {
                _StopSecond = value;
            }
        }

        /// <summary>
        /// 滚动的速度
        /// </summary>
        [Description("文字滚动的速度")]　//显示在属性设计视图中的描述
        public double RunSpeed
        {
            get { return _RunSpeed; }
            set
            {
                _RunSpeed = value;
                MarqueeTimer.Interval = _RunSpeed;
            }
        }

        /// <summary>
        /// 滚动文字源
        /// </summary>
        [Description("文字滚动的Text")]
        public string TextSource
        {
            get { return _TextSource; }
            set
            {
                _TextSource = value;
                _TempString = _TextSource + "   ";
                _OutText = _TempString;
            }
        }

        private string SetContent
        {
            get { return Content.ToString(); }
            set
            {
                Content = value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ScrollingTextControl()
        {
            MarqueeTimer.Interval = _RunSpeed;//文字移动的速度
            MarqueeTimer.Enabled = true;      //开启定时触发事件
            MarqueeTimer.Elapsed += new ElapsedEventHandler(MarqueeTimer_Elapsed);//绑定定时事件
            this.Loaded += new RoutedEventHandler(ScrollingTextControl_Loaded);//绑定控件Loaded事件
        }


        void ScrollingTextControl_Loaded(object sender, RoutedEventArgs e)
        {
            _TextSource = SetContent;
            _TempString = _TextSource + "   ";
            _OutText = _TempString;
            _SignTime = DateTime.Now;
        }


        void MarqueeTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (string.IsNullOrEmpty(_OutText)) return;

            if (_OutText.Substring(1) + _OutText[0] == _TempString)
            {
                if (_IfFirst)
                {
                    _SignTime = DateTime.Now;
                }

                if ((DateTime.Now - _SignTime).TotalMilliseconds > _StopSecond)
                {
                    _IfFirst = true; ;
                }
                else
                {
                    _IfFirst = false;
                    return;
                }
            }

            _OutText = _OutText.Substring(1) + _OutText[0];


            Dispatcher.BeginInvoke(new Action(() =>
            {
                SetContent = _OutText;
            }));


        }

    }
}