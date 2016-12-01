﻿using DanmissionManager.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DanmissionManager.ViewModels
{
    class CategoriesViewModel : BaseViewModel
    {
        public CategoriesViewModel()
        {
            this.CreatedCategory = new Category();
            this.CreatedStandardprice = new Standardprice();
            
            this.CommandGetCategories = new RelayCommand2(GetCategoriesFromDatabase);
            this.CommandAddCategory = new RelayCommand2(AddCategory);
            this.CommandAddSubCategory = new RelayCommand2(AddSubCategory);
            
        }
        public RelayCommand2 CommandGetCategories { get; set; }
        public void GetCategoriesFromDatabase()
        {
            try
            {
                using (var ctx = new ServerContext())
                {
                    this.AllCategories = new ObservableCollection<Category>(ctx.Category.ToList());
                    this.AllSubCategories = new ObservableCollection<Standardprice>(ctx.Standardprices.ToList());
                }
            }
            catch (System.Data.DataException)
            {
                MessageBox.Show("Kunne ikke oprette forbindelse til databasen. Tjek din konfiguration og internet adgang.", "Error!");
            }
        }
        public RelayCommand2 CommandAddCategory { get; set; }
        public void AddCategory()
        {
            using (var ctx = new ServerContext())
            {
                ctx.Category.Add(this.CreatedCategory);
                ctx.SaveChanges();
            }
        }
        public RelayCommand2 CommandAddSubCategory { get; set; }
        public void AddSubCategory()
        {
            this.CreatedStandardprice.Parent_id = SelectedNewCategory.id;
            this.CreatedStandardprice.CorrespondingCategoryString = SelectedNewCategory.name;
            using (var ctx = new ServerContext())
            {
                ctx.Standardprices.Add(this.CreatedStandardprice);
                ctx.SaveChanges();
            }
        }

        private ObservableCollection<Standardprice> _shownSubCategories;

        public ObservableCollection<Standardprice> ShownSubCategories
        {
            get { return _shownSubCategories; }
            set
            {
                _shownSubCategories = value;
                OnPropertyChanged("ShownSubCategories");
            }
        }

        private ObservableCollection<Standardprice> _allSubCategories;
        public ObservableCollection<Standardprice> AllSubCategories
        {
            get
            {
                return _allSubCategories;
            }
            set
            {
                _allSubCategories = value;
                OnPropertyChanged("AllSubCategories");
            }
        }
        private ObservableCollection<Category> _allCategories;
        public ObservableCollection<Category> AllCategories
        {
            get { return _allCategories; }
            set
            {
                _allCategories = value;
                OnPropertyChanged("AllCategories");
            }
        }
        //properties for containing the newly created category and subcategory
        private Category _createdCategory;
        public Category CreatedCategory
        {
            get
            {
                return _createdCategory;
            }
            set
            {
                _createdCategory = value;
                OnPropertyChanged("CreatedCategory");
            }
        }
        private Standardprice _createdStandardprice;
        public Standardprice CreatedStandardprice
        {
            get { return _createdStandardprice; }
            set
            {
                _createdStandardprice = value;
                OnPropertyChanged("CreatedStandardPrice");
            }
        }
        //there are two properties, because it is possible to select a category from multiple sources
        //maybe both those selection should change the same property, but that can be changed later if that makes more
        //sense

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
                ChangeCollection();
            }
        }
        private void ChangeCollection()
        {
            List<Standardprice> list = new List<Standardprice>();
            list = this.AllSubCategories.Where(x => this.SelectedCategory.id == x.Parent_id).ToList();
            
            ObservableCollection<Standardprice> collection = new ObservableCollection<Standardprice>(list);
            this.ShownSubCategories = collection;
        }
        private Category _selectedNewCategory;
        public Category SelectedNewCategory
        {
            get { return _selectedNewCategory; }
            set
            {
                _selectedNewCategory = value;
                OnPropertyChanged("SelectedNewCategory");
            }
        }
        //property for selected sub category item
        private Standardprice _selectedSubCategory;
        public Standardprice SelectedSubCategory
        {
            get
            {
                return _selectedSubCategory;
            }
            set
            {
                _selectedSubCategory = value;
                OnPropertyChanged("SelectedSubCategory");
            }
        }
    }
}
