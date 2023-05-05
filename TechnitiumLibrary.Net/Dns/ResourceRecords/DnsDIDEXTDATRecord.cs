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
    public class DnsDIDEXTDATRecord : DnsResourceRecordData
    {
        #region variables

        string _diddataTag; // optional primary key
        string _diddataDID; // optional secondary key
        string _diddataType;
        string _diddataSource; // "value" field
        string _diddataQuery;
        string _diddataParms;

        #endregion

        #region constructor

        public DnsDIDEXTDATRecord(string value)
        {
            _diddataTag = "default";
            _diddataDID = "";
            _diddataType = "";
            _diddataSource = value;
            _diddataQuery = "";
            _diddataParms = "";
        }

        public DnsDIDEXTDATRecord(Stream s)
            : base(s)
        { }

        public DnsDIDEXTDATRecord(dynamic jsonResourceRecord)
        {
            _length = Convert.ToUInt16(jsonResourceRecord.data.Value.Length);

            string[] parts = (jsonResourceRecord.data.Value as string).Split(' ');

            _diddataTag = parts[0];
            _diddataDID = parts[1];
            _diddataType = parts[2];
            _diddataSource = parts[3];
            _diddataQuery = parts[4];
            _diddataParms = parts[5];
        }

        public DnsDIDEXTDATRecord(string diddataTag, string diddataDID, string diddataType, string diddataSource, string diddataQUery, string diddataParms)
        {
            _diddataTag = diddataTag;
            _diddataDID = diddataDID;
            _diddataType = diddataType;
            _diddataSource = diddataSource;
            _diddataQuery = diddataQUery;
            _diddataParms = diddataParms;
        }

        #endregion

        #region protected

        protected override void Parse(Stream s)
        {
            int len = s.ReadByte();
            if (len < 0)
                throw new EndOfStreamException();
            _diddataTag = "";
            if (len > 0) _diddataTag = Encoding.ASCII.GetString(s.ReadBytes(len));

            len = s.ReadByte();
            if (len < 0)
                throw new EndOfStreamException();
            _diddataDID = "";
            if (len > 0) _diddataDID = Encoding.ASCII.GetString(s.ReadBytes(len));

            len = s.ReadByte();
            if (len < 0)
                throw new EndOfStreamException();
            _diddataType = "";
            if (len > 0) _diddataType = Encoding.ASCII.GetString(s.ReadBytes(len));

            len = s.ReadByte();
            if (len < 0)
                throw new EndOfStreamException();
            _diddataSource = "";
            if (len > 0) _diddataSource = Encoding.ASCII.GetString(s.ReadBytes(len));

            len = s.ReadByte();
            if (len < 0)
                throw new EndOfStreamException();
            _diddataQuery = "";
            if (len > 0) _diddataQuery = Encoding.ASCII.GetString(s.ReadBytes(len));

            len = s.ReadByte();
            if (len < 0)
                throw new EndOfStreamException();
            _diddataParms = "";
            if (len > 0) _diddataParms = Encoding.ASCII.GetString(s.ReadBytes(len));
        }

        protected override void WriteRecordData(Stream s, List<DnsDomainOffset> domainEntries)
        {
            s.WriteByte(Convert.ToByte(_diddataTag.Length));
            if (_diddataTag.Length > 0) s.Write(Encoding.ASCII.GetBytes(_diddataTag));
            s.WriteByte(Convert.ToByte(_diddataDID.Length));
            if (_diddataDID.Length > 0) s.Write(Encoding.ASCII.GetBytes(_diddataDID));
            s.WriteByte(Convert.ToByte(_diddataType.Length));
            if (_diddataType.Length > 0) s.Write(Encoding.ASCII.GetBytes(_diddataType));
            s.WriteByte(Convert.ToByte(_diddataSource.Length));
            if (_diddataSource.Length > 0) s.Write(Encoding.ASCII.GetBytes(_diddataSource));
            s.WriteByte(Convert.ToByte(_diddataQuery.Length));
            if (_diddataQuery.Length > 0) s.Write(Encoding.ASCII.GetBytes(_diddataQuery));
            s.WriteByte(Convert.ToByte(_diddataParms.Length));
            if (_diddataParms.Length > 0) s.Write(Encoding.ASCII.GetBytes(_diddataParms));
        }

        #endregion

        #region public

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            DnsDIDEXTDATRecord other = obj as DnsDIDEXTDATRecord;
            if (other == null)
                return false;

            return this._diddataTag.Equals(other._diddataTag, StringComparison.OrdinalIgnoreCase); // mwh
        }

        public override int GetHashCode()
        {
            return _diddataTag.GetHashCode();
        }

        public override string ToString()
        {
            return DnsDatagram.EncodeCharacterString(_diddataTag + ":" + _diddataDID + "=" + _diddataType + "," + _diddataSource + ", " + _diddataQuery + ", " + _diddataParms);
        }

        #endregion

        #region properties

        public string Tag
        { get { return _diddataTag; } }
        public string DID
        { get { return _diddataDID; } }
        public string Type
        { get { return _diddataType; } }
        public string Source
        { get { return _diddataSource; } }
        public string Query
        { get { return _diddataQuery; } }
        public string Parms
        { get { return _diddataParms; } }

        #endregion
    }
}
