using System.Collections.Generic;
using EFCoreFeaturesAndDiag.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AspNet5MultipleProject.Controllers
{
    [Route("api/[controller]")]
    public class DataEventRecordsController : Controller
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public DataEventRecordsController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<DataEventRecord> Get()
        {
            return _dataAccessProvider.GetDataEventRecords();
        }

        [HttpGet]
        [Route("SourceInfos")]
        public IEnumerable<SourceInfo> GetSourceInfos(bool withChildren)
        {
            return _dataAccessProvider.GetSourceInfos(withChildren);
        }

        [HttpGet("{id}")]
        public DataEventRecord Get(long id)
        {
            return _dataAccessProvider.GetDataEventRecord(id);
        }

        [HttpGet("AddTest/")]
        public IActionResult AddTest()
        {
            DataEventRecord dataEventRecord = new DataEventRecord();
            dataEventRecord.Name = "AddTest";
            dataEventRecord.DataEventRecordId = 0;
            dataEventRecord.MadDescription = "Mad description test";
            dataEventRecord.SourceInfo = new SourceInfo();
            dataEventRecord.SourceInfo.SourceInfoId = 0;
            dataEventRecord.SourceInfo.Name = "S Add Test";
            dataEventRecord.SourceInfo.Description = "S Description";
            dataEventRecord.SourceInfoId = 0;

            _dataAccessProvider.AddDataEventRecord(dataEventRecord);

            return Ok("test data created");
        }

        [HttpPost]
        public void Post([FromBody]DataEventRecord value)
        {
            _dataAccessProvider.AddDataEventRecord(value);
        }

        [HttpPut("{id}")]
        public void Put(long id, [FromBody]DataEventRecord value)
        {
            _dataAccessProvider.UpdateDataEventRecord(id, value);
        }

        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            _dataAccessProvider.DeleteDataEventRecord(id);
        }
    }
}
