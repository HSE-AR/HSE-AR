using HseAr.Data.Entities;
using HseAr.Infrastructure;

namespace HseAr.Data.Mappers
{
    public class TestMapper : IMapper<TestSource,TestResult>
    {
        public TestResult Map(TestSource source)
        {
            return new TestResult()
            {
                FieldResult = source.FieldSource
            };
        }
        
    }
    
    public class TestSource
    {
        public string FieldSource { get; set; }
    }
    
    public class TestResult 
    {
        public string FieldResult{ get; set; }
    }
    
    
    
}