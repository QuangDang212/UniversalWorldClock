﻿// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

using System;
using UniversalWorldClock.Converters;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace UniversalWorldClock.Views
{
    public sealed partial class Clock
    {
        public Clock()
        {
            this.InitializeComponent();
            Init();
        }

        private void Init()
        {
            InitMarkers();
            InitDigits();
        }

        private void InitMarkers()
        {
            var converter = new ObjectSizeConverter();
            var offset = converter.Convert(125);
            for (int i = 0; i < 60; ++i)
            {
                Rectangle marker = new Rectangle();

                if ((i % 5) == 0)
                {
                    marker.Width = converter.Convert(1.5);
                    marker.Height = converter.Convert(8);
                    marker.Fill = new SolidColorBrush(Color.FromArgb(0xe0, 0xff, 0xff, 0xff));
                    marker.Stroke = new SolidColorBrush(Color.FromArgb(0x80, 0x33, 0x33, 0x33));
                    marker.StrokeThickness = 0.5;
                }
                else
                {
                    marker.Width = converter.Convert(0.5);
                    marker.Height = converter.Convert(3);
                    marker.Fill = new SolidColorBrush(Color.FromArgb(0x80, 0xff, 0xff, 0xff));
                    marker.Stroke = null;
                    marker.StrokeThickness = 0;
                }

                TransformGroup transforms = new TransformGroup();

                transforms.Children.Add(new TranslateTransform {X = -(marker.Width/2), Y = marker.Width/2 - converter.Convert(100) - marker.Height});
                transforms.Children.Add(new RotateTransform {Angle = i*6});
                transforms.Children.Add(new TranslateTransform {X = offset, Y = offset});

                marker.RenderTransform = transforms;

                _markersCanvas.Children.Add(marker);
            }
        }

        private void InitDigits()
        {
            var converter = new ObjectSizeConverter();
            var offset = converter.Convert(125);
            for (int i = 1; i <= 12; ++i)
            {
                TextBlock tb = new TextBlock();

                tb.Text = i.ToString();
                tb.TextAlignment = TextAlignment.Center;
                tb.RenderTransformOrigin = new Point(1, 1);
                tb.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                tb.FontSize = converter.Convert(8);

                tb.RenderTransform = new ScaleTransform {ScaleX = 2, ScaleY = 2};

                double r = converter.Convert(92);
                double angle = Math.PI*i*30.0/180.0;
                double x = Math.Sin(angle)*r + offset, y = -Math.Cos(angle)*r + offset;

                Canvas.SetLeft(tb, x);
                Canvas.SetTop(tb, y);

                _markersCanvas.Children.Add(tb);
            }
        }
    }
}