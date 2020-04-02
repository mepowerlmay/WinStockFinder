using System;
namespace WinStockFinder.Model
{
    /// <summary>FutureStock表实体类
    /// 作者:alonso(line id: menshin7)
    /// 创建时间:2020-04-02 14:30:56
    /// </summary>
    [Serializable]
    public partial class FutureStock
    {
        public FutureStock()
        { }
        private int _Id;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _Id = value; }
            get { return _Id; }
        }
        private string _FullName;
        /// <summary>
        /// 
        /// </summary>
        public string FullName
        {
            set { _FullName = value; }
            get { return _FullName; }
        }
        private string _StName;
        /// <summary>
        /// 
        /// </summary>
        public string StName
        {
            set { _StName = value; }
            get { return _StName; }
        }
        private string _Code;
        /// <summary>
        /// 
        /// </summary>
        public string Code
        {
            set { _Code = value; }
            get { return _Code; }
        }
        private string _IsFture;
        /// <summary>
        /// 
        /// </summary>
        public string IsFture
        {
            set { _IsFture = value; }
            get { return _IsFture; }
        }
        private string _IsOption;
        /// <summary>
        /// 
        /// </summary>
        public string IsOption
        {
            set { _IsOption = value; }
            get { return _IsOption; }
        }
        private string _Makret;
        /// <summary>
        /// 
        /// </summary>
        public string Makret
        {
            set { _Makret = value; }
            get { return _Makret; }
        }
        private string _OTC;
        /// <summary>
        /// 
        /// </summary>
        public string OTC
        {
            set { _OTC = value; }
            get { return _OTC; }
        }
        private string _MakretETF;
        /// <summary>
        /// 
        /// </summary>
        public string MakretETF
        {
            set { _MakretETF = value; }
            get { return _MakretETF; }
        }
    }
}
