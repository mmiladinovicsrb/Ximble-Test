using System;
using System.Diagnostics;

namespace Ximble.DataModel
{
    /// <summary>
    /// Unit of Work class responsible for DB transactions
    /// </summary>
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

        /// <summary>
        /// Get/Set Property for product repository.
        /// </summary>
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

        /// <summary>
        /// Get/Set Property for user repository.
        /// </summary>
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

        /// <summary>
        /// Get/Set Property for token repository.
        /// </summary>
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

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
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

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        } 

    }
}