using System;
using System.Diagnostics;

namespace Ximble.DataModel
{
    public class UnitOfWork : IDisposable
    {
        private AdventureWorks2017Entities _context = null;
        private GenericRepository<ProductDescription> _productDescriptionRepository;
        private GenericRepository<Product> _productRepository;
        private GenericRepository<PurchaseOrderDetail> _purchaseOrderDetailRepository;

        public UnitOfWork()
        {
            _context = new AdventureWorks2017Entities();
        }

        public GenericRepository<Product> ProductRepository
        {
            get
            {
                if (this._productRepository == null)
                {
                    this._productRepository = new GenericRepository<Product>(_context);
                }
                    
                return _productRepository;
            }
        }

        public GenericRepository<ProductDescription> ProductDescriptionRepository
        {
            get
            {
                if (this._productDescriptionRepository == null)
                {
                    this._productDescriptionRepository = new GenericRepository<ProductDescription>(_context);
                }
                   
                return _productDescriptionRepository;
            }
        }

        public GenericRepository<PurchaseOrderDetail> PurchaseOrderDetailRepository
        {
            get
            {
                if (this._purchaseOrderDetailRepository == null)
                {
                    this._purchaseOrderDetailRepository = new GenericRepository<PurchaseOrderDetail>(_context);
                }
                    
                return _purchaseOrderDetailRepository;
            }
        }

        private bool disposed = false; 

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        } 

    }
}