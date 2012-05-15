using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using LinkPointTransaction;

namespace BDT.Payments
{
    class CreditCard
    {
        public string Number { get; set; }
        public string ExpYear { get; set; }
        public string ExpMonth { get; set; }
    }

    class Order
    {
        public CreditCard CreditCard { get; set; }
        public Address Address { get; set; }
        public string Total { get; set; }
    }

    public class Processor
    {
        private string _configFile;
        private string _keyFile;
        private string _host;
        private int _port;
        
        public Processor()
        {
            // get some stuff from app config
            NameValueCollection config = (NameValueCollection)ConfigurationSettings.GetConfig("LPAPP_Config/client");

            _configFile = config["Configfile"];
            _keyFile = config["Keyfile"];
            _host = config["Host"];
            _port = Int32.Parse(config["Port"]);
        }

        public string ProcessOrder()
        {
            var testOrder = new Order
            {
                Address = new Address
                {
                    Street = "1234",
                    Zip = "37211"
                },
                CreditCard = new CreditCard
                {
                    Number = "41111111111111111",
                    ExpMonth = "08",
                    ExpYear = "12"
                },
                Total = "99.99"
            };
            // create order
            LPOrderPart order = LPOrderFactory.createOrderPart("order");
            // create a part we will use to build the order
            LPOrderPart op = LPOrderFactory.createOrderPart();

            // Build 'orderoptions'
            op.put("ordertype", "SALE");
            // add 'orderoptions to order
            order.addPart("orderoptions", op);

            // Build 'merchantinfo'
            op.clear();
            op.put("configfile", _configFile);
            // add 'merchantinfo to order
            order.addPart("merchantinfo", op);

            // Build 'billing'
            // Required for AVS. If not provided, 
            // transactions will downgrade.
            op.clear();
            op.put("zip", testOrder.Address.Zip);
            op.put("addrnum", testOrder.Address.Street);
            // add 'billing to order
            order.addPart("billing", op);

            // Build 'creditcard'
            op.clear();
            op.put("cardnumber", testOrder.CreditCard.Number);
            op.put("cardexpmonth", testOrder.CreditCard.ExpMonth);
            op.put("cardexpyear", testOrder.CreditCard.ExpYear);
            // add 'creditcard to order
            order.addPart("creditcard", op);

            // Build 'payment'
            op.clear();
            op.put("chargetotal", testOrder.Total);
            // add 'payment to order
            order.addPart("payment", op);

            // create transaction object	
            LinkPointTxn LPTxn = new LinkPointTxn();

            // get outgoing XML from the 'order' object
            string outXml = order.toXML();

            // Call LPTxn
            string resp = LPTxn.send(_keyFile, _host, _port, outXml);

            //Store transaction data on Session and redirect
            return resp;
            //Session["outXml"] = outXml;
            //Session["resp"] = resp;
        }
    }
}
