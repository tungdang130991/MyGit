using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HPCApplication
{
    public class ConstNews
    {
        public static Int16 AddNew = 12;//Bài mới
        public static Int16 NewsReturn = 13;//Bài trả lại người nhập tin
        public static Int16 NewsAppro = 1;//Nhập tin bài      

        public static Int16 NewsApproving_tk = 72;//Bài chờ trình bày
        public static Int16 NewsReturn_tk = 73;//Bài trả lại trình bày
        public static Int16 NewsAppro_tk = 7;//Trình bày tin bài

        public static Int16 NewsApproving_tb = 82;//Bài chờ biên tập
        public static Int16 NewsReturn_tb = 83;//Bài trả lại biên tập
        public static Int16 NewsAppro_tb = 8;//Biên tập tin bài

        public static Int16 NewsApproving_tbt = 92;//Bài chờ duyệt
        public static Int16 NewsAppro_tbt = 9;//Duyệt tin bài
        public static Int16 NewsPublishingSchedule = 93;//Bài hẹn giờ xuất bản

        public static Int16 NewsPublishing = 6;//Bài đang đăng
        public static Int16 NewsUnPublishing = 4;//Bài hủy đăng

        public static Int16 NewsDelete = 55;//Bài đã xóa
    }
}
