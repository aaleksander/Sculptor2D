/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 31.01.2013
 * Время: 12:55
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using PetaPoco;

namespace Model
{
	[Serializable]
	public class NoTableNameAttributeException: Exception
	{
		public NoTableNameAttributeException(SerializationInfo si, StreamingContext sc):base(si, sc){}
		public NoTableNameAttributeException():base(){}
		public NoTableNameAttributeException(String aMessage):base(aMessage){}
		public NoTableNameAttributeException(String str, Exception ex):base(str, ex){}
	}
	
	public class SQLFactory
	{
		protected static string TableName<T>()
		{
			System.Attribute[] attrs = (System.Attribute[])typeof(T).GetCustomAttributes( typeof(TableNameAttribute), false );

			string tableName = ((TableNameAttribute) attrs[0]).Value;
            
            if( tableName == "" )
            {
            	throw new NoTableNameAttributeException();
            }
            
            return tableName;
		}
		
        public static Sql Select<T>()
        {            
            return Sql.Builder
                .Select("*")
            	.From(TableName<T>());
        }
        
        public static Sql _SelectByRN<T>(int aRN)
        {
            return
                Sql.Builder
				.Select("*")
				.From(TableName<T>())
				.Where("RN = " + aRN.ToString(CultureInfo.CurrentCulture));
        }
        
		//Возвращает количество записей в таблице
		public static int Count<T>()
		{
			using( var db = new DB() )
			{
				return db.ExecuteScalar<int>("SELECT Count(*) FROM " + TableName<T>());
			}
		}
		
		
	}	
	
	/// <summary>
	/// Description of ModelBase.
	/// </summary>
	public class ModelBase
	{
		public ModelBase()
		{
		}
		
		/// <summary>
		/// Возвращает готовый запрос вида select * from ....
		/// </summary>
		/// <returns></returns>
/*		public static Sql Select
		{
			get{
				return Sql.Builder
					.Select("*")
					.From(TableName);
			}
		}*/

/*		public static Sql SelectByRN(int aRN)
		{
			return Sql.Builder
				.Select("*")
				.From(TableName)
				.Where("RN = " + aRN.ToString());
		}*/
		
/*		public static Sql SelectByOther(string aField, int aRN)
		{
			return Sql.Builder
				.Select("*")
				.From(SQLFactory.GetTableName<)
				.Where(aField + " = " + aRN.ToString());
		}		*/

		/// <summary>
		/// имя таблицы БД
		/// </summary>
/*		public static string TableName
		{
			get{
				//ищем аттрибуд TableName	
				//берем весь стэк вызовов				
				StackTrace st = new StackTrace(false);
				string tableName = "";
				for(int i = st.FrameCount - 1; i >= 0; i--) //перебираем от дочерних классов к базовым
				{
					//определяем типы во всех стэках					
					StackFrame frame = st.GetFrame(i);
					MethodBase mi = frame.GetMethod();
					Type declaringType = mi.DeclaringType;
	
					System.Attribute[] attrs = System.Attribute.GetCustomAttributes( declaringType );
					tableName = "";
			        foreach (System.Attribute attr in attrs )
			        {
			            if (attr is TableNameAttribute)
			            {//мы нашли нужный аттрибут
			                tableName = ((TableNameAttribute) attr).Value;
							break;
			            }
			        }
			        if( tableName != "" )
			        	break;
				}
				
		        if( tableName == "" )		        	
		        	throw new NoTableNameAttributeException();
		        
		        return tableName;
			}		
		}*/
		

	}
}
