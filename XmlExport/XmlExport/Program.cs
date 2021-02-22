using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XmlExport
{
    class Program
    {
        private const string ExportFilePath = "videos.xls";

        static async Task Main(string[] args)
        {
            var propTable = CreateTable<replyNode>();
            var equips = await ReadFileAsync("videos.xml");

            var serializer = new XmlSerializer(typeof(reply));
            var data = Encoding.UTF8.GetBytes(equips);
            using (var ms = new MemoryStream(data))
            using (var sr = new StreamReader(ms))
            {
                var reply = serializer.Deserialize(sr) as reply;

                //foreach (var node in reply.nodeList)
                //{
                //    // 只保留音视频设备
                //    //if (node.nodeType != "av" && node.avType != "AV_CHANNEL")
                //    //    continue;

                //    count++;
                //}

                FillData(propTable, reply.nodeList);
                TableToExcel(propTable, ExportFilePath);
            }

            Console.WriteLine(propTable.Rows.Count);
        }

        private static async Task<string> ReadFileAsync(string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (var sr = new StreamReader(stream))
            {
                return await sr.ReadToEndAsync();
            }
        }

        /// <summary>
        /// 创建表
        /// </summary>
        private static DataTable CreateTable<T>()
        {
            var result = new DataTable();
            var type = typeof(T);
            foreach (var property in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                var propertyType = property.PropertyType;
                if ((propertyType.IsGenericType) && (propertyType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                    propertyType = propertyType.GetGenericArguments()[0];
                result.Columns.Add(property.Name, propertyType);
            }
            return result;
        }

        /// <summary>
        /// 填充数据
        /// </summary>
        private static void FillData<T>(DataTable dt, IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                dt.Rows.Add(CreateRow(dt, entity));
            }
        }

        /// <summary>
        /// 创建行
        /// </summary>
        private static DataRow CreateRow<T>(DataTable dt, T entity)
        {
            DataRow row = dt.NewRow();
            var type = typeof(T);
            foreach (var property in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                row[property.Name] = property.GetValue(entity) ?? DBNull.Value;
            }
            return row;
        }

        /// <summary>
        /// Datable导出成Excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="file">导出路径(包括文件名与扩展名)</param>
        public static void TableToExcel(DataTable dt, string file)
        {
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Sheet1");

            //表头  
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].ColumnName);
            }

            //数据  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                }
            }

            //转为字节数组  
            MemoryStream stream = new MemoryStream();
            workbook.Write(stream);
            var buf = stream.ToArray();

            //保存为Excel文件  
            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                fs.Write(buf, 0, buf.Length);
                fs.Flush();
            }
        }
    }


    //#region 类

    ///// <summary>
    ///// 常量
    ///// </summary>
    //public static class ZNetConst
    //{
    //    public const int COMM_STATUS = 0x1001; //状态消息 参考@ZNET_DeviceStatusInfo
    //    public const int COMM_ALARM = 0x1100; //报警信息
    //    public const int COMM_TRADEINFO = 0x1500; //ATMDVR主动上传交易信息
    //    public const int EXCEPTION_AUDIOEXCHANGE = 0x8001; //语音对讲异常
    //    public const int EXCEPTION_ALARM = 0x8002; //报警异常
    //    public const int EXCEPTION_PREVIEW = 0x8003; //网络预览异常
    //    public const int EXCEPTION_SERIAL = 0x8004; //透明通道异常
    //    public const int EXCEPTION_RECONNECT = 0x8005; //预览时重连
    //    public const int DEV_PATHS_LEN = 64;
    //    public const int GRP_ID_LEN = 64;
    //    public const int GRP_NAME_LEN = 128;
    //    public const int GRP_NODE_LEN = 24;
    //    public const int NODE_ID_LEN = 40;
    //    public const int URL_LEN = 256;
    //    public const int IPSAN_TARGET_LEN = 128;
    //    public const int NRU_SERVER_ID_LEN = 64;
    //    public const int NRU_SERVER_NAME_LEN = 128;
    //    public const int IPV6_LEN = 128;
    //    public const int IP_ADDR_LEN = 16;
    //    public const int MAC_ADDR_LEN = 32;
    //    public const int IP_LEN = 256;
    //    public const int NAME_LEN = 32;
    //    public const int SERVER_ID_LEN = 32;
    //    public const int GRP_DIR_ID_LEN = 32;
    //    public const int DEVID_LEN = 32;

    //    public const int SERIALNO_LEN = 48;
    //    public const int MACADDR_LEN = 6;
    //    public const int MAX_ETHERNET = 2;
    //    public const int PATHNAME_LEN = 128;
    //    public const int PASSWD_LEN = 16;
    //    public const int MAX_CHANNUM = 512;
    //    public const int MAX_ALARMOUT = 128;
    //    public const int MAX_TIMESEGMENT = 4;
    //    public const int MAX_PRESET = 128;
    //    public const int MAX_DAYS = 7;
    //    public const int PHONENUMBER_LEN = 32;
    //    public const int MAX_DISKNUM = 16;
    //    public const int MAX_WINDOW = 16;
    //    public const int MAX_VGA = 1;
    //    public const int MAX_USERNUM = 16;
    //    public const int MAX_EXCEPTIONNUM = 16;
    //    public const int MAX_LINK = 6;
    //    public const int MAX_ALARMIN = 128;
    //    public const int MAX_VIDEOOUT = 2;
    //    public const int MAX_NAMELEN = 16; //DVR本地登陆名
    //    public const int MAX_CHANNAME_LEN = 64;
    //    public const int MAX_RIGHT = 32; //权限
    //    public const int MAX_SHELTERNUM = 4; //遮挡区域数
    //    public const int MAX_DECPOOLNUM = 4;
    //    public const int MAX_DECNUM = 4;
    //    public const int MAX_TRANSPARENTNUM = 2;
    //    public const int MAX_STRINGNUM = 4;
    //    public const int MAX_AUXOUT = 4;
    //    public const int MAX_NFS_DISK = 8;
    //    public const int MAX_CYCLE_CHAN = 16;
    //    public const int MAX_DOMAIN_NAME = 64;
    //    public const int MAX_SERIAL_NUM = 64;

    //    public const int MAX_PATH_LEN = 2048;
    //    public const int MAX_ENVDAT_LEN = 32;
    //    public const int MAX_ENVOBJ_LEN = 256;
    //    public const int MAX_CRVSOBJ_LEN = 256;
    //    public const int MAX_LOG_SEVVERID_LEN = 64;
    //    public const int MAX_LOG_REVERSE_LEN = 256;
    //    public const int MAX_LOG_TIME_LEN = 128;
    //    public const int MAX_SVROBJ_LEN = 128;
    //    public const int PROTOCOL_NAME_LEN = 64;
    //    public const int NET_CHANNEL_EXTEN_LEN = 256;
    //    public const int D3D_TEXT_FACENAME_LEN = 32;
    //    public const int D3D_TEXT_MAX_LEN = 1024;
    //    public const int NET_IF_10M_HALF = 1; // 10M ethernet
    //    public const int NET_IF_10M_FULL = 2;
    //    public const int NET_IF_100M_HALF = 3; // 100M ethernet
    //    public const int NET_IF_100M_FULL = 4;
    //    public const int NET_IF_AUTO = 5;
    //    public const int IAVS = 0; //IAVS设备
    //}

    //// 注意: 生成的代码可能至少需要 .NET Framework 4.5 或 .NET Core/Standard 2.0。
    ///// <remarks/>
    //[System.SerializableAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    //[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    ///// <summary>
    ///// 设备信息查询应答
    ///// </summary>
    //public partial class reply
    //{

    //    private byte pageNumField;

    //    private byte totalPageField;

    //    private byte totalItemsField;

    //    private replyNode[] nodeListField;

    //    /// <remarks/>
    //    public byte pageNum
    //    {
    //        get
    //        {
    //            return this.pageNumField;
    //        }
    //        set
    //        {
    //            this.pageNumField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public byte totalPage
    //    {
    //        get
    //        {
    //            return this.totalPageField;
    //        }
    //        set
    //        {
    //            this.totalPageField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public byte totalItems
    //    {
    //        get
    //        {
    //            return this.totalItemsField;
    //        }
    //        set
    //        {
    //            this.totalItemsField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlArrayItemAttribute("node", IsNullable = false)]
    //    public replyNode[] nodeList
    //    {
    //        get
    //        {
    //            return this.nodeListField;
    //        }
    //        set
    //        {
    //            this.nodeListField = value;
    //        }
    //    }
    //}

    ///// <remarks/>
    //[System.SerializableAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    //public partial class replyNode
    //{

    //    private string idField;

    //    private string nameField;

    //    private string titleField;

    //    private string nodeObjNameField;

    //    private string referObjNameField;

    //    private string nodeTypeField;

    //    private byte levelField;

    //    private string modelField;

    //    private object descriptionField;

    //    private object indexField;

    //    private byte refPortField;

    //    private bool refPortFieldSpecified;

    //    private byte recordField;

    //    private bool recordFieldSpecified;

    //    private string avTypeField;

    //    private replyNodeInStreams inStreamsField;

    //    private replyNodeOutStreams outStreamsField;

    //    private string urlField;

    //    private string accessServerObjNameField;

    //    private string restoreServerObjNameField;

    //    private string statusField;

    //    private object longitudeField;

    //    private object latitudeField;

    //    private byte totalVideoChannelField;

    //    private byte onlineVideoChannelField;

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
    //    public string id
    //    {
    //        get
    //        {
    //            return this.idField;
    //        }
    //        set
    //        {
    //            this.idField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public string name
    //    {
    //        get
    //        {
    //            return this.nameField;
    //        }
    //        set
    //        {
    //            this.nameField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public string title
    //    {
    //        get
    //        {
    //            return this.titleField;
    //        }
    //        set
    //        {
    //            this.titleField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public string nodeObjName
    //    {
    //        get
    //        {
    //            return this.nodeObjNameField;
    //        }
    //        set
    //        {
    //            this.nodeObjNameField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public string referObjName
    //    {
    //        get
    //        {
    //            return this.referObjNameField;
    //        }
    //        set
    //        {
    //            this.referObjNameField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public string nodeType
    //    {
    //        get
    //        {
    //            return this.nodeTypeField;
    //        }
    //        set
    //        {
    //            this.nodeTypeField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public byte level
    //    {
    //        get
    //        {
    //            return this.levelField;
    //        }
    //        set
    //        {
    //            this.levelField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public string model
    //    {
    //        get
    //        {
    //            return this.modelField;
    //        }
    //        set
    //        {
    //            this.modelField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public object description
    //    {
    //        get
    //        {
    //            return this.descriptionField;
    //        }
    //        set
    //        {
    //            this.descriptionField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public object index
    //    {
    //        get
    //        {
    //            return this.indexField;
    //        }
    //        set
    //        {
    //            this.indexField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public byte refPort
    //    {
    //        get
    //        {
    //            return this.refPortField;
    //        }
    //        set
    //        {
    //            this.refPortField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlIgnoreAttribute()]
    //    public bool refPortSpecified
    //    {
    //        get
    //        {
    //            return this.refPortFieldSpecified;
    //        }
    //        set
    //        {
    //            this.refPortFieldSpecified = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public byte record
    //    {
    //        get
    //        {
    //            return this.recordField;
    //        }
    //        set
    //        {
    //            this.recordField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlIgnoreAttribute()]
    //    public bool recordSpecified
    //    {
    //        get
    //        {
    //            return this.recordFieldSpecified;
    //        }
    //        set
    //        {
    //            this.recordFieldSpecified = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public string avType
    //    {
    //        get
    //        {
    //            return this.avTypeField;
    //        }
    //        set
    //        {
    //            this.avTypeField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public replyNodeInStreams inStreams
    //    {
    //        get
    //        {
    //            return this.inStreamsField;
    //        }
    //        set
    //        {
    //            this.inStreamsField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public replyNodeOutStreams outStreams
    //    {
    //        get
    //        {
    //            return this.outStreamsField;
    //        }
    //        set
    //        {
    //            this.outStreamsField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public string url
    //    {
    //        get
    //        {
    //            return this.urlField;
    //        }
    //        set
    //        {
    //            this.urlField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public string accessServerObjName
    //    {
    //        get
    //        {
    //            return this.accessServerObjNameField;
    //        }
    //        set
    //        {
    //            this.accessServerObjNameField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public string restoreServerObjName
    //    {
    //        get
    //        {
    //            return this.restoreServerObjNameField;
    //        }
    //        set
    //        {
    //            this.restoreServerObjNameField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public string status
    //    {
    //        get
    //        {
    //            return this.statusField;
    //        }
    //        set
    //        {
    //            this.statusField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public object longitude
    //    {
    //        get
    //        {
    //            return this.longitudeField;
    //        }
    //        set
    //        {
    //            this.longitudeField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public object latitude
    //    {
    //        get
    //        {
    //            return this.latitudeField;
    //        }
    //        set
    //        {
    //            this.latitudeField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public byte totalVideoChannel
    //    {
    //        get
    //        {
    //            return this.totalVideoChannelField;
    //        }
    //        set
    //        {
    //            this.totalVideoChannelField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public byte onlineVideoChannel
    //    {
    //        get
    //        {
    //            return this.onlineVideoChannelField;
    //        }
    //        set
    //        {
    //            this.onlineVideoChannelField = value;
    //        }
    //    }
    //}

    ///// <remarks/>
    //[System.SerializableAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    //public partial class replyNodeInStreams
    //{

    //    private object transferField;

    //    private byte subStreamNoField;

    //    /// <remarks/>
    //    public object transfer
    //    {
    //        get
    //        {
    //            return this.transferField;
    //        }
    //        set
    //        {
    //            this.transferField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public byte subStreamNo
    //    {
    //        get
    //        {
    //            return this.subStreamNoField;
    //        }
    //        set
    //        {
    //            this.subStreamNoField = value;
    //        }
    //    }
    //}

    ///// <remarks/>
    //[System.SerializableAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    //public partial class replyNodeOutStreams
    //{

    //    private object transferField;

    //    private byte subStreamNoField;

    //    /// <remarks/>
    //    public object transfer
    //    {
    //        get
    //        {
    //            return this.transferField;
    //        }
    //        set
    //        {
    //            this.transferField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public byte subStreamNo
    //    {
    //        get
    //        {
    //            return this.subStreamNoField;
    //        }
    //        set
    //        {
    //            this.subStreamNoField = value;
    //        }
    //    }
    //}

    //#endregion
}
