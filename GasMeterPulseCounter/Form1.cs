using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace GasMeterPulseCounter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "0";
            label2.Text = "0";
            PulseCountClass pulse = new PulseCountClass("COM4");
            //pulse.GasMeterPortname = "COM4";
            pulse.PulseCount();
            label1.Text = pulse.TimeIPMeasure.ToString();
            label2.Text = pulse.GasMeterFlow.ToString();
        }
        #region　廃止

        /*
        System.IO.Ports.SerialPort spGasMeter = new System.IO.Ports.SerialPort();
        Stopwatch STWIP = new Stopwatch();
        Stopwatch STWCnt = new Stopwatch();
        string GasMeterPortname = "COM4";

        public double TimeIPMeasure;
        public ulong GasMeterCnt;
        public long TimeLimitIPMeasure = 10000;
        //string GasMeterStr;
        //public ulong 


        public void PulseCount()
        {
            //STWCnt.Reset();
            //STWIP.Reset();
            spGasMeter.PortName = GasMeterPortname;
            spGasMeter.BaudRate = 115200;

            spGasMeter.Open();
            //ulong cnt = 0;
            //GasMeterCnt = 0;
           
            bool flg ;

            flg = true;
            spGasMeter.Write("b");
            STWCnt.Restart();
            STWIP.Restart();

            while (flg)
            {
                try
                {
                    spGasMeter.DiscardInBuffer();
                    spGasMeter.ReadTimeout = 100;
                    spGasMeter.Write("a");
                    GasMeterCnt = ulong.Parse(spGasMeter.ReadLine());
                    //Console.WriteLine("時間：{0}秒  パルス：{1}", STWCnt.Elapsed.TotalSeconds.ToString(), GasMeterStr);
                }
                catch (Exception)
                {
                }

                if (STWCnt.ElapsedMilliseconds > TimeLimitIPMeasure)
                {
                    TimeIPMeasure = (double)STWIP.ElapsedMilliseconds;
                    STWIP.Reset();
                    STWCnt.Reset();
                    TimeIPMeasure = TimeIPMeasure / 1000;
                    flg = false;
                }
            }

            spGasMeter.Close();

        }
        */
        #endregion
    }

    /// <summary>
    /// COMNoを渡す。TimeIPMeasureとGasMeterCnt
    /// </summary>
    public class PulseCountClass
    {
        public PulseCountClass(string portname)
        {
            spGasMeter.PortName = portname;
            
        }

        System.IO.Ports.SerialPort spGasMeter = new System.IO.Ports.SerialPort();
        Stopwatch STWIP = new Stopwatch();
        Stopwatch STWCnt = new Stopwatch();
        //public string GasMeterPortname;

        /// <summary>
        /// 実測定時間[s]
        /// </summary>
        public double TimeIPMeasure;

        /// <summary>
        /// 測定ガス流量[L]
        /// </summary>
        public double GasMeterFlow;


        private long GasMeterCnt;

        /// <summary>
        /// 目標測定時間[s]
        /// </summary>
        public long TimeLimitIPMeasure;
        public void PulseCount()
        {
            //spGasMeter.PortName = GasMeterPortname;
            spGasMeter.BaudRate = 115200;

            spGasMeter.Open();
            bool flg=true;
            spGasMeter.Write("b");
            STWCnt.Restart();
            STWIP.Restart();

            while (flg)
            {
                try
                {
                    spGasMeter.DiscardInBuffer();
                    spGasMeter.ReadTimeout = 100;
                    spGasMeter.Write("a");
                    GasMeterCnt = long.Parse(spGasMeter.ReadLine());
                }
                catch (Exception)
                {
                }

                if (STWCnt.ElapsedMilliseconds > TimeLimitIPMeasure*1000)
                {
                    TimeIPMeasure = (double)STWIP.ElapsedMilliseconds;
                    STWIP.Reset();
                    STWCnt.Reset();
                    TimeIPMeasure = TimeIPMeasure / 1000;
                    flg = false;
                }
            }

            spGasMeter.Close();
            GasMeterFlow = (double)GasMeterCnt / 100;//ガス流量単位を[L]に変更
            
        }

    }

}
