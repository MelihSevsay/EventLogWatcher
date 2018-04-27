using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using EventLogListener.Contract.Type.Models;
using EventLogListener.Contract.Type.Enums;
using EventLogListener.Contract.Extentions;

namespace EventLogListener.Contract.Managers
{
    public static class DbOperationManager
    {


        public static string ConnString = ConfigurationManager.AppSettings["ConnString"].ToString();

        public static void GetRDPHistory()
        {

            try
            {

                //string constr = "user id=MASSDENEME;password=123456_tT;data source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=192.168.56.2)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=SID_NAME)))";
                
                OracleConnection conn = new OracleConnection(ConnString);

                if (conn.State != ConnectionState.Open)
                    conn.Open();

                OracleParameter parm = new OracleParameter();
                parm.OracleDbType = OracleDbType.Int16;

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM SYS_RDP_LOG";
                cmd.CommandType = CommandType.Text;

                OracleDataReader dr = cmd.ExecuteReader();

                var rdpHistoryList = new List<RDPUser>();
                while (dr.Read())
                {
                    var oUser = new RDPUser();
                    RDPType enumResult;
                    Enum.TryParse<RDPType>(dr["RDP_TYPE"].ToString(), out enumResult);

                    oUser.EventId = Convert.ToInt32(dr["eventid"]);
                    oUser.RDP_IP = Convert.ToString(dr["RDP_IP"]);
                    oUser.RDP_USER = Convert.ToString(dr["RDP_USER"]);
                    oUser.RDP_TYPE = enumResult.ToString();
                    rdpHistoryList.Add(oUser);
                }
                dr.Close();
                dr.Dispose();

                // Close connection
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            catch (Exception ex)
            {

                StreamWriterExtention.WriteToFile(ex.Message, forceToWrite: true);

            }



        }


        public static void InsertRDPHistory(RDPUser rdpUser)
        {


            try
            {
                
                var commandText = $"insert into SYS_RDP_LOG(eventid, rdp_ip, rdp_user, rdp_date, rdp_type) values(:{nameof(rdpUser.EventId)}, :{nameof(rdpUser.RDP_IP)}, :{nameof(rdpUser.RDP_USER)}, :{nameof(rdpUser.RDP_DATE)}, :{nameof(rdpUser.RDP_TYPE)})";

                using (OracleConnection connection = new OracleConnection(ConnString))
                using (OracleCommand command = new OracleCommand(commandText, connection))
                {

                    command.Parameters.Add(new OracleParameter(nameof(rdpUser.EventId), rdpUser.EventId));
                    command.Parameters.Add(new OracleParameter(nameof(rdpUser.RDP_IP), rdpUser.RDP_IP));
                    command.Parameters.Add(new OracleParameter(nameof(rdpUser.RDP_USER), rdpUser.RDP_USER));
                    command.Parameters.Add(new OracleParameter(nameof(rdpUser.RDP_DATE), rdpUser.RDP_DATE));
                    command.Parameters.Add(new OracleParameter(nameof(rdpUser.RDP_TYPE), rdpUser.RDP_TYPE.ToString()));

                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    command.Connection.Close();

                }

            }
            catch (Exception ex)
            {
                StreamWriterExtention.WriteToFile(ex.Message,forceToWrite: true);
            }


            


        }

    }
}
