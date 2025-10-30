using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace VokabelTrainer
{
    //*******************************************************************************************************
    /// <summary>
    /// This form displays a graph of learning historyy
    /// </summary>
    //*******************************************************************************************************
    public partial class GraphsForm : Form
    {
        //===================================================================================================
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        //===================================================================================================
        /// <summary>
        /// Font for displaying dates
        /// </summary>
        private Font m_oDateFont;
        /// <summary>
        /// Text brush
        /// </summary>
        private Brush m_oTextBrush;
        /// <summary>
        /// The pen for drawing axes
        /// </summary>
        private Pen m_oAxisPen;
        /// <summary>
        /// The pen for the total exercises 
        /// </summary>
        private Pen m_oTotalGraphPen;
        /// <summary>
        /// The pen for number of words
        /// </summary>
        private Pen m_oWordsGraphPen;
        /// <summary>
        /// The pen for learned words
        /// </summary>
        private Pen m_oLearnedWordsGraphPen;

        //===================================================================================================
        /// <summary>
        /// The data for total exercises
        /// </summary>
        private SortedDictionary<DateTime, int> m_oTotalGraphData;
        /// <summary>
        /// The data for the number or words
        /// </summary>
        private SortedDictionary<DateTime, int> m_oWordsGraphData;
        /// <summary>
        /// The data for learned words
        /// </summary>
        private SortedDictionary<DateTime, int> m_oLearnedWordsGraphData;

        //===================================================================================================
        /// <summary>
        /// The label for total exercises
        /// </summary>
        private string m_strTotalWordsLabel;
        /// <summary>
        /// The label for the number of words
        /// </summary>
        private string m_strWordsLabel;
        /// <summary>
        /// The label for learned words
        /// </summary>
        private string m_strLearnedWordsLabel;

        //===================================================================================================
        /// <summary>
        /// Minimum date for scaling graphs
        /// </summary>
        private DateTime m_dtmMinDate;
        /// <summary>
        /// Maximum date for scaling graphs
        /// </summary>
        private DateTime m_dtmMaxDate;
        /// <summary>
        /// Maximu value for scaling graphs
        /// </summary>
        private int m_nMaxValue;

        //===================================================================================================
        /// <summary>
        /// Constructs a new GraphForm object
        /// </summary>
        /// <param name="oWordsIncludingRepeats">The graph for total exercises</param>
        /// <param name="oWordsInBookOverTime">The graph for number of words</param>
        /// <param name="oWordsMinusErrors">The graph for learned words</param>
        //===================================================================================================
        public GraphsForm(
            SortedDictionary<DateTime, int> oWordsIncludingRepeats,
            SortedDictionary<DateTime, int> oWordsInBookOverTime,
            SortedDictionary<DateTime, int> oWordsMinusErrors)
        {
            InitializeComponent();
            InitializeResources();

            // save graph data
            SortedDictionary<DateTime, int> oWordsIncludingRepeats2 =
                new SortedDictionary<DateTime, int>();

            int nDownscaling = oWordsInBookOverTime.Values.Last<int>() == 0 ? 1 : 
                oWordsIncludingRepeats.Values.Last<int>() /
                oWordsInBookOverTime.Values.Last<int>();
            if (nDownscaling < 1)
                nDownscaling = 1;

            foreach (KeyValuePair<DateTime, int> oKvp in oWordsIncludingRepeats)
            {
                oWordsIncludingRepeats2[oKvp.Key] = oKvp.Value / nDownscaling; 
            }
            m_oTotalGraphData = oWordsIncludingRepeats2;
            m_oWordsGraphData = oWordsInBookOverTime;
            m_oLearnedWordsGraphData = oWordsMinusErrors;

            // save localized labels in strings 
            m_strTotalWordsLabel = m_lblTotal.Text.Trim() + 
                " (" + oWordsIncludingRepeats.Values.Last<int>().ToString() + ")";
            m_strWordsLabel = m_lblWords.Text.Trim() +
                 " (" + oWordsInBookOverTime.Values.Last<int>().ToString() + ")";
            m_strLearnedWordsLabel = m_lblWordsLearned.Text.Trim() +
                 " (" + oWordsMinusErrors.Values.Last<int>().ToString() + ")";

            // Determine min and max dates
            m_dtmMinDate = DateTime.MaxValue;
            m_dtmMaxDate = DateTime.MinValue;

            foreach (var dtmDate in oWordsIncludingRepeats.Keys)
            {
                if (dtmDate < m_dtmMinDate) m_dtmMinDate = dtmDate;
                if (dtmDate > m_dtmMaxDate) m_dtmMaxDate = dtmDate;
            }

            // Calculate the maximum value across all graphs
            m_nMaxValue = Math.Max(
                oWordsIncludingRepeats.Values.Max(),
                Math.Max(
                    oWordsInBookOverTime.Values.Max(),
                    oWordsMinusErrors.Values.Max()
                )
            );

            // Redraw on resize
            this.Resize += (s, e) => this.Invalidate(); 
        }

        //===================================================================================================
        /// <summary>
        /// Initializes reusable resources
        /// </summary>
        //===================================================================================================
        private void InitializeResources()
        {
            m_oDateFont = new Font("Arial", 8);
            m_oTextBrush = Brushes.Black;

            m_oAxisPen = new Pen(Color.Black, 2);
            m_oTotalGraphPen = new Pen(Color.Blue, 2);
            m_oWordsGraphPen = new Pen(Color.Green, 2);
            m_oLearnedWordsGraphPen = new Pen(Color.Red, 2);
        }


        //===================================================================================================
        /// <summary>
        /// Disposes the object
        /// </summary>
        /// <param name="disposing"></param>
        //===================================================================================================
        protected override void Dispose(
            bool bDisposing
            )
        {
            if (bDisposing && (components != null))
            {
                components.Dispose();
            }

            if (bDisposing)
            {
                // Dispose of all resources
                if (m_oDateFont != null)
                    m_oDateFont.Dispose();
                if (m_oAxisPen != null)
                    m_oAxisPen.Dispose();
                if (m_oTotalGraphPen != null)
                    m_oTotalGraphPen.Dispose();
                if (m_oWordsGraphPen != null)
                    m_oWordsGraphPen.Dispose();
                if (m_oLearnedWordsGraphPen != null)
                    m_oLearnedWordsGraphPen.Dispose();
            }

            base.Dispose(bDisposing);
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when the form is painted
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void OnPaintForm(
            object oSender, 
            PaintEventArgs oArgs
            )
        {
            Graphics oGraphics = oArgs.Graphics;

            // Calculate margins and drawable area
            SizeF oMaxDateSize = oGraphics.MeasureString(m_dtmMaxDate.ToShortDateString(), m_oDateFont);
            int nBottomMargin = (int)oMaxDateSize.Height + 10;
            int nRightMargin =
                Math.Max(
                    (int)oGraphics.MeasureString(m_strTotalWordsLabel, m_lblTotal.Font).Width,
                    Math.Max(
                    (int)oGraphics.MeasureString(m_strWordsLabel, m_lblTotal.Font).Width,
                    (int)oGraphics.MeasureString(m_strLearnedWordsLabel, m_lblTotal.Font).Width)
                    ) + 10;

            MinimumSize = new Size(nRightMargin + 100, this.MinimumSize.Height);

            Rectangle oClientArea = this.ClientRectangle;
            Rectangle oDrawingArea = new Rectangle(
                oClientArea.Left + 50,
                oClientArea.Top + 20,
                oClientArea.Width - 50 - nRightMargin,
                oClientArea.Height - 20 - nBottomMargin
            );

            // Draw axes
            oGraphics.DrawLine(m_oAxisPen, oDrawingArea.Left, oDrawingArea.Bottom,
                oDrawingArea.Right, oDrawingArea.Bottom); // X-axis
            oGraphics.DrawLine(m_oAxisPen, oDrawingArea.Left, oDrawingArea.Top, 
                oDrawingArea.Left, oDrawingArea.Bottom);   // Y-axis

            // Draw date labels
            DrawDates(oGraphics, oDrawingArea);

            // Draw curves
            float fYPositionMainLabel = 0;
            DrawCurve(oGraphics, oDrawingArea, m_oWordsGraphData,
                m_oWordsGraphPen, m_strWordsLabel, ref fYPositionMainLabel, true);
            DrawCurve(oGraphics, oDrawingArea, m_oLearnedWordsGraphData,
                m_oLearnedWordsGraphPen, m_strLearnedWordsLabel, ref fYPositionMainLabel, true);
            DrawCurve(oGraphics, oDrawingArea, m_oTotalGraphData,
                m_oTotalGraphPen, m_strTotalWordsLabel, ref fYPositionMainLabel, false);
        }

        //===================================================================================================
        /// <summary>
        /// Draws the date labels
        /// </summary>
        /// <param name="oGraphics">Graphics object to draw at</param>
        /// <param name="oArea">The area for drawing</param>
        //===================================================================================================
        private void DrawDates(
            Graphics oGraphics, 
            Rectangle oArea
            )
        {
            int totalDays = (int)(m_dtmMaxDate - m_dtmMinDate).TotalDays;
            int xStep = oArea.Width / totalDays;
            float lastDrawnX = -float.MaxValue;

            for (int i = 0; i <= totalDays; i++)
            {
                DateTime currentDate = m_dtmMinDate.AddDays(i);
                int x = oArea.Left + i * xStep;

                string strDateText = currentDate.ToShortDateString();
                SizeF dateSize = oGraphics.MeasureString(strDateText, m_oDateFont);

                if (x - lastDrawnX >= dateSize.Width + 5)
                {
                    oGraphics.DrawString(strDateText, m_oDateFont, m_oTextBrush, 
                        new PointF(x - (dateSize.Width / 2), oArea.Bottom + 5));
                    lastDrawnX = x;
                }
            }
        }

        //===================================================================================================
        /// <summary>
        /// Draws curves
        /// </summary>
        /// <param name="oGraphics">Grapics object for drawing</param>
        /// <param name="oArea">Area for drawing</param>
        /// <param name="oData">Data to draw</param>
        /// <param name="oPen">Pen to use</param>
        /// <param name="strName">Name of the graph</param>
        /// <param name="fPosition">The last position of the main graph (words)</param>
        /// <param name="bAbove">Indicates if the new label shall be placed above the given position</param>
        //===================================================================================================
        private void DrawCurve(
            Graphics oGraphics, 
            Rectangle oArea, 
            IDictionary<DateTime, int> oData, 
            Pen oPen, 
            string strName,
            ref float fPosition,
            bool bAbove
            )
        {
            List<Point> aPoints = new List<Point>();

            int nTotalDays = (int)(m_dtmMaxDate - m_dtmMinDate).TotalDays;
            foreach (var oEntry in oData)
            {
                int x = oArea.Left + (int)((oEntry.Key - m_dtmMinDate).TotalDays * oArea.Width / nTotalDays);
                int y = oArea.Bottom - (oEntry.Value * oArea.Height / (m_nMaxValue + 1));
                aPoints.Add(new Point(x, y));
            }

            if (aPoints.Count > 1)
            {
                oGraphics.DrawLines(oPen, aPoints.ToArray());
            }

            if (aPoints.Count > 0)
            {
                Point oLastPoint = aPoints[aPoints.Count - 1];

                using (Brush oBrush = new SolidBrush(oPen.Color))
                {
                    float fNewPosition = oLastPoint.Y - 10;
                    if (bAbove)
                    {
                        if (fPosition + m_lblWords.Height > fNewPosition)
                            fNewPosition = fPosition + m_lblWords.Height;

                        if (fPosition == 0)
                            fPosition = fNewPosition;
                    }
                    else
                    {
                        if (fPosition - m_lblWordsLearned.Height < fNewPosition)
                            fNewPosition = fPosition - m_lblWordsLearned.Height;

                    }

                    StringFormat oFormat = new StringFormat();

                    if (this.RightToLeft == RightToLeft.Yes)
                    {
                        oFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
                        oFormat.Alignment = StringAlignment.Far;
                    }

                    oGraphics.DrawString(strName, m_lblTotal.Font, oBrush,
                            new PointF(oLastPoint.X + 5, fNewPosition), oFormat);
                }
            }
        }
    }
}
