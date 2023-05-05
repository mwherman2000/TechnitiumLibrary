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
    public class DnsDIDTXTRecord : DnsResourceRecordData
    {
        #region variables

        string _didtxtTag; // optional primary key
        string _didtxtDID; // optional secondary key
        string _didtxtTextData; // "value" field

        #endregion

        #region constructor

        public DnsDIDTXTRecord(string value)
        {
            _didtxtTag = "";
            _didtxtDID = "";
            _didtxtTextData = value;
        }

        public DnsDIDTXTRecord(string didtxtTag, string didtxtDID, string didtxtData)
        {
            _didtxtTag = didtxtTag;
            _didtxtDID = didtxtDID;
            _didtxtTextData = didtxtData;
        }

        public DnsDIDTXTRecord(Stream s)
            : base(s)
        { }

        public DnsDIDTXTRecord(dynamic jsonResourceRecord)
        {
            _length = Convert.ToUInt16(jsonResourceRecord.data.Value.Length);

            string[] parts = (jsonResourceRecord.data.Value as string).Split(' ');

            _didtxtTag = parts[0];
            _didtxtDID = parts[1];
            _didtxtTextData = parts[2];
        }

        #endregion

        #region protected

        protected override void Parse(Stream s)
        {
            int len = s.ReadByte();
            if (len < 0)
                throw new EndOfStreamException();
            _didtxtTag = "";
            if (len > 0) _didtxtTag = Encoding.ASCII.GetString(s.ReadBytes(len));

            len = s.ReadByte();
            if (len < 0)
                throw new EndOfStreamException();
            _didtxtDID = "";
            if (len > 0) _didtxtDID = Encoding.ASCII.GetString(s.ReadBytes(len));

            len = s.ReadByte();
            if (len < 0)
                throw new EndOfStreamException();
            _didtxtTextData = "";
            if (len > 0) _didtxtTextData = Encoding.ASCII.GetString(s.ReadBytes(len));
        }

        protected override void WriteRecordData(Stream s, List<DnsDomainOffset> domainEntries)
        {
            s.WriteByte(Convert.ToByte(_didtxtTag.Length));
            if (_didtxtTag.Length > 0) s.Write(Encoding.ASCII.GetBytes(_didtxtTag));

            s.WriteByte(Convert.ToByte(_didtxtDID.Length));
            if (_didtxtDID.Length > 0) s.Write(Encoding.ASCII.GetBytes(_didtxtDID));

            s.WriteByte(Convert.ToByte(_didtxtTextData.Length));
            if (_didtxtTextData.Length > 0) s.Write(Encoding.ASCII.GetBytes(_didtxtTextData));
        }

        #endregion

        #region public

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            DnsDIDTXTRecord other = obj as DnsDIDTXTRecord;
            if (other == null)
                return false;

            if (this._didtxtTag.Length > 0)
            {
                if (!this._didtxtTag.Equals(other._didtxtTag, StringComparison.OrdinalIgnoreCase))
                    return false;
            }
            else if (this._didtxtDID.Length > 0)
            {
                if (this._didtxtDID != other._didtxtDID)
                    return false;
            }
            else
            {
                if (this._didtxtTextData != other._didtxtTextData)
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return _didtxtTextData.GetHashCode();
        }

        public override string ToString()
        {
            return DnsDatagram.EncodeCharacterString(_didtxtTag + ":" + _didtxtDID + "=" + _didtxtTextData );
        }

        #endregion

        #region properties

        public string Tag
        { get { return _didtxtTag; } }
        public string DID
        { get { return _didtxtDID; } }
        public string TextData
        { get { return _didtxtTextData; } }

        #endregion
    }
}
