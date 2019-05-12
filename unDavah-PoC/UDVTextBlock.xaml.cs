using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace com.undavah.unDavah_PoC
{
    /// <summary>
    /// MyTextBlock.xaml の相互作用ロジック
    /// </summary>
    public partial class UDVTextBlock : UserControl
    {
        private const String DEFAULT_FONT_COLOR_FG = "#ffffff";
        private const String DEFAULT_FONT_COLOR_BG = "#cc6c00";
            
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                nameof(Text),
                typeof(string),
                typeof(UDVTextBlock),
                new PropertyMetadata(null, (s, e) =>
                {
                    ((UDVTextBlock)s).Parse();
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
                                Text = restoreEscape(textNode.Value),
                                Foreground = new SolidColorBrush(
                                    (Color)ColorConverter.ConvertFromString(DEFAULT_FONT_COLOR_FG)),
                                Background = new SolidColorBrush(
                                    (Color)ColorConverter.ConvertFromString(DEFAULT_FONT_COLOR_BG))
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
                                        Text = restoreEscape(text),
                                        Foreground = new SolidColorBrush(
                                            (Color)ColorConverter.ConvertFromString(fgcolor)),
                                        Background = new SolidColorBrush(
                                            (Color)ColorConverter.ConvertFromString(bgcolor))
                                    });
                                    break;
                                }

                                case "br":
                                {
                                    this.TextBlock.Inlines.Add(new Run
                                    {
                                        Text = Environment.NewLine
                                    });
                                    break;
                                }
                                default:
                                {
                                    continue;
                                }
                            }
                            break;
                        }
                    }
                }
            }
            catch { }
        }
        private String restoreEscape(String aStr)
        {
            Regex re = new Regex("&lt;");
            aStr = re.Replace(aStr, "<");
            re = new Regex("&gt;");
            aStr = re.Replace(aStr, ">");
            re = new Regex("&amp;");
            aStr = re.Replace(aStr, "&");

            return aStr;
        }
        public UDVTextBlock()
        {
            InitializeComponent();
        }
    }
}
