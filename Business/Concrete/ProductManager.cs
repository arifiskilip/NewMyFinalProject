using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using FluentValidation;
using ValidationException = FluentValidation.ValidationException;
using Core.Entities;
using System.Linq;
using Core.Utilities.Business;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        private readonly ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product entity)
        {
            var result = BusinessRules.Run(/*CheckIfProductCountOfCategoryCorrect(entity.CategoryId),*/
                /*CheckIfProductNameExists(entity.ProductName)*/Deneme());
            if (result != null)
            {
                return result;
            }
            _productDal.Add(entity);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(Product entity)
        {
            _productDal.Delete(entity);
            return new SuccessResult(Messages.ProductDeleted);
        }

        public IDataResult<Product> Get(int id)
        {
            return new SuccessDataResult<Product>(_productDal.Get(x=> x.ProductId == id));
        }

        public IDataResult<List<Product>> GetAll()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsList);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAllByCategoryId(categoryId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetByUnitPrice(min, max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(),Messages.ProductsDetailList);
        }

        public IResult Update(Product entity)
        {
            _productDal.Update(entity);
            return new SuccessResult();
        }
        

        //Bussiness Codes

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {//Kategorideki ürün sayının doğrulunu control et.
            int result = _productDal.GetAll(x => x.CategoryId == categoryId).Count;
            if (result > 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }
        private IResult CheckIfProductNameExists(string productName)
        {//Aynı isimde ürün eklenemez
            bool result = _productDal.GetAll(x => x.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.UnableToAddProductWithSameName);
            }
            return new SuccessResult();
        }
        private IResult CheckIfCategoryLimitExceeded()
        { //Kategori limitinin aşılmadığını kontrol et.
            int result = _categoryService.GetAll().Data.Count();
            if (result>7)
            {
                return new ErrorResult(Messages.CategoryLimitExceeded);
            }
            return new SuccessResult();
        }

    }
}
