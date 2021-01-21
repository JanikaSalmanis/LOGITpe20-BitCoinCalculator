using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitCoinCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void gateRatesBtn_Click(object sender, EventArgs e)
        {
            if (currencyMenu.SelectedItem.ToString() == "EUR")
            {
                    resultLbl.Visible = true;
                    result.Visible = true;

                    BitCoinRate resultRates = GetRates();
                    int userCoins = Int32.Parse(amountOfBtc.Text);                
                    float currentRate = resultRates.bpi.EUR.rate_float;
                    float btcResult = userCoins * currentRate;
                    result.Text = $"{btcResult} {resultRates.bpi.EUR.code}";
            }
        }

        private static BitCoinRate GetRates()
        {

            string url = "https://api.coindesk.com/v1/bpi/currentprice.json";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            BitCoinRate bitcoin;

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            using (var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd();
                bitcoin = JsonConvert.DeserializeObject<BitCoinRate>(response);

            }

            return bitcoin;
        }
    }
}
