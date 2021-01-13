﻿using System;
using System.Collections.Generic;
using System.Text;
using Utility.Extensions;
using Xunit;

namespace Utility.TST
{
    public class SqlHelperTest
    {
        private const string Except = @"INSERT INTO [dbo].[table]([Key],[Name],[Value],[Type],[Class],[CreateTime],[UpdateTime],[ModifiedTime])
VALUES('10050',null,5,4,null,'2021-01-11 12:55:44.543','2021-01-11 12:55:44.543',null)";

        [Fact]
        public void GenerateInsert_Test()
        {
            var model = new Model
            {
                Key = "10050",
                Name = null,
                Value = 5,
                Type = 4,
                Class = null,
                CreateTime = new DateTime(2021, 1, 11, 12, 55, 44, 543),
                UpdateTime = new DateTime(2021, 1, 11, 12, 55, 44, 543),
                ModifiedTime = null,
            };

            var actual = SqlHelper.GenerateInsert(model, "[dbo].[table]", "Invalid");
            Assert.Equal(Except, actual);
        }
    }

    public class Model
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public int? Type { get; set; }
        public int? Class { get; set; }
        public int? Invalid { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? ModifiedTime { get; set; }
    }
}
