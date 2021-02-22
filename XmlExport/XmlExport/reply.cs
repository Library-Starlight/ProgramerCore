using System;
using System.Collections.Generic;
using System.Text;

namespace XmlExport
{
    // 注意: 生成的代码可能至少需要 .NET Framework 4.5 或 .NET Core/Standard 2.0。
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class reply
    {

        private byte pageNumField;

        private byte totalPageField;

        private ushort totalItemsField;

        private replyNode[] nodeListField;

        /// <remarks/>
        public byte pageNum
        {
            get
            {
                return this.pageNumField;
            }
            set
            {
                this.pageNumField = value;
            }
        }

        /// <remarks/>
        public byte totalPage
        {
            get
            {
                return this.totalPageField;
            }
            set
            {
                this.totalPageField = value;
            }
        }

        /// <remarks/>
        public ushort totalItems
        {
            get
            {
                return this.totalItemsField;
            }
            set
            {
                this.totalItemsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("node", IsNullable = false)]
        public replyNode[] nodeList
        {
            get
            {
                return this.nodeListField;
            }
            set
            {
                this.nodeListField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class replyNode
    {

        private string idField;

        private string nameField;

        private string titleField;

        private string nodeObjNameField;

        private string referObjNameField;

        private string nodeTypeField;

        private byte levelField;

        private string modelField;

        private string descriptionField;

        private string indexField;

        private byte recordField;

        private bool recordFieldSpecified;

        private string avTypeField;

        private replyNodeInStreams inStreamsField;

        private string outStreamsField;

        private string urlField;

        private string accessServerObjNameField;

        private string restoreServerObjNameField;

        private string statusField;

        private string longitudeField;

        private string latitudeField;

        private ushort totalVideoChannelField;

        private ushort onlineVideoChannelField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        public string nodeObjName
        {
            get
            {
                return this.nodeObjNameField;
            }
            set
            {
                this.nodeObjNameField = value;
            }
        }

        /// <remarks/>
        public string referObjName
        {
            get
            {
                return this.referObjNameField;
            }
            set
            {
                this.referObjNameField = value;
            }
        }

        /// <remarks/>
        public string nodeType
        {
            get
            {
                return this.nodeTypeField;
            }
            set
            {
                this.nodeTypeField = value;
            }
        }

        /// <remarks/>
        public byte level
        {
            get
            {
                return this.levelField;
            }
            set
            {
                this.levelField = value;
            }
        }

        /// <remarks/>
        public string model
        {
            get
            {
                return this.modelField;
            }
            set
            {
                this.modelField = value;
            }
        }

        /// <remarks/>
        public string description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public string index
        {
            get
            {
                return this.indexField;
            }
            set
            {
                this.indexField = value;
            }
        }

        /// <remarks/>
        public byte record
        {
            get
            {
                return this.recordField;
            }
            set
            {
                this.recordField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool recordSpecified
        {
            get
            {
                return this.recordFieldSpecified;
            }
            set
            {
                this.recordFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string avType
        {
            get
            {
                return this.avTypeField;
            }
            set
            {
                this.avTypeField = value;
            }
        }

        /// <remarks/>
        public replyNodeInStreams inStreams
        {
            get
            {
                return this.inStreamsField;
            }
            set
            {
                this.inStreamsField = value;
            }
        }

        /// <remarks/>
        public string outStreams
        {
            get
            {
                return this.outStreamsField;
            }
            set
            {
                this.outStreamsField = value;
            }
        }

        /// <remarks/>
        public string url
        {
            get
            {
                return this.urlField;
            }
            set
            {
                this.urlField = value;
            }
        }

        /// <remarks/>
        public string accessServerObjName
        {
            get
            {
                return this.accessServerObjNameField;
            }
            set
            {
                this.accessServerObjNameField = value;
            }
        }

        /// <remarks/>
        public string restoreServerObjName
        {
            get
            {
                return this.restoreServerObjNameField;
            }
            set
            {
                this.restoreServerObjNameField = value;
            }
        }

        /// <remarks/>
        public string status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        public string longitude
        {
            get
            {
                return this.longitudeField;
            }
            set
            {
                this.longitudeField = value;
            }
        }

        /// <remarks/>
        public string latitude
        {
            get
            {
                return this.latitudeField;
            }
            set
            {
                this.latitudeField = value;
            }
        }

        /// <remarks/>
        public ushort totalVideoChannel
        {
            get
            {
                return this.totalVideoChannelField;
            }
            set
            {
                this.totalVideoChannelField = value;
            }
        }

        /// <remarks/>
        public ushort onlineVideoChannel
        {
            get
            {
                return this.onlineVideoChannelField;
            }
            set
            {
                this.onlineVideoChannelField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class replyNodeInStreams
    {

        private object[] itemsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("subStreamNo", typeof(byte))]
        [System.Xml.Serialization.XmlElementAttribute("transfer", typeof(object))]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }
}
