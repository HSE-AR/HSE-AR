using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.ExceptionServices;
using HseAr.Infrastructure;

namespace HseAr.Dependencies
{
    public sealed class ContainerMapper : IMapper
    {
        private readonly IServiceProvider _service;

        public ContainerMapper(IServiceProvider service)
        {
            _service = service;
        }

        public TDest Map<TSource, TDest>(TSource source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            try
            {
                var mapper = (IMapper<TSource, TDest>)_service.GetService(typeof(IMapper<TSource, TDest>));
                return mapper.Map(source);
            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException != null)
                {
                    ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                }

                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Mapper wasn't registered.", ex);
            }

        }
    }
}