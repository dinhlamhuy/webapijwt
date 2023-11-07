using System;
using System.IO;


namespace webapijwt
{
    public class sqlConnect
    {
        static readonly string svrdatabase = "PET_STORE_MANAGEMENT";
        static readonly string sname = "192.168.32.196";
        static readonly string svvrruser = "sa";
        static readonly string svrpass = "123";
        public static string Connect_String = $"Provider = SQLOLEDB;Data Source={sname};Initial Catalog={svrdatabase};User Id={svvrruser};Password={svrpass}";
        //string Connect_String = Get_SettingFile("ConnectString.ini").ToString();
        public static string Get_SettingFile(string FileName)
        {

            string applicationPath = AppDomain.CurrentDomain.BaseDirectory;

            string sStr = ",,,,,,,,";
            FileInfo fScreenSet = new FileInfo(applicationPath + @"\" + FileName);
            StreamReader srScreenSet = fScreenSet.OpenText();

            while (srScreenSet.Peek() >= 0)
            {
                sStr = srScreenSet.ReadLine();
            }
            srScreenSet.Close();
            return sStr;
        }
    }
}