﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace unDavah_PoC
{
    /// <summary>
    /// MyTextBlock.xaml の相互作用ロジック
    /// </summary>
    public partial class UVDTextBlock : UserControl
    {
        private const String DEFAULT_FONT_COLOR_FG = "#00EE00";
        private const String DEFAULT_FONT_COLOR_BG = "#000000";

        //依存関係プロパティの登録
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                nameof(Text),
                typeof(string),
                typeof(UVDTextBlock),
                new PropertyMetadata(null, (s, e) =>
                {
                    ((UVDTextBlock)s).Parse();
                }));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public void Parse()
        {
            this.TextBlock.Inlines.Clear();
            try
            {
                XDocument doc = XDocument.Parse(this.Text);
                foreach (XNode node in doc.Root.Nodes())
                {
                    switch (node.NodeType)
                    {
                        case System.Xml.XmlNodeType.Text:
                            {
                                XText textNode = (XText)node;
                                this.TextBlock.Inlines.Add(new Run
                                {
                                    Text = textNode.Value,
                                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(DEFAULT_FONT_COLOR_FG)),
                                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(DEFAULT_FONT_COLOR_BG))
                                });
                                break;
                            }

                        case System.Xml.XmlNodeType.Element:
                            {
                                XElement elm = (XElement)node;
                                switch (elm.Name.ToString())
                                {
                                    case "style":
                                        {
                                            String fgcolor = elm.Attribute("fgcolor")?.Value ?? DEFAULT_FONT_COLOR_FG;
                                            String bgcolor = elm.Attribute("bgcolor")?.Value ?? DEFAULT_FONT_COLOR_BG;
                                            String text = elm.Value;

                                            this.TextBlock.Inlines.Add(new Run
                                            {
                                                Text = text,
                                                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(fgcolor)),
                                                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(bgcolor))
                                            });
                                            break;
                                        }

                                    case "br":
                                        this.TextBlock.Inlines.Add(new Run
                                        {
                                            Text = Environment.NewLine
                                        });
                                        break;
                                    default:
                                        continue;
                                }

                                break;
                            }
                    }
                }
            }
            catch { }
        }
        public UVDTextBlock()
        {
            InitializeComponent();
        }
    }
}