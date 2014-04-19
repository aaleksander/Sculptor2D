/*
 * Сделано в SharpDevelop.
 * Пользователь: anufrievaa
 * Дата: 10/26/2012
 * Время: 17:51
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Input;
using System.Linq;
using Model;
using Commands;

namespace ViewModel
{
	/// <summary>
	/// поле, обозначенное этим аттрибутом, будет обнуляться при allPropertyChanged
	/// </summary>
	public class ExternalAttribute: Attribute
	{
		private object _val;
		
		public ExternalAttribute(object a)
		{
			_val = a;
		}
		
		public object GetEmptyValue()
		{
			return _val;
		}
	}
	
	
	
	/// <summary>
	/// Description of ViewModelBase.
	/// </summary>
	public class ViewModelBase<T>:INotifyPropertyChanged
	{
		protected static string dateFormat = "d";
		
		/// <summary>
		/// Надо ли делать апдейт при изменении свойств
		/// </summary>
		private bool _realTimeUpdate = false;
		
        protected T _data;

        public T Data
        {
            set
            {
                _data = value;
                OnAllPropertyChanged();
            }
            get
            {
                return _data;
            }
        }

        public ViewModelBase(T a)
        {
            _data = a;
            _changed = false;
            
        }

		public bool RealTimeUpdate{
			set{
				_realTimeUpdate = value;
			}
			get{
				return _realTimeUpdate;
			}
		}

		public ViewModelBase()
		{
            _data = default(T);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void onPropertyChanged(string aProp){			
			_onPropertyChanged(new PropertyChangedEventArgs(aProp));						
		}		

		protected void _onPropertyChanged(PropertyChangedEventArgs e)
		{
			if( PropertyChanged != null )
				PropertyChanged(this, e);
		}
		
		public void OnAllPropertyChanged()
		{//Получить все свойcтва и послать сигнал об их обновлении
			_onPropertyChanged(new PropertyChangedEventArgs(null));
			
			//пройтись по свойствам и, если есть аттрибут External - обналлить их			
			PropertyInfo[] props = this.GetType().GetProperties();
			
	        foreach (var prop in props)
	        {
	        	object val;
	        	var attrs = prop.GetCustomAttributes(typeof(ExternalAttribute), false);
	        	if( attrs.Length > 0 )
	        	{
	        		val = ((ExternalAttribute)attrs[0]).GetEmptyValue();
	        		prop.SetValue(this, Convert.ChangeType(val, prop.PropertyType), null);
	        		onPropertyChanged(prop.Name);
	        	}
	        }
		}		

        //некое универсальное свойство, что мол было изменено
		public bool Changed{
			set{
				_changed = value;
				//OnAllPropertyChanged();
				_onPropertyChanged(new PropertyChangedEventArgs("Changed"));
			}
			get{
				return _changed;
			}			
		}
		private bool _changed = false;

        /// <summary>
        /// Автоматический порядковый номер в списке
        /// </summary>
        public int AutoPP
        {
            set
            {
                _autoPP = value;
                onPropertyChanged("PP");
            }
            get
            {
                return _autoPP;
            }
        }

		public Boolean IsSelected{
			set{
				_selected = value;
				onPropertyChanged("IsSelected");
			}
			get{
				return _selected;
			}
		}
		private Boolean _selected;
        
        
        private int _autoPP;
	}
}
