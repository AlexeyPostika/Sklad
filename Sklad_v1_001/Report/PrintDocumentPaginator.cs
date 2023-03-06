using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows;

namespace Sklad_v1_001.Report
{
    public class PrintDocumentPaginator : DocumentPaginator
    {
        private Int32 _startIndex;
        private Int32 _endIndex;
        private DocumentPaginator _paginator;
        public PrintDocumentPaginator(DocumentPaginator paginator, PageRange pageRange)
        {
            _startIndex = pageRange.PageFrom - 1;
            _endIndex = pageRange.PageTo - 1;
            _paginator = paginator;
            _endIndex = Math.Min(_endIndex, _paginator.PageCount - 1);
        }
        public override DocumentPage GetPage(Int32 pageNumber)
        {
            var page = _paginator.GetPage(pageNumber + _startIndex);
            var cv = new ContainerVisual();
            if (page.Visual is FixedPage)
            {
                foreach (var child in ((FixedPage)page.Visual).Children)
                {
                    var childClone = (UIElement)child.GetType().GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(child, null);
                    var parentField = childClone.GetType().GetField("_parent", BindingFlags.Instance | BindingFlags.NonPublic);
                    if (parentField != null)
                    {
                        parentField.SetValue(childClone, null);
                        cv.Children.Add(childClone);
                    }
                }
                return new DocumentPage(cv, page.Size, page.BleedBox, page.ContentBox);
            }
            return page;
        }
        public override Boolean IsPageCountValid
        {
            get { return true; }
        }
        public override Int32 PageCount
        {
            get
            {
                if (_startIndex > _paginator.PageCount - 1)
                    return 0;
                if (_startIndex > _endIndex)
                    return 0;
                return _endIndex - _startIndex + 1;
            }
        }
        public override Size PageSize
        {
            get { return _paginator.PageSize; }
            set { _paginator.PageSize = value; }
        }
        public override IDocumentPaginatorSource Source
        {
            get { return _paginator.Source; }
        }
    }
}
