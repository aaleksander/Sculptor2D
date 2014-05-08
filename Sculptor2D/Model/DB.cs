/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 31.01.2013
 * Время: 11:20
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Xml;
using System.Collections.Generic;
using PetaPoco;
using System.Linq;

namespace Model
{
    public class InvalideIndexException : Exception { }
	public class EmptySelectException : Exception { }
	/// <summary>
	/// Работа с БД
	/// </summary>
	public class DB: Database
	{
		//public DB():base(App.ConnectionString, "Oracle.ManagedDataAccess.Client"){}
		public DB():base(""){}

        public DB(string cs) : base(cs) { }

        public T SelectByRN<T>(int aRN)
        {
            try{
				var res = this.Single<T>(aRN);//    // Query<T>(SQLFactory._SelectByRN<T>(aRN));            	
            	return res;
            }catch(InvalidOperationException)
            {
            	throw new EmptySelectException();
            }
        }

        public List<T> SelectAll<T>(string aOrder = "")
        {
        	Sql s = SQLFactory.Select<T>();
        	if( aOrder != "" )
        		s = s.OrderBy(aOrder);
        	var res = Query<T>(s);
        	return res.ToList();
        }
        
        public List<T> SelectLike<T>(string aField, string aLike)
        {
        	Sql s = SQLFactory.Select<T>()
        		.Where(aField + " like '" + aLike + "'");
        	var res = Query<T>(s);
        	return res.ToList();
        }
        
        
        public IEnumerable<T> SelectByPRN<T>(string aFieldPRN, int aPRN, string aOrder = "")
        {
        	Sql s = SQLFactory.Select<T>().Where(aFieldPRN + " = " + aPRN.ToString());
        	if( aOrder != "" )
        		s = s.OrderBy(aOrder);
        	return Query<T>(s);
        }

		/// <summary>
		/// запросить у БД новый идешник
		/// </summary>
		/// <returns></returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public int GetId()
		{
			return ExecuteScalar<int>("select OEN.seq_common.nextval from dual");
		}

/*		public static void setParams(string aUserName, string aPassword, string aSchema)
		{
			//переписываем файл конфигурации
			System.Data.SqlClient.SqlConnectionStringBuilder sb = new System.Data.SqlClient.SqlConnectionStringBuilder();
            sb.DataSource = string.Format(CultureInfo.InvariantCulture, "(DESCRIPTION = (ADDRESS = " +
                "(Protocol = tcp)(Host={0})" + "(PORT = 1521))(CONNECT_DATA = " 
                + "(SERVICE_NAME={1})))", "db09-uht", aSchema); // строка, задающая параметры соединения

			sb.UserID = aUserName;
			sb.Password= aPassword;
			UpdateConfigFile(sb.ConnectionString);

			ConfigurationManager.RefreshSection("connectionStrings");
		}*/

		public static void UpdateConfigFile(string con)
		{
		    //updating config file
		    XmlDocument XmlDoc = new XmlDocument();
		    //Loading the Config file
		    XmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
		    foreach (XmlElement xElement in XmlDoc.DocumentElement)
		    {
		        if (xElement.Name == "connectionStrings")
		        {
		            //setting the coonection string
		            xElement.FirstChild.Attributes[2].Value = con;
		        }
		    }
		    //writing the connection string in config file
		    XmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
		}

/*        public static bool Connect(string aUser, string aPassword)
        {
            System.Data.SqlClient.SqlConnectionStringBuilder sb = new System.Data.SqlClient.SqlConnectionStringBuilder();
            sb.DataSource = string.Format(CultureInfo.InvariantCulture, "(DESCRIPTION = (ADDRESS = "                                          
                + "(Protocol = TCP)(Host={0})" + "(PORT = 1521))(CONNECT_DATA = "
                + "(FAILOVER_MODE=(TYPE=select)(METHOD=basic))(SERVER=dedicated)"
                + "(SERVICE_NAME={1})))", "db09-uht", "OEN"); // строка, задающая параметры соединения
            
            sb.UserID = aUser;
            sb.Password = aPassword;
            //string connString = sb.ConnectionString;

            string connString = "User ID=oen; Password=oen; Data Source=db09-uht:1521/OEN";
           
            //DB.setParams(aUser, aPassword, "OEN");
			//UpdateConfigFile(sb.ConnectionString);
			UpdateConfigFile(connString);

			ConfigurationManager.RefreshSection("connectionStrings");

            return true;
        }*/
	}
}
