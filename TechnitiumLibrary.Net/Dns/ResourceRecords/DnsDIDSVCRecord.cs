/*
Technitium Library
Copyright (C) 2019  Shreyas Zare (shreyas@technitium.com)

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.

*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TechnitiumLibrary.IO;

namespace TechnitiumLibrary.Net.Dns.ResourceRecords
{
    public class DnsDIDSVCRecord : DnsResourceRecordData
    {
        #region variables

        string _didsvcTag; // optional primary key
        string _didsvcDID; // optional secondary key
        string _didsvcType;
        string _didsvcDescription;
        string _didsvcServiceEndpointUrl; // "value" field

        #endregion

        #region constructor

        public DnsDIDSVCRecord(string value)
        {
            _didsvcTag = ""; 
            _didsvcDID = "";
            _didsvcType = "";
            _didsvcDescription = "";
            _didsvcServiceEndpointUrl = value;
        }

        public DnsDIDSVCRecord(Stream s)
            : base(s)
        { }

        public DnsDIDSVCRecord(dynamic jsonResourceRecord)
        {
            _length = Convert.ToUInt16(jsonResourceRecord.data.Value.Length);

            string[] parts = (jsonResourceRecord.data.Value as string).Split(' ');

            _didsvcTag = parts[0];
            _didsvcDID = parts[1];
            _didsvcType = parts[2];
            _didsvcDescription = parts[3];
            _didsvcServiceEndpointUrl = parts[4];
        }

        public DnsDIDSVCRecord(string didsvcTag, string didsvcDID, string didsvcType, string didsvcDescription, string didsvcServiceEndpointUrl)
        {
            _didsvcTag = didsvcTag;
            _didsvcDID = didsvcDID;
            _didsvcType = didsvcType;
            _didsvcDescription = didsvcDescription;
            _didsvcServiceEndpointUrl = didsvcServiceEndpointUrl;

        }

        #endregion

        #region protected

        protected override void Parse(Stream s)
        {
            int len = s.ReadByte();
            if (len < 0)
                throw new EndOfStreamException();
            _didsvcTag = "";
            if (len > 0) _didsvcTag = Encoding.ASCII.GetString(s.ReadBytes(len));

            len = s.ReadByte();
            if (len < 0)
                throw new EndOfStreamException();
            _didsvcDID = "";
            if (len > 0) _didsvcDID = Encoding.ASCII.GetString(s.ReadBytes(len));

            len = s.ReadByte();
            if (len < 0)
                throw new EndOfStreamException();
            _didsvcType = "";
            if (len > 0) _didsvcType = Encoding.ASCII.GetString(s.ReadBytes(len));

           len = s.ReadByte();
            if (len < 0)
                throw new EndOfStreamException();
            _didsvcDescription = "";
            if (len > 0) _didsvcDescription = Encoding.ASCII.GetString(s.ReadBytes(len));

            len = s.ReadByte();
            if (len < 0)
                throw new EndOfStreamException();
            _didsvcServiceEndpointUrl = "";
            if (len > 0) _didsvcServiceEndpointUrl = Encoding.ASCII.GetString(s.ReadBytes(len));
        }

        protected override void WriteRecordData(Stream s, List<DnsDomainOffset> domainEntries)
        {
            s.WriteByte(Convert.ToByte(_didsvcTag.Length));
            if (_didsvcTag.Length > 0) s.Write(Encoding.ASCII.GetBytes(_didsvcTag));
            s.WriteByte(Convert.ToByte(_didsvcDID.Length));
            if (_didsvcDID.Length > 0) s.Write(Encoding.ASCII.GetBytes(_didsvcDID));
            s.WriteByte(Convert.ToByte(_didsvcType.Length));
            if (_didsvcType.Length > 0) s.Write(Encoding.ASCII.GetBytes(_didsvcType));
            s.WriteByte(Convert.ToByte(_didsvcDescription.Length));
            if (_didsvcDescription.Length > 0) s.Write(Encoding.ASCII.GetBytes(_didsvcDescription));
            s.WriteByte(Convert.ToByte(_didsvcServiceEndpointUrl.Length));
            if (_didsvcServiceEndpointUrl.Length > 0) s.Write(Encoding.ASCII.GetBytes(_didsvcServiceEndpointUrl));
        }

        #endregion

        #region public

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            DnsDIDSVCRecord other = obj as DnsDIDSVCRecord;
            if (other == null)
                return false;

            if (this._didsvcTag.Length > 0)
            {
                if (!this._didsvcTag.Equals(other._didsvcTag, StringComparison.OrdinalIgnoreCase))
                    return false;
            }
            else if (this._didsvcDID.Length > 0)
            {
                if (this._didsvcDID != other._didsvcDID)
                    return false;
            }
            else
            {
                if ((this._didsvcType.Length > 0) && (this._didsvcType != other._didsvcType))
                    return false;
                if (this._didsvcServiceEndpointUrl != other._didsvcServiceEndpointUrl)
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return _didsvcServiceEndpointUrl.GetHashCode();
        }

        public override string ToString()
        {
            return DnsDatagram.EncodeCharacterString(_didsvcTag + ":" + _didsvcDID + "=" + _didsvcServiceEndpointUrl
                + "=" + _didsvcType + "," + _didsvcDescription + "," + _didsvcServiceEndpointUrl);
        }
        #endregion

        #region properties

        public string Tag
        { get { return _didsvcTag; } }
        public string DID
        { get { return _didsvcDID; } }
        public string Type
        { get { return _didsvcType; } }
        public string Description
        { get { return _didsvcDescription; } }
        public string ServiceEndpointUrl
        { get { return _didsvcServiceEndpointUrl; } }

        #endregion
    }
}
