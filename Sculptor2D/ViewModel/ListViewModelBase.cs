using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Model;

namespace ViewModel
{
    public class ListViewModelBase<Model, ViewModel>: ViewModelBase<Model>
        where Model: ModelBase
        where ViewModel: ViewModelBase<Model>, new()
    {
        protected ObservableCollection<ViewModel> _list = new ObservableCollection<ViewModel>();

        public ListViewModelBase()
        {
        	//FillList();
        }

        public ListViewModelBase(Model aFirst)
        {
        	FillList();
                
            if( aFirst != null )
            {//эта штука нужна для выпадающих списков для дополнительного значения, типа "не выбрано".
            	var n = new ViewModel();
            	n.Data = aFirst;
            	_list.Insert(0, n);
            }                	
        }

        protected void FillList(String aWhere = "", String aOrder = "", string aConnect = "")
        {        	
        	DB db;

        	if( aConnect == "" )
        		db = new DB();
        	else
        		db = new DB(aConnect);        			
        	
        	var sql = SQLFactory.Select<Model>();
        	if( aWhere != "" )
        		sql.Where(aWhere);
        	if( aOrder != "" )
        		sql.OrderBy(aOrder);
        	
        	var q = db.Query<Model>(sql);

        	_list.Clear();
            foreach (Model r in q)
            {
                var n = new ViewModel();
                n.Data = r;
                _list.Add(n);
            }
            db.Dispose();
        }
        
        public ObservableCollection<ViewModel> List
        {
            get
            {
                return _list;
            }
        }

        public ViewModel this[int index]
        {
            get
            {
                if (index < 0 || index >= _list.Count)
                    throw new InvalideIndexException();
                return _list[index];
            }
        }

        /// <summary>
        /// проставить автоматическую нумерацию
        /// </summary>
        public void SetAutoPPs()
        {
            int pp = 1;
            foreach (var r in _list)
            {
                r.AutoPP = pp;
                pp++;
            }
        }
        
        /// <summary>
        /// удаляет из списка элементы по условию
        /// </summary>
        /// <param name="f"></param>
        public void RemoveBy(Func<ViewModel, bool> f)
        {
        	var xx = List.FirstOrDefault(f);
        	if( xx != default(ViewModel) )
        	{
        		while (List.Remove(xx))
        		{
        			xx = List.FirstOrDefault(f);
        			if( xx == default(ViewModel) )
        				break;
        		}
        	}        
        }
        
    }
}
