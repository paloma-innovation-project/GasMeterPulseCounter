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
            label2.Text = pulse.GasMeterCnt.ToString();
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

    public class PulseCountClass
    {
        public PulseCountClass(string portname)
        {
            spGasMeter.PortName = portname;
            
        }

        System.IO.Ports.SerialPort spGasMeter = new System.IO.Ports.SerialPort();
        Stopwatch STWIP = new Stopwatch();
        Stopwatch STWCnt = new Stopwatch();
        public string GasMeterPortname;

        public double TimeIPMeasure;
        public ulong GasMeterCnt;
        public long TimeLimitIPMeasure = 10000;
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

    }

}
