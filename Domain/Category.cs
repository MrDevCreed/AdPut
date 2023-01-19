using System;
using System.Collections.Generic;

namespace Domain
{
    public class Category
    {
        public Category(string title)
        {
            this.Title = title;
        }

        public Category(string title, Category parentCategory) : this(title)
        {
            this.ParentCategory = parentCategory;
        }

        public int Id { get; private set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Title Is Null!");

                if (value.Length > 100 || value.Length < 1)
                    throw new ArgumentOutOfRangeException("The Title Length Should Be [1-100]");

                _title = value;
            }
        }

        public List<Ad> Ads { get; private set; }

        public List<Category> SubCategories { get; private set; }

        private Category _parentCategory;

        public Category ParentCategory
        {
            get { return _parentCategory; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("Parent Category is Null!");

                _parentCategory = value;
            }
        }


        public bool IsBaseCategory => (ParentCategory != null);

        //Functions

        public void ChangeTitle(string title)
        {
            this.Title = title;
        }

        public void AddSubCategory(Category subCategory)
        {
            if (subCategory == null)
                throw new ArgumentNullException("Sub Category Is Null");

            this.SubCategories.Add(subCategory);
        }

        public void RemoveSubCategory(Category subcategory)
        {
            if (subcategory == null)
                throw new ArgumentNullException("Sub Category Is Null");

            this.SubCategories.Remove(subcategory);
        }

        public void ChangeParentCategory(Category parentCategory)
        {
            this.ParentCategory = parentCategory;
        }
    }
}
